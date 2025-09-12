pipeline {
  agent { label 'windows' }                 // your Windows agent
  options { timestamps(); disableConcurrentBuilds() }

  // For a classic single Pipeline job (not Multibranch), this makes pushes trigger builds:
  triggers { githubPush() }                 // or: pollSCM('H/5 * * * *') if webhook not possible

  environment {
    PROJECT     = 'LedxLiveReport/LedxLiveReport.csproj'
    CONFIG      = 'Release'
    PUBLISH_DIR = 'publish'
    IIS_HOST    = 'VMI809849'               // your IIS server
    IIS_SITE    = 'api.ledx.digital'        // exact IIS site name
    HEALTH_URL  = ''                        // set to http://api.ledx.digital/health if you have one
  }

  stages {
    stage('Checkout') { steps { checkout scm } }
    stage('Restore')  { steps { bat "dotnet restore" } }
    stage('Build')    { steps { bat "dotnet build %PROJECT% -c %CONFIG% --no-restore" } }
    stage('Test')     { steps { bat "dotnet test -c %CONFIG% --no-build --logger trx" } }
    stage('Publish')  {
      steps {
        bat "if exist %PUBLISH_DIR% rmdir /s /q %PUBLISH_DIR%"
        bat "dotnet publish %PROJECT% -c %CONFIG% -o %PUBLISH_DIR%"
        archiveArtifacts artifacts: 'publish/**/*', fingerprint: true
      }
    }

    // First run keeps WHAT-IF; remove later for speed
    stage('Deploy (what-if)') {
      when { branch 'main' }
      steps {
        withCredentials([usernamePassword(credentialsId: 'msdeploy-ledx',
          usernameVariable: 'DEPLOY_USER', passwordVariable: 'DEPLOY_PASS')]) {
          powershell '''
            $msd = (Get-Command msdeploy.exe -ErrorAction SilentlyContinue).Source
            if (-not $msd) {
              foreach ($p in @(
                "C:\\Program Files\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe",
                "C:\\Program Files (x86)\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe"
              )) { if (Test-Path $p) { $msd = $p; break } }
            }
            if (-not $msd) { throw "msdeploy.exe not found on agent." }

            & $msd -verb:sync `
              "-source:contentPath=$env:PUBLISH_DIR" `
              "-dest:contentPath=$env:IIS_SITE,computerName=https://$($env:IIS_HOST):8172/MsDeploy.axd?site=$($env:IIS_SITE),userName=$env:DEPLOY_USER,password=$env:DEPLOY_PASS,authType=Basic" `
              -whatif -allowUntrusted
          '''
        }
      }
    }

    stage('Deploy (apply)') {
      when { branch 'main' }                // <-- deploy only on pushes to main
      steps {
        withCredentials([usernamePassword(credentialsId: 'msdeploy-ledx',
          usernameVariable: 'DEPLOY_USER', passwordVariable: 'DEPLOY_PASS')]) {
          powershell '''
            $msd = (Get-Command msdeploy.exe -ErrorAction SilentlyContinue).Source
            if (-not $msd) {
              foreach ($p in @(
                "C:\\Program Files\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe",
                "C:\\Program Files (x86)\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe"
              )) { if (Test-Path $p) { $msd = $p; break } }
            }
            if (-not $msd) { throw "msdeploy.exe not found on agent." }

            & $msd -verb:sync `
              "-source:contentPath=$env:PUBLISH_DIR" `
              "-dest:contentPath=$env:IIS_SITE,computerName=https://$($env:IIS_HOST):8172/MsDeploy.axd?site=$($env:IIS_SITE),userName=$env:DEPLOY_USER,password=$env:DEPLOY_PASS,authType=Basic" `
              -enableRule:AppOffline -usechecksum -retryAttempts:3 -retryInterval:2000 -allowUntrusted
          '''
        }
      }
    }

    stage('Health check') {
      when { allOf { branch 'main'; expression { return env.HEALTH_URL?.trim() } } }
      steps {
        powershell '''
          try {
            $r = Invoke-WebRequest $env:HEALTH_URL -UseBasicParsing -TimeoutSec 15
            if ($r.StatusCode -ne 200) { throw "Health check failed: $($r.StatusCode)" }
          } catch { Write-Error $_; exit 1 }
        '''
      }
    }
  }

  post {
    success { echo '✅ Build, publish, and deploy complete.' }
    failure { echo '❌ Pipeline failed. See stage logs.' }
  }
}

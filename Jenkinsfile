pipeline {
  agent { label 'windows' }
  options { timestamps(); disableConcurrentBuilds() }
  triggers { pollSCM('H/5 * * * *') }   // remove if you switch to GitHub webhooks

  environment {
    PROJECT     = 'LedxLiveReport/LedxLiveReport.csproj'
    CONFIG      = 'Release'
    PUBLISH_DIR = 'publish'
    IIS_HOST    = 'localhost'
    IIS_SITE    = 'api.ledx.digital'
    HEALTH_URL  = ''                    // e.g. 'http://api.ledx.digital/health'
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

        // stamp version.json in the publish output
        powershell '''
          $sha = $env:GIT_COMMIT; if (-not $sha) { $sha = git rev-parse HEAD }
          $info = [ordered]@{
            time  = (Get-Date).ToString("s")
            sha   = $sha
            build = $env:BUILD_NUMBER
          } | ConvertTo-Json
          $out = Join-Path $env:PUBLISH_DIR 'version.json'
          $info | Out-File -Encoding utf8 -FilePath $out
        '''
        archiveArtifacts artifacts: 'publish/**/*', fingerprint: true
      }
    }

    stage('Deploy') {
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

            $src = (Resolve-Path -LiteralPath $env:PUBLISH_DIR).Path
            Write-Host "Deploying from $src to $env:IIS_SITE on $env:IIS_HOST"

            & $msd -verb:sync `
              "-source:contentPath=$src" `
              "-dest:contentPath=$env:IIS_SITE,computerName=https://$($env:IIS_HOST):8172/MsDeploy.axd?site=$($env:IIS_SITE),userName=$env:DEPLOY_USER,password=$env:DEPLOY_PASS,authType=Basic" `
              -enableRule:AppOffline -usechecksum -retryAttempts:3 -retryInterval:2000 -allowUntrusted
          '''
        }
      }
    }

    stage('Health check') {
      when { expression { return env.HEALTH_URL?.trim() } }
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
    success { echo '✅ Build, publish, and msdeploy completed.' }
    failure { echo '❌ Pipeline failed. Check the Deploy stage logs.' }
  }
}

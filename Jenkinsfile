pipeline {
  agent { label 'windows' }
  options { timestamps(); disableConcurrentBuilds() }

  environment {
    PROJECT     = 'LedxLiveReport/LedxLiveReport.csproj'
    CONFIG      = 'Release'
    PUBLISH_DIR = 'publish'
    IIS_HOST    = 'localhost'            // since Jenkins is on the IIS server
    IIS_SITE    = 'api.ledx.digital'
    HEALTH_URL  = ''                     // set later if you have one
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

    // optional first-run validation; remove later
    stage('Deploy (what-if)') {
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

  # Resolve absolute path to the published output
  $src = (Resolve-Path -LiteralPath $env:PUBLISH_DIR).Path
  Write-Host "Deploying from $src"

  & $msd -verb:sync `
    "-source:contentPath=$src" `
    "-dest:contentPath=$env:IIS_SITE,computerName=https://$($env:IIS_HOST):8172/MsDeploy.axd?site=$($env:IIS_SITE),userName=$env:DEPLOY_USER,password=$env:DEPLOY_PASS,authType=Basic" `
    -enableRule:AppOffline -usechecksum -retryAttempts:3 -retryInterval:2000 -allowUntrusted
'''

        }
      }
    }

    stage('Deploy (apply)') {
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

  # Resolve absolute path to the published output
  $src = (Resolve-Path -LiteralPath $env:PUBLISH_DIR).Path
  Write-Host "Deploying from $src"

  & $msd -verb:sync `
    "-source:contentPath=$src" `
    "-dest:contentPath=$env:IIS_SITE,computerName=https://$($env:IIS_HOST):8172/MsDeploy.axd?site=$($env:IIS_SITE),userName=$env:DEPLOY_USER,password=$env:DEPLOY_PASS,authType=Basic" `
    -enableRule:AppOffline -usechecksum -retryAttempts:3 -retryInterval:2000 -allowUntrusted
'''

        }
      }
    }

    stage('Health check') {
      when { expression { return env.HEALTH_URL?.trim() } }  // only runs if set
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
    failure { echo '❌ Pipeline failed. Check logs above.' }
  }
}

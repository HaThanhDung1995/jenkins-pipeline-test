//Kaynak kodun adresi
String githubUrl = "https://github.com/HaThanhDung1995/jenkins-pipeline-test"
//test
//Kaynak kodun içerisindeki projenin ismi
String projectName = "MyPhamTrueLife"

//Kaynak kodun publish edileceği dizin
String publishedPath = "TestServer\\TestServer.App\\bin\\Release\\netcoreapp2.2\\publish"

//Hedef makinesindeki IIS'de tanımlı olan sitenizin ismi
String iisApplicationName = "cisample"

//Hedef makinesindeki IIS'de tanımlı olan sitenizin dizini
String iisApplicationPath = "C:\\inetpub\\wwwroot\\projecttinh"
pipeline {
    agent any
    stages {
        stage('Checkout') {
            steps {
                checkout([
                $class: 'GitSCM', 
                branches: [[name: 'master']], 
                doGenerateSubmoduleConfigurations: false, 
                extensions: [], 
                submoduleCfg: [], 
                userRemoteConfigs: [[url: """ "${githubUrl}" """]]])
            }
        
    }
        stage('Build') {
            steps {
                bat """
                iisreset /stop
                cd ${projectName}
                dotnet build -c Release /p:Version=${BUILD_NUMBER}
                dotnet publish -o  C:\\inetpub\\wwwroot\\jenkins-test
                iisreset /noforce
                """
            }
        }
    }
}
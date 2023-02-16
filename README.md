# DTE Study API
This project is an ASP.NET Core Web API project as an AWS Lambda exposed through Amazon API Gateway. 

## Run the application locally

1. Update appsettings.json with secrets from AWS
2. Update the https certification
```
dotnet dev-certs https --clean
dotnet dev-certs https
dotnet dev-certs https --trust
```
3. Right click on the study-api project and select properties.  When the modal pops up select run/configurations/default and set the environment variable ASPNETCORE_ENVIRONMENT to Development

4. Install the AWS CLI
```
brew install awscli  
```
5. Run aws configure and add the AccessKeyId and SecretAccessKey which can be generated on AWS
```
aws configure
```
6. Get your device ID from AWS
7. Get new AccessKeyId, SecretAccessKey and SessionToken to get around MFA
```
aws sts get-session-token --serial-number replaceMeWithDeviceID --token-code replaceMeWithTokenFromDevice
```
8. run aws configure and add the new AccessKeyId and SecretAccessKey when prompted
```
aws configure
```
9. Set the new session key inside of your configure file
```
aws configure set aws_session_token replaceMeWithSessionToken
```

10. To access the API directly go to https://localhost:5001/swagger

11. To generate a new token you must delete your aws_session_token from your aws credentials file, restart the process at stage 4 and add your original AccessKeyId and SecretAccessKey. This is because the session token is only valid for 1 day.

If you ever need to see or update any of the configure options these can be found by running the following command on mac
```
 open ~/.aws/credentials
```



The Serverless Application Model Command Line Interface (SAM CLI) is an extension of the AWS CLI that adds functionality for building and testing Lambda applications. It uses Docker to run your functions in an Amazon Linux environment that matches Lambda. It can also emulate your application's build environment and API.

To use the SAM CLI, you need the following tools.

* SAM CLI - [Install the SAM CLI](https://docs.aws.amazon.com/serverless-application-model/latest/developerguide/serverless-sam-cli-install.html)
* .NET Core - [Install .NET Core](https://www.microsoft.com/net/download)
* Docker - [Install Docker community edition](https://hub.docker.com/search/?type=edition&offering=community)

## Here are some steps to follow to get started from the command line:

Execute build
```
    dotnet build
```

Execute unit tests
```
    dotnet test
```

Execute SAM client to run the function locally, you can choose or create a new event that points to the controller action
```
sam build StudyApiFunction --template template.yaml --build-dir .aws-sam/build
sam local invoke StudyApiFunction --template .aws-sam/build/template.yaml --event "./events/GetAllStudies.json"
```
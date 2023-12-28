# DTE Study API

DTE Study API is an ASP.NET Core Web API project that provides a backend for a volunteer service application. The API is designed to interact with other APIs like participant-api and location-api. The service is deployed on AWS Lambda and is exposed through Amazon API Gateway.
## Table of Contents
- [Project Description](#project-description)
- [How to Install and Run the Project](#how-to-install-and-run-the-project)
- [Usage](#usage)

## Project Description
The DTE Study API is designed to support a volunteer service application that allows volunteers to register and be part of research studies related to health conditions they're interested in. The API provides a simple registration process that captures the volunteers' basic information, including contact details. Once registered, volunteers can be contacted about health conditions they're interested in.

The API interacts with participant-api and location-api to provide information about participants and locations for the research studies. The API is built using ASP.NET Core and deployed as an AWS Lambda function for scalability and cost-effectiveness. The API is exposed through Amazon API Gateway, which allows for easy configuration and management of the API.

## How to Install and Run the Project
To run the project, you will need to have the following installed on your machine:

- .NET Core 6 SDK or later
- AWS CLI

To run the project, follow these steps:
1. Clone the repository to your local machine.
2. Update `appsettings.json` with secrets from AWS.
3. Update the https certification by running the following commands:
```
dotnet dev-certs https --clean
dotnet dev-certs https
dotnet dev-certs https --trust
```
3. Right click on the study-api project and select properties. When the modal pops up select run/configurations/default and set the environment variable ASPNETCORE_ENVIRONMENT to Development.
4. Install the AWS CLI by running the following command:
```
brew install awscli
```
5. Run `aws configure` and add the `AccessKeyId` and `SecretAccessKey` which can be generated on AWS.
```
aws configure
```
6. Get your device ID from AWS.
7. Get new `AccessKeyId`, `SecretAccessKey`, and `SessionToken` to get around MFA.
```
aws sts get-session-token --serial-number replaceMeWithDeviceID --token-code replaceMeWithTokenFromDevice
```
8. Run `aws configure` and add the new `AccessKeyId` and `SecretAccessKey` when prompted.
```
aws configure
```
9. Set the new session key inside of your configure file.
```
aws configure set aws_session_token replaceMeWithSessionToken
```
10. To access the API directly go to https://localhost:5001/swagger.

11. To generate a new token you must delete your `aws_session_token` from your `aws` credentials file, restart the process at stage 4 and add your original `AccessKeyId` and `SecretAccessKey`. This is because the session token is only valid for 1 day.

If you ever need to see or update any of the configure options, these can be found by running the following command on Mac:
```
open ~/.aws/credentials
```

## Usage
To use the API, send HTTP requests to the endpoint that was created by the API Gateway deployment.

To use the project, you can follow these steps:

1. Set the environment variable `ASPNETCORE_ENVIRONMENT` to `Local`.
2. Start the project.
3. Access the API through `https://localhost:5001/swagger`.

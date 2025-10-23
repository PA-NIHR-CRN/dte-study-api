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

- [.NET Core 8 SDK or later](#net-core-8-sdk-or-later)
- [Homebrew](#homebrew)
- [AWS CLI](#aws-cli)
- [C# dev kit extension for VS Code](#c-dev-kit-extension-for-vs-code)
- [VS Code Solution Explorer extension](#vs-code-solution-explorer-extension)

To run the project, follow these steps:
1. Clone the repository to your local machine.
2. Update `appsettings.json` with secrets from AWS.
3. Update the https certification by running the following commands:
```
dotnet dev-certs https --clean
dotnet dev-certs https
dotnet dev-certs https --trust
```
3. Right click on the study-api project and select properties. When the modal pops up select run/configurations/default and set the environment variable ASPNETCORE_ENVIRONMENT to Development or add launchSettings.json to the study-api project in a Properties folder with the following content:
```
{
  "profiles": {
    "study-api": {
      "commandName": "Project",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```
5. Run `aws configure` and add the `AccessKeyId`, `SecretAccessKey` and `Region` which can be obtained from the dev secret user on AWS.
```
aws configure
```
7. Add nuget package
```
dotnet nuget add source --username ${{ secrets.NUGET_PACKAGE_USERNAME }} --password ${{ secrets.NUGET_PACKAGE_TOKEN }} --store-password-in-clear-text --name github "https://nuget.pkg.github.com/pa-nihr-crn/index.json"
```
Secrets can be obtained from the team


## Usage
To use the API, send HTTP requests to the endpoint that was created by the API Gateway deployment.

To use the project, you can follow these steps:

1. Set the environment variable `ASPNETCORE_ENVIRONMENT` to `Development`.
2. Start the project.
3. Access the API through `https://localhost:5001/swagger`.

## Pre-requisites
### .NET Core 6 SDK or later
1. The project requires .NET Core 6 SDK or later to be installed on your machine. You can download the latest version of .NET Core from [here](https://dotnet.microsoft.com/download/dotnet/6.0).
2. Sym-link your .NET SDK by running the following command in your terminal(this will require sudo access):
For Mac M1:
```
sudo ln -s /usr/local/share/dotnet/dotnet /usr/local/bin/
```
For Mac Intel:
```
sudo ln -s /usr/local/share/dotnet/x64/dotnet /usr/local/bin/
```
3. Add the PATH to your .NET SDK to your `~/.zshrc` or `~/.bashrc` file:
```
PATH="/usr/local/share/dotnet:$PATH"
```


### Homebrew
1. Homebrew is a package manager for macOS. You can install Homebrew by running the following command in your terminal:
```
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
```
2. Add the PATH to Homebrew to your `~/.zshrc` or `~/.bashrc` file:
```
PATH="/opt/homebrew/bin:$PATH"
```

### AWS CLI
The AWS CLI is a command-line tool that allows you to interact with AWS services. You can install the AWS CLI by running the following command in your terminal:
```
brew install awscli
```

### C# dev kit extension for VS Code
The C# dev kit extension for VS Code allows you to develop .NET Core applications in VS Code. You can install the extension by searching for `C#` in the extensions tab in VS Code.

### VS Code Solution Explorer extension
The VS Code Solution Explorer extension allows you to view the solution explorer in VS Code. You can install the extension by searching for `Solution Explorer` in the extensions tab in VS Code.  More information about the extension can be found [here](https://marketplace.visualstudio.com/items?itemName=fernandoescolar.vscode-solution-explorer).

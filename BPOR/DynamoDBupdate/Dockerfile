# Use the official .NET 8 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG NUGET_PACKAGE_USERNAME
ARG NUGET_PACKAGE_TOKEN
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copy project files
COPY ["BPOR/DynamoDBupdate/DynamoDBupdate.csproj", "DynamoDBupdate/"]

# Add your private NuGet source for restore
RUN dotnet nuget add source --username $NUGET_PACKAGE_USERNAME --password $NUGET_PACKAGE_TOKEN --store-password-in-clear-text --name github "https://nuget.pkg.github.com/pa-nihr-crn/index.json"

# Restore dependencies
RUN dotnet restore "DynamoDBupdate/DynamoDBupdate.csproj"

# Copy rest of the source code
COPY BPOR/DynamoDBupdate/ DynamoDBupdate/

# Build and publish
RUN dotnet publish "DynamoDBupdate/DynamoDBupdate.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DynamoDBupdate.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG NUGET_PACKAGE_USERNAME
ARG NUGET_PACKAGE_TOKEN
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Add NuGet source
RUN dotnet nuget add source --username $NUGET_PACKAGE_USERNAME --password $NUGET_PACKAGE_TOKEN --store-password-in-clear-text --name github "https://nuget.pkg.github.com/pa-nihr-crn/index.json"

# Copy the main csproj file
COPY ["BPOR/DynamoDBupdate/DynamoDBupdate.csproj", "DynamoDBupdate/"]

# Restore dependencies
RUN dotnet restore "DynamoDBupdate/DynamoDBupdate.csproj"

# Copy all required projects to resolve project references
COPY "BPOR/BPOR.Infrastructure" "BPOR.Infrastructure"
COPY "BPOR/BPOR.Domain" "BPOR.Domain"
COPY "BPOR/DynamoDBupdate" "DynamoDBupdate"
COPY "BPOR/NIHR.Infrastructure" "NIHR.Infrastructure"
COPY "BPOR/NIHR.Infrastructure.AspNetCore" "NIHR.Infrastructure.AspNetCore"
COPY "BPOR/NIHR.Infrastructure.EntityFrameworkCore" "NIHR.Infrastructure.EntityFrameworkCore"
COPY "BPOR/NIHR.Infrastructure.EntityFrameworkCore.MySql" "NIHR.Infrastructure.EntityFrameworkCore.MySql"

WORKDIR "/src/DynamoDBupdate"

# Build
RUN dotnet build "DynamoDBupdate.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DynamoDBupdate.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DynamoDBupdate.dll"]

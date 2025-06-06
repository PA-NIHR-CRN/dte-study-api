FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define build configuration with default
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copy project files
COPY ["BPOR/DynamoDBupdate/DynamoDBupdate.csproj", "DynamoDBupdate/"]

# Copy supporting projects (to resolve references early)
COPY "BPOR/BPOR.Infrastructure" "BPOR.Infrastructure"
COPY "BPOR/BPOR.Domain" "BPOR.Domain"
COPY "BPOR/DynamoDBupdate" "DynamoDBupdate"
COPY "BPOR/NIHR.Infrastructure" "NIHR.Infrastructure"
COPY "BPOR/NIHR.Infrastructure.AspNetCore" "NIHR.Infrastructure.AspNetCore"
COPY "BPOR/NIHR.Infrastructure.EntityFrameworkCore" "NIHR.Infrastructure.EntityFrameworkCore"
COPY "BPOR/NIHR.Infrastructure.EntityFrameworkCore.MySql" "NIHR.Infrastructure.EntityFrameworkCore.MySql"

# Restore dependencies
WORKDIR /src/DynamoDBupdate
RUN dotnet restore "DynamoDBupdate.csproj"

# Add GitHub NuGet source via build argument (set securely at runtime)
# This must be done AFTER restore if private packages are needed for build
# (optional, if restore needs access to GitHub Packages)
ARG NUGET_PACKAGE_USERNAME
ARG NUGET_PACKAGE_TOKEN
RUN dotnet nuget add source --username "$NUGET_PACKAGE_USERNAME" --password "$NUGET_PACKAGE_TOKEN" --store-password-in-clear-text --name github "https://nuget.pkg.github.com/pa-nihr-crn/index.json"

# Build and publish
RUN dotnet publish "DynamoDBupdate.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DynamoDBupdate.dll"]

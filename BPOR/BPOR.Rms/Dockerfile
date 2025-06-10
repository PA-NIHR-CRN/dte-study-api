FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG NUGET_PACKAGE_USERNAME
ARG NUGET_PACKAGE_TOKEN
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BPOR/BPOR.Rms/BPOR.Rms.csproj", "BPOR.Rms/"]
RUN dotnet nuget add source --username $NUGET_PACKAGE_USERNAME --password $NUGET_PACKAGE_TOKEN --store-password-in-clear-text --name github "https://nuget.pkg.github.com/pa-nihr-crn/index.json"
RUN dotnet restore "BPOR.Rms/BPOR.Rms.csproj"
COPY "BPOR/BPOR.Infrastructure" "BPOR.Infrastructure"
COPY "BPOR/BPOR.Domain" "BPOR.Domain"
COPY "BPOR/BPOR.Rms" "BPOR.Rms"
COPY "BPOR/BPOR.Registration.Stream.Handler" "BPOR.Registration.Stream.Handler"
COPY "BPOR/NIHR.Infrastructure" "NIHR.Infrastructure"
COPY "BPOR/NIHR.Infrastructure.AspNetCore" "NIHR.Infrastructure.AspNetCore"
COPY "BPOR/NIHR.Infrastructure.EntityFrameworkCore" "NIHR.Infrastructure.EntityFrameworkCore"
COPY "BPOR/NIHR.Infrastructure.EntityFrameworkCore.MySql" "NIHR.Infrastructure.EntityFrameworkCore.MySql"
COPY "BPOR/NIHR.NotificationService" "NIHR.NotificationService"
COPY "BPOR/NIHR.GovUk.AspNetCore.Mvc" "NIHR.GovUk.AspNetCore.Mvc"
COPY "BPOR/DynamoDBupdate" "DynamoDBupdate"

WORKDIR "/src/BPOR.Rms"
RUN dotnet build "BPOR.Rms.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BPOR.Rms.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BPOR.Rms.dll"]

# TODO update this dockerfile when packages are created

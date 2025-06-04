# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["BPOR/DynamoDBupdate/DynamoDBupdate.csproj", "BPOR/DynamoDBupdate/"]

RUN dotnet restore "BPOR/DynamoDBupdate/DynamoDBupdate.csproj"

COPY BPOR/DynamoDBupdate/. BPOR/DynamoDBupdate/

WORKDIR "/src/BPOR/DynamoDBupdate"

RUN dotnet publish "DynamoDBupdate.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

USER $APP_UID

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DynamoDBupdate.dll"]
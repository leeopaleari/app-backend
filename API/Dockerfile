FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

## CAPROVER CONFIG

#ARG CAPROVER_GIT_COMMIT_SHA=${CAPROVER_GIT_COMMIT_SHA}
#ENV CAPROVER_GIT_COMMIT_SHA=${CAPROVER_GIT_COMMIT_SHA}
#
#ARG SRV_DB_PWD=${SRV_DB_PWD}
#ENV SRV_DB_PWD=${SRV_DB_PWD}
#
#ARG SRV_DB_USER=${SRV_DB_USER}
#ENV SRV_DB_USER=${SRV_DB_USER}
#
#ARG SRV_DB_PORT=${SRV_DB_PORT}
#ENV SRV_DB_PORT=${SRV_DB_PORT}
#
#ARG SRV_DB_NAME=${SRV_DB_NAME}
#ENV SRV_DB_NAME=${SRV_DB_NAME}

WORKDIR /src

COPY ["API/API.csproj", "API/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infra.Data/Infra.Data.csproj", "Infra.Data/"]
COPY ["Infra.IoC/Infra.IoC.csproj", "Infra.IoC/"]

RUN dotnet restore "API/API.csproj"
COPY . .
WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]

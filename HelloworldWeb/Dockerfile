#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG ACR_NAME=""
ENV DOCKER_REGISTRY $ACR_NAME.azurecr.io
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HelloworldWeb/HelloworldWeb.csproj", "HelloworldWeb/"]
RUN dotnet restore "./HelloworldWeb/HelloworldWeb.csproj"
COPY . .
WORKDIR "/src/HelloworldWeb"
RUN dotnet build "./HelloworldWeb.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HelloworldWeb.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloworldWeb.dll"]
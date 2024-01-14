# Base stage with the ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Switch to root user to install packages
USER root

# Install ICU libraries
RUN apt-get update && apt-get install -y libicu-dev

# Switch back to the 'app' user for security reasons
USER app

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage for compiling the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CourtDemoProject.CaseManagementSystem.Api/CourtDemoProject.CaseManagementSystem.Api.csproj", "CourtDemoProject.CaseManagementSystem.Api/"]
RUN dotnet restore "./CourtDemoProject.CaseManagementSystem.Api/./CourtDemoProject.CaseManagementSystem.Api.csproj"
COPY . .
WORKDIR "/src/CourtDemoProject.CaseManagementSystem.Api"
RUN dotnet build "./CourtDemoProject.CaseManagementSystem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage for preparing the release binaries
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CourtDemoProject.CaseManagementSystem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage creating the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CourtDemoProject.CaseManagementSystem.Api.dll"]

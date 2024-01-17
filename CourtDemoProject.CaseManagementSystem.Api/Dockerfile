FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
EXPOSE 8080
EXPOSE 8081

ARG BUILD_CONFIGURATION=Release
WORKDIR /src/CourtDemoProject.CaseManagementSystem.Api

# Copy the API project file
COPY ["CourtDemoProject.CaseManagementSystem.Api/CourtDemoProject.CaseManagementSystem.Api.csproj", "./"]

# Copy the Data project file
COPY ["CourtDemoProject.CaseManagementSystem.Data/CourtDemoProject.CaseManagementSystem.Data.csproj", "../CourtDemoProject.CaseManagementSystem.Data/"]

RUN dotnet restore "CourtDemoProject.CaseManagementSystem.Api.csproj"
COPY CourtDemoProject.CaseManagementSystem.Api/ .
COPY CourtDemoProject.CaseManagementSystem.Data/ ../CourtDemoProject.CaseManagementSystem.Data/
RUN dotnet build "CourtDemoProject.CaseManagementSystem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CourtDemoProject.CaseManagementSystem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CourtDemoProject.CaseManagementSystem.Api.dll"]
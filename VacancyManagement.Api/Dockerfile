FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VacancyManagement.Api/VacancyManagement.Api.csproj", "VacancyManagement.Api/"]
COPY ["VacancyManagement.Application/VacancyManagement.Application.csproj", "VacancyManagement.Application/"]
COPY ["VacancyManagement.Domain/VacancyManagement.Domain.csproj", "VacancyManagement.Domain/"]
COPY ["VacancyManagement.Infrastructure/VacancyManagement.Infrastructure.csproj", "VacancyManagement.Infrastructure/"]
RUN dotnet restore "VacancyManagement.Api/VacancyManagement.Api.csproj"
COPY . .
WORKDIR "/src/VacancyManagement.Api"
RUN dotnet build "VacancyManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "VacancyManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VacancyManagement.Api.dll"]

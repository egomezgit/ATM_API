# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ATM_API.Web/ATM_API.Web.csproj", "src/ATM_API.Web/"]
COPY ["src/ATM_API.Application/ATM_API.Application.csproj", "src/ATM_API.Application/"]
COPY ["src/ATM_API.Core/ATM_API.Core.csproj", "src/ATM_API.Core/"]
COPY ["src/ATM_API.Infrastructure/ATM_API.Infrastructure.csproj", "src/ATM_API.Infrastructure/"]
RUN dotnet restore "./src/ATM_API.Web/ATM_API.Web.csproj"
COPY . .
WORKDIR "/src/src/ATM_API.Web"
RUN dotnet build "./ATM_API.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ATM_API.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ATM_API.Web.dll"]
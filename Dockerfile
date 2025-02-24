# Etapa 1: Build de la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./src/ATM_API.Web/ATM_API.Web.csproj ./src/ATM_API.Web/
COPY ./src/ATM_API.Application/ATM_API.Application.csproj ./src/ATM_API.Application/
COPY ./src/ATM_API.Infrastructure/ATM_API.Infrastructure.csproj ./src/ATM_API.Infrastructure/
COPY ./src/ATM_API.Core/ATM_API.Core.csproj ./src/ATM_API.Core/
RUN dotnet restore ./src/ATM_API.Web/ATM_API.Web.csproj

COPY . .
RUN dotnet build ./src/ATM_API.Web/ATM_API.Web.csproj -c Release -o /app/build
RUN dotnet publish ./src/ATM_API.Web/ATM_API.Web.csproj -c Release -o /app/publish

# Etapa 2: Contenedor para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ATM_API.Web.dll"]

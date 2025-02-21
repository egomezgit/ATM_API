FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ATM_API/ATM_API.csproj", "ATM_API/"]
RUN dotnet restore "ATM_API/ATM_API.csproj"
COPY . .
WORKDIR "/src/ATM_API"
RUN dotnet build "ATM_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ATM_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ATM_API.dll"]
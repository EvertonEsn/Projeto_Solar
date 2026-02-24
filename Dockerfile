# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["Solar.API/Solar.API.csproj", "Solar.API/"]
COPY ["Solar.Application/Solar.Application.csproj", "Solar.Application/"]
COPY ["Solar.CrossCutting/Solar.CrossCutting.csproj", "Solar.CrossCutting/"]
COPY ["Solar.Domain/Solar.Domain.csproj", "Solar.Domain/"]
COPY ["Solar.Infrastructure/Solar.Infrastructure.csproj", "Solar.Infrastructure/"]

RUN dotnet restore "Solar.API/Solar.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR /src/Solar.API
RUN dotnet publish "Solar.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS="http://+:{PORT:-80}"
EXPOSE 80

ENTRYPOINT ["dotnet", "Solar.API.dll"]

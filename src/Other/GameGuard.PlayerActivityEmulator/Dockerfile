FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ["Other/GameGuard.PlayerActivityEmulator/GameGuard.PlayerActivityEmulator.csproj", "Other/GameGuard.PlayerActivityEmulator/"]
COPY ["GameGuard.Application/GameGuard.Application.csproj", "GameGuard.Application/"]
COPY ["GameGuard.Domain/GameGuard.Domain.csproj", "GameGuard.Domain/"]
RUN dotnet restore "Other/GameGuard.PlayerActivityEmulator/GameGuard.PlayerActivityEmulator.csproj"

# Copy everything else and build
COPY . ./
RUN dotnet publish "Other/GameGuard.PlayerActivityEmulator/GameGuard.PlayerActivityEmulator.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "GameGuard.PlayerActivityEmulator.dll"]
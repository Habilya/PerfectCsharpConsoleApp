# PRELIMINARY STEPS
# 1. Right-click on project > Open in Terminal
# 2. dotnet dev-certs https -ep cert.pfx -p Test1234!
# 3. Rebuild project
# 4. open [...]test\Customers.WebApp.Tests\bin\Debug\net8.0
# 5. open powershell as administrator and cd to the path above
# 6. Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy Unrestricted -Force
# 6. ./playwright.ps1 install
# DONE
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy everything
COPY . ./

# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build-env /app/out .
# Run this to generate it: dotnet dev-certs https -ep cert.pfx -p Test1234!
COPY ["cert.pfx", "/https/cert.pfx"]
ENTRYPOINT ["dotnet", "Customers.WebApp.dll"]

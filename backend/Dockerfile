# ---- Stage 1 : Build ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier uniquement les .csproj d'abord (optimisation du cache Docker)
COPY src/API/API.csproj src/API/
COPY src/Application/Application.csproj src/Application/
COPY src/Domain/Domain.csproj src/Domain/
COPY src/Persistance/Persistance.csproj src/Persistance/

RUN dotnet restore src/API/API.csproj

# Copier le reste du code source
COPY src/ src/

# Build et publish
RUN dotnet publish src/API/API.csproj -c Release -o /app/publish --no-restore

# ---- Stage 2 : Runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Créer un utilisateur non-root (bonne pratique sécurité, section 6.1 du cahier des charges)
RUN adduser --disabled-password --gecos "" appuser
USER appuser

COPY --from=build /app/publish .

# Port interne (section 4.1)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "API.dll"]
# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar todo el proyecto
COPY . .

# Restaurar dependencias
RUN dotnet restore

# Compilar y publicar en modo Release
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiar los archivos publicados desde la etapa anterior
COPY --from=build /app/publish .

# Configurar la URL de escucha
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "ClienteAPI.dll"]

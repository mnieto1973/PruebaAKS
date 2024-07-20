 # Usar la imagen base de ASP.NET Core
 FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
 WORKDIR /app
 EXPOSE 80
 EXPOSE 443
 
 # Usar la imagen base del SDK de .NET para construir la aplicación
 FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
 WORKDIR /src
 COPY ["PruebaAKS.csproj", "."]
 RUN dotnet restore "PruebaAKS.csproj"
 COPY . .
 WORKDIR "/src/"
 RUN dotnet build "PruebaAKS.csproj" -c Release -o /app/build
 
 # Publicar la aplicación
 FROM build AS publish
 RUN dotnet publish "PruebaAKS.csproj" -c Release -o /app/publish
 
 # Construir la imagen final
 FROM base AS final
 WORKDIR /app
 COPY --from=publish /app/publish .
 
 # Copiar los archivos .db al contenedor
# Asegúrate de que los archivos .db estén en el mismo directorio que tu Dockerfile
COPY *.db /app/

 ENTRYPOINT ["dotnet", "PruebaAKS.dll"]
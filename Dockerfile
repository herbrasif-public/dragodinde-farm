# Utiliser l'image de base du SDK .NET 9 pour la phase de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copier le fichier .csproj et restaurer les dépendances
COPY ["DragoDinde-MudBlazor/DragoDinde-MudBlazor.csproj", "DragoDinde-MudBlazor/"]
RUN dotnet restore "DragoDinde-MudBlazor/DragoDinde-MudBlazor.csproj"

# Copier le reste des fichiers et construire l'application
COPY . .
WORKDIR "/src/DragoDinde-MudBlazor"
RUN dotnet build "DragoDinde-MudBlazor.csproj" -c Release -o /app/build

# Publier l'application
RUN dotnet publish "DragoDinde-MudBlazor.csproj" -c Release -o /app/publish

# Utiliser l'image de base du runtime .NET 9 pour la phase de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
LS
ENTRYPOINT ["dotnet", "DragoDinde-MudBlazor.dll"]

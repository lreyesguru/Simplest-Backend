#!/bin/bash

# Variables
OLD_NAME="Simplest.Backend.API.Api"
NEW_NAME="Simplest.Backend.API"
OLD_CSPROJ="$OLD_NAME.csproj"
NEW_CSPROJ="$NEW_NAME.csproj"

echo "Renombrando carpeta del proyecto..."
mv "src/$OLD_NAME" "src/$NEW_NAME"

echo "Renombrando archivo .csproj..."
mv "src/$NEW_NAME/$OLD_CSPROJ" "src/$NEW_NAME/$NEW_CSPROJ"

echo "Reemplazando namespaces en archivos .cs..."
find "src/$NEW_NAME" -type f -name "*.cs" -exec sed -i "" "s/namespace $OLD_NAME/namespace $NEW_NAME/g" {} +

echo "Reemplazando nombre del archivo .csproj dentro del proyecto..."
sed -i "" "s/<RootNamespace>$OLD_NAME<\/RootNamespace>/<RootNamespace>$NEW_NAME<\/RootNamespace>/g" "src/$NEW_NAME/$NEW_CSPROJ"

echo "Actualizando referencias en la solución (.sln)..."
sed -i "" "s|$OLD_NAME/$OLD_CSPROJ|$NEW_NAME/$NEW_CSPROJ|g" Simplest.Backend.API.sln

echo "Actualizando referencias en los ProjectReference de otros .csproj..."
find src -type f -name "*.csproj" -exec sed -i "" "s|$OLD_NAME/$OLD_CSPROJ|$NEW_NAME/$NEW_CSPROJ|g" {} +

echo "Listo! Limpia y recompila la solución para asegurarte de que todo está bien."
echo "Ejecuta:"
echo "  dotnet clean"
echo "  dotnet build"

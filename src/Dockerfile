FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App
EXPOSE 443
EXPOSE 8080

COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
USER $APP_UID
COPY --from=build /App/out .
ENTRYPOINT ["dotnet", "Api.dll"]
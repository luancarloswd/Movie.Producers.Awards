FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

COPY ./src/ ./
RUN dotnet publish Movie.Producers.Awards.Api/Movie.Producers.Awards.Api.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Movie.Producers.Awards.Api.dll"]
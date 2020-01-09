FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster-arm64v8 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster-arm32v7
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "PlanetAPI.dll" ]
EXPOSE 6003
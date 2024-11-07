FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App
ARG BUILD_NUMBER

# Copy everything
COPY ./OdevTakip/ ./OdevTakip/
COPY ./OdevTakip.Entities/ ./OdevTakip.Entities/
COPY ./OdevTakip.Services/ ./OdevTakip.Services/

# Build and publish a release
RUN dotnet publish ./OdevTakip/OdevTakip.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .

ENTRYPOINT ["dotnet", "OdevTakip.dll"]


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /App
ARG BUILD_NUMBER

# Copy everything
COPY ./WorkFollow.Web/ ./WorkFollow.Web/
COPY ./WorkFollow.Entities/ ./WorkFollow.Entities/
COPY ./WorkFollow.Services/ ./WorkFollow.Services/

# Build and publish a release
RUN dotnet publish ./WorkFollow.Web/WorkFollow.Web.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /App
COPY --from=build-env /App/out .

ENTRYPOINT ["dotnet", "WorkFollow.Web.dll"]


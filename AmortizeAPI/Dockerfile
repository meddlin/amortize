# # FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
# FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env
# WORKDIR /app

# # Copy csproj and restore as distinct layers
# COPY *.csproj ./
# RUN dotnet restore

# # Copy everything else and build
# COPY . .
# RUN dotnet publish -c Release -o out

# # Build runtime image
# # FROM mcr.microsoft.com/dotnet/aspnet:5.0
# FROM mcr.microsoft.com/dotnet/aspnet:3.1
# WORKDIR /app
# COPY --from=build-env /app/out .

# # Run the app on container startup
# # Use your project name for the second parameter
# # e.g. MyProject.dll
# # ENTRYPOINT [ "dotnet", "AmortizeAPI.dll" ]
# CMD ASPNETCORE_URLS=http://*:$PORT dotnet AmortizeAPI.dll


# ##
# # Ref: https://hub.docker.com/_/microsoft-dotnet-sdk/
# # Ref: https://hub.docker.com/_/microsoft-dotnet-runtime/



FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["AmortizeAPI.csproj", "./"]
RUN dotnet restore "./AmortizeAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AmortizeAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AmortizeAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# The below is commented and the line after is used when in heroku
# ENTRYPOINT ["dotnet", "AmortizeAPI.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet AmortizeAPI.dll
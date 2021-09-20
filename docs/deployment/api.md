---
layout: page
title: Deployment - API
permalink: /deployment/api
---

## Create Dockerfile

Ref: [https://adevtalks.com/programming/deployment/deploy-net-core-3-1-web-api-to-heroku/](https://adevtalks.com/programming/deployment/deploy-net-core-3-1-web-api-to-heroku/)

`Dockerfile` for deploying the API (.NET Core 3.1.x)

Create this in the same directory as `AmortizeAPI`

```bash
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
```

> Note: The last line `CMD ASPNETCORE_URLS=http://*:$PORT dotnet AmortizeAPI.dll` is specifically
> for use with Heroku. If deploying elsewhere, you may need to comment this line and use the
> `ENTRYPOINT ["dotnet", "AmortizeAPI.dll"]`

## Push to Heroku

After creating the `Dockerfile`, we can now build the image (test locally if you want), and push it to Heroku.

- Create `Dockerfile` and `.dockerignore` in `/AmortizeAPI`
- Build image with: `docker build -t drushing/amortize-api`
- Push to Heroku with... (In Heroku app dashboard, check "Deploy" tab, select "Container Registry")
  - `heroku container:login`
  - `heroku container:push api --app=amortize-api`
  - `heroku container:release api --app=amortize-api`
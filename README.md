# Amortization Calculator

Basic amortization table calculator.

While looking for a simple amortization table online, I couldn't find one that included the right options for my situation. So, I built this one paying attention to include:

- Basic principal and interest
- Escrow items: home insurance, property taxes, & mortgage insurance
- Extra principal payments
- _Show the change in payments as PMI rolls off the loan_


## Tech Stack

- .NET Core 3.1
- React (`create-react-app`)
- Docker
- Heroku (_Note: Check the end of the API's Dockerfile for the only Heroku-specific configuration._)


## Deployment

_Since the API and client images are being pushed to different Heroku "apps", make sure they go to their own registries (`registry.heroku.com/<app>/<process-type>`, see Refs in API)._

### API

This article has the `Dockerfile` I needed: https://adevtalks.com/programming/deployment/deploy-net-core-3-1-web-api-to-heroku/

- Create `Dockerfile` and `.dockerignore` in `/AmortizeAPI`
- Build image with: `docker build -t drushing/amortize-api`
- Push to Heroku with... (In Heroku app dashboard, check "Deploy" tab, select "Container Registry")
  - `heroku container:login`
  - `heroku container:push api --app=amortize-api`
  - `heroku container:release api --app=amortize-api`

### Client

- Create `Dockerfile` and `.dockerignore` in `/amortize-client`
- Build image with: `docker build . -t drushingdev/amortize-cra`
- Push to Heroku with... (In Heroku app dashboard, check "Deploy" tab, select "Container Registry")
  - `heroku container:login`
  - `heroku container:push web`
  - `heroku container:release web`

#### Refs

Ref: https://adevtalks.com/programming/deployment/deploy-net-core-3-1-web-api-to-heroku/

Ref: https://medium.com/@shakyShane/lets-talk-about-docker-artifacts-27454560384f

This was a helpful starting point for getting a Docker container running. However, configuring nginx (like the author does) had to be left out for Heroku.



## Development Notes

### React Notes

React forms were built with this: https://www.digitalocean.com/community/tutorials/how-to-build-forms-in-react


### API/.NET Deployment Notes

These two links helped me get up to speed, but they ultimately didn't have all the pieces I needed.

- Ref: https://devcenter.heroku.com/articles/container-registry-and-runtime#pushing-an-existing-image
- Ref: https://dev.to/smiththe_4th/deploy-asp-net-core-with-mysql-to-heroku-44dp (This is for deploying .NET Core (.NET 5?) to Heroku w/o Docker)

Also, I never could get .NET 5.0.x working using all of these tutorials, so I had to switch to .NET Core 3.1.

Further, this line at the end of the `Dockerfile` for the API was crucial for Heroku, specifically.

```
CMD ASPNETCORE_URLS=http://*:$PORT dotnet AmortizeAPI.dll
```
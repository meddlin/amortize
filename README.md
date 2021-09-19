# Amortization Calculator

Does what it says...it isn't pretty, though.


## Deployment

_Since the API and client images are being pushed to different Heroku "apps", make sure they go to their own registries (`registry.heroku.com/<app>/<process-type>`, see Refs in API)._

### API

- Create `Dockerfile` and `.dockerignore` in `/AmortizeAPI`
- Build image with: `docker build -t drushing/amortize-api`
- Push to Heroku with... (In Heroku app dashboard, check "Deploy" tab, select "Container Registry")
  - `heroku container:login`
  - `heroku container:push api --app=amortize-api`
  - `heroku container:release api --app=amortize-api`

Ref: https://dev.to/smiththe_4th/deploy-asp-net-core-with-mysql-to-heroku-44dp

Ref: https://devcenter.heroku.com/articles/container-registry-and-runtime#pushing-an-existing-image

This is for deploying .NET Core (.NET 5?) to Heroku w/o Docker

### Client

- Create `Dockerfile` and `.dockerignore` in `/amortize-client`
- Build image with: `docker build . -t drushingdev/amortize-cra`
- Push to Heroku with... (In Heroku app dashboard, check "Deploy" tab, select "Container Registry")
  - `heroku container:login`
  - `heroku container:push web`
  - `heroku container:release web`

#### Refs

Ref: https://medium.com/@shakyShane/lets-talk-about-docker-artifacts-27454560384f

This was a helpful starting point for getting a Docker container running. However, configuring nginx (like the author does) had to be left out for Heroku.
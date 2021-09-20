---
layout: page
title: Deployment - Frontend
permalink: /deployment/frontend
filename: deployment-frontend.md
---

## Create Dockerfile

Ref: [https://medium.com/@shakyShane/lets-talk-about-docker-artifacts-27454560384f](https://medium.com/@shakyShane/lets-talk-about-docker-artifacts-27454560384f)

```bash
FROM node:10 as build-deps
WORKDIR /usr/src/app
COPY package.json yarn.lock ./
RUN yarn
COPY . ./
RUN yarn build
CMD ["npm", "start"]
```

> Note: In the article referenced above (See: Ref), there is a configuration
> for nginx. Heroku doesn't allow for this, so in deploying this project we
> leave that out entirely.



## (Optional) Run Container Locally

Build image: `docker build -t YourAppName .` (_Don't foget the period at the end!_)

Run container: `docker run -d -p 8080:80 --name abc YourAppName`

Ref: [https://dev.to/alrobilliard/deploying-net-core-to-heroku-1lfe](https://dev.to/alrobilliard/deploying-net-core-to-heroku-1lfe)



## Push to Heroku

After creating the `Dockerfile`, we can now build the image (test locally if you want), and push it to Heroku.

- Create `Dockerfile` and `.dockerignore` in `/amortize-client`
- Build image with: `docker build . -t drushingdev/amortize-cra`
- Push to Heroku with... (In Heroku app dashboard, check "Deploy" tab, select "Container Registry")
  - `heroku container:login`
  - `heroku container:push web`
  - `heroku container:release web`
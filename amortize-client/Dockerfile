# # Docker Image which is used as foundation to create
# # a custom Docker Image with this Dockerfile
# FROM node:10
 
# # A directory within the virtualized Docker environment
# # Becomes more relevant when using Docker Compose later
# WORKDIR /usr/src/app
 
# # Copies package.json and package-lock.json to Docker environment
# COPY package*.json ./
 
# # Installs all node packages
# RUN npm install
 
# # Copies everything over to Docker environment
# COPY . .
 
# # Uses port which is used by the actual application
# EXPOSE 3000
 
# # Finally runs the application
# CMD [ "npm", "start" ]

##
# Credit: https://medium.com/@shakyShane/lets-talk-about-docker-artifacts-27454560384f
##
FROM node:10 as build-deps
WORKDIR /usr/src/app
COPY package.json yarn.lock ./
RUN yarn
COPY . ./
RUN yarn build
CMD ["npm", "start"]

## 
# This was blocked by permissions on Heroku. Apparently, they are doing
# something where we don't have to configure our own web server?
##

# FROM nginx:1.12-alpine
# COPY --from=build-deps /usr/src/app/build /usr/share/nginx/html
# EXPOSE 80
# CMD ["nginx", "-g", "daemon off;"]
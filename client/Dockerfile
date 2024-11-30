# Stage 0, "build-stage", based on Node.js, to build and compile the frontend
FROM node:18-alpine as build-stage
WORKDIR /app
COPY . .
RUN npm ci
ARG configuration=production
RUN npm run build -- --configuration $configuration

# Stage 1, based on Nginx, to have only the compiled app, ready for production with Nginx
FROM nginx:latest
#Copy ci-dashboard-dist
COPY --from=build-stage /app/dist/client/browser /usr/share/nginx/html
#Copy default nginx configuration
COPY ./deployment_configs/nginx-custom.conf /etc/nginx/nginx.conf
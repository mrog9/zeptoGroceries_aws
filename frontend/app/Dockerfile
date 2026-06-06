FROM node:latest AS builder
WORKDIR /react
COPY react/package*.json .
RUN npm install
COPY react/. .
RUN npm run build

FROM nginx:alpine
COPY nginx/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=builder /react/dist/ /usr/share/nginx/html
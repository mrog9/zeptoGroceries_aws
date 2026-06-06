FROM node:latest
WORKDIR /react
COPY app/react/package*.json .
RUN npm install
COPY app/react/. .
RUN npm run build
FROM node:latest
WORKDIR /src
COPY src/package.json .
RUN npm install
COPY src/. .
CMD ["node","requestRouter.js"]
FROM node:latest
WORKDIR /apptests
COPY ./playwright/package*.json .
RUN npm install
RUN npx playwright install --with-deps

COPY ./playwright/tests .
CMD ["npx", "playwright", "test"]

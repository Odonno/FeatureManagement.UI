FROM node:13.12.0-alpine

WORKDIR /usr/app

ENV PATH /node_modules/.bin:$PATH

COPY package.json package-lock.json ./

RUN npm install --silent

COPY . ./

EXPOSE 8080

CMD npm run dev
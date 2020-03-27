FROM node as build-stage

WORKDIR /app
COPY . /app
RUN npm install
RUN npm run build -- --output-path=./dist/out

FROM nginx:alpine

COPY --from=build-stage /app/dist/out/ /usr/share/nginx/html
COPY --from=build-stage /app/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 8080
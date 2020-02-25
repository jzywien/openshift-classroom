FROM node:alpine as build
RUN apk update
USER 0
WORKDIR /opt/app-root
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build:prod

FROM nginx:alpine as final
RUN apk update && apk upgrade && apk add bash && apk add curl
RUN rm /etc/nginx/conf.d/default.conf
COPY config/frontend.conf /etc/nginx/conf.d
RUN rm -rf /usr/share/nginx/html/*
RUN chown -R 1001:0 /usr/share/nginx/html
USER 1001
COPY --from=build /opt/app-root/dist/client /usr/share/nginx/html
EXPOSE 8080
CMD ["nginx", "-g", "daemon off;"]
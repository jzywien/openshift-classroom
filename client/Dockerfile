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
RUN chmod g+rwx /var/cache/nginx /var/run /var/log/nginx
RUN rm /etc/nginx/conf.d/default.conf
COPY config/frontend.conf /etc/nginx/conf.d
EXPOSE 8080
RUN sed -i.bak 's/^user/#user/' /etc/nginx/nginx.conf
RUN rm -rf /usr/share/nginx/html/*
COPY --from=build /opt/app-root/dist/client /usr/share/nginx/html
CMD ["nginx", "-g", "daemon off;"]
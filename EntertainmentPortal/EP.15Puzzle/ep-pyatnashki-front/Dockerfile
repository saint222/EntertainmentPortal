FROM nginx:alpine

EXPOSE 80

COPY nginx.conf /etc/nginx/nginx.conf

WORKDIR /usr/share/nginx/html

COPY ./dist/ep-pyatnashki-front/ .

FROM mcr.microsoft.com/mssql/server:latest

USER root

RUN apt-get -y update  && \
    apt-get install -y dos2unix

WORKDIR /usr/config

COPY . /usr/config

RUN dos2unix *

RUN chmod +x /usr/config/entrypoint.sh
RUN chmod +x /usr/config/configure-db.sh

USER mssql

CMD /bin/bash ./entrypoint.sh
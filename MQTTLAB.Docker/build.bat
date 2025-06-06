@echo off
chcp 65001 >nul
set "BASEDIR=%~dp0"
docker run -d -p 1883:1883 -p 9001:9001 -v %BASEDIR%\mosquitto.conf:/mosquitto/config/mosquitto.conf --name mosquitto eclipse-mosquitto
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
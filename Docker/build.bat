@echo off
chcp 65001 >nul
set "BASEDIR=%~dp0"
docker run -d -p 1883:1883 -p 9001:9001 -v %BASEDIR%\mosquitto.conf:/mosquitto/config/mosquitto.conf --name mosquitto eclipse-mosquitto

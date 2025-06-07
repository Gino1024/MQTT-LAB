#!/bin/bash
BASEDIR="$(cd "$(dirname "$0")" && pwd)"
# 啟動 Mosquitto MQTT Broker
docker run -d \
  -p 1883:1883 \
  -p 9001:9001 \
  -v "$BASEDIR/mosquitto.conf":/mosquitto/config/mosquitto.conf \
  --name mosquitto \
  eclipse-mosquitto

# 啟動 RabbitMQ（包含 Management UI）
docker run -d \
  --name rabbitmq \
  -p 5672:5672 \
  -p 15672:15672 \
  rabbitmq:3-management

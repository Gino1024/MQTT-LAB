# Makefile
NAME ?= 
PROJECT_PATH=./MQTTLAB.Share.Infrastructure/MQTTLAB.Share.Infrastructure.csproj
STARTUP_PATH=./MQTTLAB.SensorAPI/MQTTLAB.SensorAPI.csproj
CONTEXT=MQTTLABDbContext
OUTPUT_DIR=./Database/Migrations

## 建立 Migration (需輸入名稱)
migrate:
	dotnet ef migrations add $(NAME) \
		--project $(PROJECT_PATH) \
		--startup-project $(STARTUP_PATH) \
		--context $(CONTEXT) \
		--output-dir $(OUTPUT_DIR)

## 更新資料庫
update:
	dotnet ef database update \
		--project $(PROJECT_PATH) \
		--startup-project $(STARTUP_PATH) \
		--context $(CONTEXT)

.PHONY: migrate update
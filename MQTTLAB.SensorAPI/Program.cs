using MQTTLAB.gRPC.Controller.Services;
using Infrastructrue.Database;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDBService(builder.Configuration, ServiceLifetime.Scoped);
builder.Services.AddGrpcService(builder.Configuration);

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<SensorGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

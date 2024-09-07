using Ordering.API;
using Ordering.Application;
using Ordering.Infastructure;

var builder = WebApplication.CreateBuilder(args);

//Services for container

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

//Request pipeline

app.UseApiServices();

app.Run();
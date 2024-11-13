using Ordering.API;
using Ordering.Application;
using Ordering.Infastructure;
using Ordering.Infastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Services for container

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

//Request pipeline

app.UseApiServices();

if (app.Environment.IsDevelopment())
    await app.InitialiseDatabaseAsync();

await app.RunAsync();
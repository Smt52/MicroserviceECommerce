using YarpApiGateway.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//Add services here

builder.Services.AddGatewayServices(builder.Configuration);

var app = builder.Build();

//Configure pipeline here

app.UseRateLimiter();

app.MapReverseProxy();

app.UseCors("AllowLocalhost");

await app.RunAsync();
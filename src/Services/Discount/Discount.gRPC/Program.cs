using Discount.gRPC.Data;
using Discount.gRPC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<DiscountContext>(options =>
    options.UseSqlite(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.

#region Commented for education purposes

//app.MapGrpcService<GreeterService>();
//app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

#endregion Commented for education purposes

app.UseMigration();
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Hello");
app.Run();
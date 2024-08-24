using CommonOperations.Behaviors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Add Services

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(CommonOperations.Behaviors.ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database"));
}).UseLightweightSessions();

var app = builder.Build();

//Configure HTTP requests
app.MapCarter();


// if (!app.Environment.IsDevelopment())
// {
    app.UseExceptionHandler(expHandler =>
    {
        expHandler.Run(async context =>
        {
            var exp = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (exp == null)
            {
                return;
            }

            var problemDetails = new ProblemDetails
            {
                Title = exp.Message,
                Status = StatusCodes.Status500InternalServerError,
                Detail = exp.StackTrace,
            };
            
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(exp,exp.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsJsonAsync(problemDetails);
        });
    });
// }

app.Run();
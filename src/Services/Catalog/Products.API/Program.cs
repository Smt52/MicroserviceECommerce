var builder = WebApplication.CreateBuilder(args);

//Add Services

var assembly = typeof(Program).Assembly;

//Everything related to MediatR must be defined here in the config. For the entire request response pipeline
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database"));
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure HTTP request pipeline.
app.MapCarter();


//Empty options parameter is pointing that we are relying on the custom exception handler
app.UseExceptionHandler(options => { });

await app.RunAsync();
using Serilog; // Add Serilog for logging as a thirdpart 
// serilog is a logging library that provides a simple way to log messages in .NET applications. 

var builder = WebApplication.CreateBuilder(args);
// Configure Serilog for logging
// Serilog is a logging library that provides a simple way to log messages in .NET applications.
long.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Write logs to the console
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Write logs to a file with daily rolling
    .CreateLogger();

// pipeline configuration
builder.Host.UseSerilog(); // Use Serilog for logging
builder.Services.AddControllers(); // Add controllers to the service collection
// Adding logging services
// This adds the logging services to the service collection, allowing you to use logging throughout your application.
builder.Logging.ClearProviders(); // Clear default logging providers
builder.Logging.AddConsole(); // Add console logging provider

// build the application
// The Build method creates the application instance based on the configuration and services defined in the builder.
var app = builder.Build();
app.UseHttpsRedirection();
// This runs everytime the application runs to handle Global error handling.
// It catches any unhandled exceptions that occur during the request processing pipeline.
// Adding Global Middleware for error handling
app.Use(async (context, next) =>
{
    try
    {
        await next(); // Call the next middleware in the pipeline
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Global exception caught: {ex.Message}");
        context.Response.StatusCode = 500; // Internal Server Error
        await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
    }
});

app.UseRouting(); // Enable routing
app.MapControllers(); // Map controllers to the request pipeline


app.Run();

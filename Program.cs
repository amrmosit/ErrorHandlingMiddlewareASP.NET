using Serilog; // Add Serilog for logging // serilog is a logging library that provides a simple way to log messages in .NET applications. 

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
builder.Logging.ClearProviders(); // Clear default logging providers
builder.Logging.AddConsole(); // Add console logging provider

// build the application
// The Build method creates the application instance based on the configuration and services defined in the builder.
var app = builder.Build();
app.UseHttpsRedirection();



app.Run();

using System.Reflection;
using System.Text.Json.Serialization;
using tusdotnet;
using tusdotnet.Helpers;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Add services to the container.
services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
        builder.AllowCredentials();
        builder.WithExposedHeaders(exposedHeaders: CorsHelper.GetExposedHeaders());
        builder.WithOrigins("http://localhost:4200", "https://localhost:4200");
    });
});
services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

services.AddAssetService(configuration.GetSection("VehicleService"), Assembly.GetExecutingAssembly());
services.AddFileService();

// Add Swagger UI
services.AddApiDocumentation();

// Build the app and configure the request pipeline
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseCors();

app.UseTus(app.Configuration);

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseApiDocumentation();
app.Run();
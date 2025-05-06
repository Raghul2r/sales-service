using Microsoft.EntityFrameworkCore;
using SalesAndService.Models;
using SalesAndService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Db connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("testCon"))
);

// CORS configuration
builder.Services.AddCors(option =>
{
    option.AddPolicy(name: "allowcors", builder =>
    {
        builder.WithOrigins("https://localhost:4200", "http://localhost:4200")
               .AllowCredentials()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


builder.Services.AddScoped<CsvLoader>();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    // Enable Swagger in development mode
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalesAndService API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

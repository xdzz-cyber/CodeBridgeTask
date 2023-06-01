using System.Reflection;
using Application;
using Application.Common.Mappings;
using Application.Interfaces;
using Persistence;
using WebApplication1.Middlewares.CustomExceptionHandler;
using WebApplication1.Middlewares.RequestRateLimit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestRateLimitMiddleware();
app.UseCustomExceptionHandlerMiddleware();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
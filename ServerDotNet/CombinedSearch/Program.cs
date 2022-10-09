using CombinedSearch.Middlewares;
using CombinedSearch.Startup;
using CombinedSearch.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterLoggerServices();

builder.Services.Configure<SearchEngineConfig>(builder.Configuration.GetSection("SearchEngineConfig"));

builder.Services.AddHttpClient();

builder.Services.RegisterApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(origin => true)
                            .AllowCredentials());

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

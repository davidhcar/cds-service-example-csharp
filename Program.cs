using Microsoft.Extensions.Logging;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.WebHost.UseNLog();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "WithCredentialsAnyOrigin",
        builder =>
        {
            builder.AllowCredentials()
                .WithOrigins("localhost")
                .AllowAnyHeader()
                .WithMethods("GET", "POST", "OPTIONS");
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();

app.Run();

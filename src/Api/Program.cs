using Infrastructure;
using Application;
using Api.Middlewares;

using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(
        opt =>
        {
            opt.AddPolicy("AllowAnyOrigin", policy => { policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
        });

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment, builder.Host);
builder.Services.AddApplicationServices();


builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "BuyNowPayLater", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement {{
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[]{}
    }});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

// app.UseMiddleware<JWTMiddleware>();

app.UseAuthorization();

app.UseCors("AllowAnyOrigin");

// app.UseMiddleware<RequestLogContextMiddleware>();

// app.UseSerilogRequestLogging();

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();

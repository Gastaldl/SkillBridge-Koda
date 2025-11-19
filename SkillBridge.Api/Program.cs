using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SkillBridge.Application.Services;
using SkillBridge.Domain.Interfaces;
using SkillBridge.Infrastructure.Data;
using SkillBridge.Infrastructure.Repositories;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do Banco de Dados (Oracle)
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString)
);

// 2. Injeção de Dependência (DI)
builder.Services.AddScoped<ITrilhaRepository, TrilhaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITrilhaService, TrilhaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// 3. Configuração da API, Versionamento e Swagger
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

apiVersioningBuilder.AddMvc()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SkillBridge API",
        Version = "v1",
        Description = "API para plataforma de Upskilling/Reskilling."
    });
});

var app = builder.Build();

// 4. Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
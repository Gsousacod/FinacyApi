
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FinacyApi.Data;
using System.Text;
using FinacyApi.Services;
using Microsoft.OpenApi.Models;
using Serilog;
using FinacyApi.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
{
    config
        .Enrich.FromLogContext() // Adiciona contexto aos logs
        .WriteTo.File("logs.txt", 
            rollingInterval: RollingInterval.Day, // Gera um novo arquivo por dia
            retainedFileCountLimit: 7, // Mantém logs dos últimos 7 dias
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj} {NewLine}{Exception}"); 
});

builder.Services.AddSingleton<LoggingService>(); 
builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FinancyDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira seu token JWT no campo abaixo (sem 'Bearer ')"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// Configurar autenticação JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Secret"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidateAudience = false,
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            ValidateLifetime = true
        };
    });
builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
        options.AddPolicy("ClientOnly", policy => policy.RequireRole("Client"));
    });

builder.Services.AddSingleton<TokensService>(); // Adicionar TokenService
builder.Services.AddScoped<IGenerateReport, GenerateReport>();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

using ImoveisConnect.API.Core.Extensions;
using ImoveisConnect.Application;
using ImoveisConnect.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inje��o dos servi�os de dom�nio
builder.Services.UseDomainServices();

// Configura��o dos Controllers
builder.Services.AddControllers();

// 4. Configura��o do Swagger com autentica��o JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ImoveisConnect API",
        Version = "v1",
        Description = "API para o sistema ImoveisConnect"
    });

    // Configura��o do esquema de seguran�a JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Insira o token JWT no formato: Bearer {token}"
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


// Configura��o do Swagger/OpenAPI
//builder.Services.AddOpenApi();

// Configura��o de autentica��o JWT usando a extens�o personalizada
var jwtSecret = builder.Configuration["SecurityConfig:Secret"];
builder.Services.UseAuthentication(jwtSecret);

// Adicione ANTES de builder.Build()
builder.Services.Configure<ApplicationConfig>(builder.Configuration.GetSection("ApplicationConfig"));



var app = builder.Build();

// Aplicar migrations e seed do banco de dados
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        // Aplicar migrations automaticamente
        context.Database.Migrate();

        await DbInitializer.Initialize(context, services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao inicializar o banco de dados");
    }
}

// Configura��o do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();



    app.UseSwagger(c =>
    {
        c.RouteTemplate = "openapi/{documentName}.json"; // Isso gera /openapi/v1.json
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "ImoveisConnectAPI v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

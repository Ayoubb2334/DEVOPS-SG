using API.Middleware;
using Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;

var builder = WebApplication.CreateBuilder(args);

// --- Services ---

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Persistance (PostgreSQL via variables d'environnement PG_HOST, PG_DATABASE, PG_USERNAME, PG_PASSWORD)
builder.Services.AddPersistance(builder.Configuration);

// MediatR (CQRS) - scanne l'assembly Application pour trouver Commands/Queries/Handlers
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Application.Features.Smartphones.Commands.CreateSmartphone.CreateSmartphoneCommand).Assembly));

// Pipeline de validation automatique (FluentValidation) avant chaque Command/Query
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddValidatorsFromAssembly(typeof(Application.Features.Smartphones.Commands.CreateSmartphone.CreateSmartphoneCommand).Assembly);

// AutoMapper
builder.Services.AddAutoMapper(typeof(Application.Mappings.MappingProfile));

// CORS - autorise le frontend Angular à appeler l'API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// --- Migrations automatiques au démarrage ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// --- Pipeline HTTP ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
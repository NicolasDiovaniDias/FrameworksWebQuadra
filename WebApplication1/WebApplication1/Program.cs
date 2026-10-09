using Microsoft.EntityFrameworkCore;
using WebApplication1.Api.Data;
using WebApplication1.Api.Dtos;
using WebApplication1.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var db = new AppDbContext())
{
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/api/quadras", (Quadra quadra) =>
{
    using var db = new AppDbContext();
    db.Quadras.Add(quadra);
    db.SaveChanges();
    return Results.Created($"/api/quadras/{quadra.Id}", quadra);
});

app.MapGet("/api/quadras", () =>
{
    using var db = new AppDbContext();
    return db.Quadras
        .Select(q => new {
            q.Id,
            q.Nome,
            Reservas = q.Reservas.Select(r => new {
                r.Id,
                r.Inicio,
                r.Fim
            }).ToList()
        })
        .ToList();
});

app.MapPost("/api/reserva", (CriarReservaDto dto) =>
{
    using var db = new AppDbContext();

    var reserva = new Reserva(dto.Inicio, dto.Fim) { QuadraID = dto.QuadraId };

    db.Reserva.Add(reserva);
    db.SaveChanges();

    return Results.Created($"/api/reserva/{reserva.Id}", reserva);
});

app.MapGet("/api/reserva", () =>
{
    using var db = new AppDbContext();
    return db.Reserva
        .Select(r => new {
            r.Id,
            r.Inicio,
            r.Fim,
            Quadra = new { r.Quadra!.Id, r.Quadra.Nome }
        })
        .ToList();
});

app.Run();
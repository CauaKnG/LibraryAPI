using LibraryAPI.Application.Services;
using LibraryAPI.Domain.Interfaces;
using LibraryAPI.Domain.Repositories.Interfaces;
using LibraryAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILivroRepository, LivroRepository>(); 
builder.Services.AddScoped<LivroService>();
builder.Services.AddScoped<IEmprestimoService, EmprestimoService>();
builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

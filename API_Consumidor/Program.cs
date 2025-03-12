using API_Consumidor.Consumidor;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Consumidor_Atualizar>();
builder.Services.AddSingleton<Consumidor_Deletar>();
builder.Services.AddSingleton<Consumidor_Publica>();

var app = builder.Build();
new Consumidor_Atualizar(app.Configuration).Consumir_Atualizar();
new Consumidor_Deletar(app.Configuration).Consumir_Deletar();
new Consumidor_Publica(app.Configuration).Consumir_Publicar();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

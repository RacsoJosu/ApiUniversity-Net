// 1 .using para usar entity framework 
using Microsoft.EntityFrameworkCore;
using ApiRestfull.DataAcces;

var builder = WebApplication.CreateBuilder(args);
// 2. conexion con la base de datos
const string CONNECTIONAME= "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONAME);


// 3. contexto de la app
builder.Services.AddDbContext<UniversityContext>(context => context.UseSqlServer(connectionString));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();

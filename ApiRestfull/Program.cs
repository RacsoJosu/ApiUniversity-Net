// 1 .using para usar entity framework 
using Microsoft.EntityFrameworkCore;
using ApiRestfull.DataAcces;
using ApiRestfull.Services;
using ApiRestfull;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// 2. conexion con la base de datos
const string CONNECTIONAME= "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONAME);


// 3. contexto de la app
builder.Services.AddDbContext<UniversityContext>(context => context.UseSqlServer(connectionString));

// 7.agregar el token 

builder.Services.AddJwtTokenServices(builder.Configuration);

// 4. Add services to the container.


builder.Services.AddControllers();
// para poder inyectar los servicios en nuestros controller 
builder.Services.AddScoped<IStudentService, StudentService>();

// 5. Habilitar el CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

// 8. añadir autorizacion al proyecto 

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// 9. añadir aurizacion a swagger 
// agregar el token
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Autorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme"

    }) ;

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            } , 
            new string[]{}
        }
        
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
// que la api haga uso de cors
app.UseCors("CorsPolicy");


app.Run();

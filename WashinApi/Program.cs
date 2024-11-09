using WashinApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using WashinApi;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore.InMemory;


var builder = WebApplication.CreateBuilder(args);

// Ajouter le service DbContext en mémoire
builder.Services.AddDbContext<WashinContext>(options =>
    options.UseInMemoryDatabase("WashinDb") // Utilisation d'une base de données en mémoire
);



// Ajouter les services de l'application (contrôleurs, etc.)
builder.Services.AddControllers();

// Ajouter Swagger pour la documentation d'API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

SeedData.Initialize(app.Services);
app.MapControllers();
// Activer Swagger si l'environnement est le développement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Utiliser les contrôleurs (les routes de l'API)


// Démarrer l'application
app.Run();





//// Add services to the container.
////builder.Services.AddDbContext<WashinContext>(options =>
////    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
////        new MySqlServerVersion(new Version(8, 0, 39))));

//builder.Services.AddSingleton<DB>();

//// Configure CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSwagger", builder =>
//    {
//        builder.AllowAnyOrigin() // Utilisez .WithOrigins("http://localhost:<port>") pour restreindre l'origine si nécessaire
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();


//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

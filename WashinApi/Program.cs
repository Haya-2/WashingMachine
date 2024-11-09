using WashinApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using WashinApi;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore.InMemory;


var builder = WebApplication.CreateBuilder(args);

// Ajouter le service WashinContext en singleton
builder.Services.AddSingleton<WashinContext>(serviceProvider =>
{
    // Créer une instance de WashinContext (qui charge les données JSON)
    var context = new WashinContext();
    context.LoadData(); // Charger les données depuis les fichiers JSON au démarrage
    return context;
});

// Ajouter les services de l'application (contrôleurs, etc.)
builder.Services.AddControllers();

// Ajouter Swagger pour la documentation d'API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Mapper les contrôleurs
app.MapControllers();

// Activer Swagger si l'environnement est le développement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
//        builder.AllowAnyOrigin() // Utilisez .WithOrigins("http://localhost:<port>") pour restreindre l'origine si n�cessaire
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

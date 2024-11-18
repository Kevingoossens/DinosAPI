using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;

// Création du constructeur WebApplication pour configurer les services de l'application
var builder = WebApplication.CreateBuilder(args);

// Configuration d'Entity Framework et d'ASP.NET Core Identity
builder.Services.AddDbContext<DinosaurContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// Configurer Entity Framework pour utiliser SQLite en récupérant la chaîne de connexion depuis appsettings.json

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DinosaurContext>()
    .AddDefaultTokenProviders();
// Configurer les services d'ASP.NET Core Identity pour gérer les utilisateurs et rôles

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login"; // Définir l'URL de connexion
    options.LogoutPath = "/logout"; // Définir l'URL de déconnexion
});
// Configurer les chemins de connexion et de déconnexion pour les cookies d'authentification

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
// Ajouter et configurer les services d'authentification avec des cookies

builder.Services.AddAuthorization(); // Ajoute les services d'autorisation
// Ajouter les services nécessaires pour l'autorisation des utilisateurs

builder.Services.AddControllers();  // Ajouter les contrôleurs pour gérer les requêtes HTTP
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Ajouter les services pour la documentation Swagger, utile pour tester et explorer les API

var app = builder.Build(); // Construire l'application

// Démarre Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Environnement de développement : activer Swagger pour explorer et tester les API

app.UseHttpsRedirection();
// Rediriger les requêtes HTTP vers HTTPS

app.UseAuthentication(); // Important : ajoute l'authentification
app.UseAuthorization();
// Ajouter les middlewares pour l'authentification et l'autorisation

app.MapControllers(); // Utilise les contrôleurs pour gérer les routes HTTP

// Route GET pour obtenir tous les dinosaures
app.MapGet("Get all dinosaurs", async (DinosaurContext db) =>
{
    return await db.Dinosaurs.ToListAsync();
});

// Route GET pour obtenir un dinosaure spécifique par ID
app.MapGet("Search dinosaurs by {id}", async (int id, DinosaurContext db) =>
{
    var dinosaur = await db.Dinosaurs.FindAsync(id);
    return dinosaur is not null ? Results.Ok(dinosaur) : Results.NotFound();
});

// GET by Name (insensible à la casse) pour obtenir un dinosaure spécifique par nom
app.MapGet("Search dinosaurs by name", async (string name, DinosaurContext db) =>
{
    var dinosaurs = await db.Dinosaurs
        .Where(d => d.Name.ToLower().Contains(name.ToLower()))
        .ToListAsync();
    return dinosaurs.Any() ? Results.Ok(dinosaurs) : Results.NotFound();
});

// GET by Period (insensible à la casse) pour obtenir un dinosaure spécifique par période
app.MapGet("Search dinosaurs by period", async (string period, DinosaurContext db) =>
{
    var dinosaurs = await db.Dinosaurs
        .Where(d => d.Period.ToLower().Contains(period.ToLower()))
        .ToListAsync();
    return dinosaurs.Any() ? Results.Ok(dinosaurs) : Results.NotFound();
});

// GET by Feed (insensible à la casse) pour obtenir un dinosaure spécifique par type d'alimentation
app.MapGet("Search Dinosaurs by feed", async (string feed, DinosaurContext db) =>
{
    var dinosaurs = await db.Dinosaurs
        .Where(d => d.Feed.ToLower().Contains(feed.ToLower()))
        .ToListAsync();
    return dinosaurs.Any() ? Results.Ok(dinosaurs) : Results.NotFound();
});

// Route POST pour ajouter un nouveau dinosaure (protéger par authentification)
app.MapPost("Post new dinosaurs", async (Dinosaur dinosaur, DinosaurContext db, HttpContext context) =>
{
    // Vérification si l'utilisateur est authentifié
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
    if (!context.User.Identity.IsAuthenticated)
    {
        return Results.Unauthorized(); // 401 Unauthorized
    }
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.

    // Ajoute le dinosaure à la base de données
    db.Dinosaurs.Add(dinosaur);
    await db.SaveChangesAsync();
    
    // Retourne une réponse HTTP 201 "Created" avec l'URL du nouvel objet et les détails du dinosaure
    return Results.Created($"/dinosaurs/{dinosaur.Id}", dinosaur);
});

// Route PUT pour mettre à jour un dinosaure existant (protéger par authentification)
app.MapPut("Modify dinosaurs by {id}", async (int id, Dinosaur inputDinosaur, DinosaurContext db, HttpContext context) =>
{

// Vérification si l'utilisateur est authentifié
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
    if (!context.User.Identity.IsAuthenticated)
    {
        return Results.Unauthorized(); // 401 Unauthorized
    }
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.

    // Recherche du dinosaure par ID
    var dinosaur = await db.Dinosaurs.FindAsync(id);
    
    // Si le dinosaure n'est pas trouvé, retourner une 404
    if (dinosaur is null)
    {
        return Results.NotFound("Dinosaur not found."); // 404 Not Found
    }

    dinosaur.Name = inputDinosaur.Name;
    dinosaur.Period = inputDinosaur.Period;
    dinosaur.Feed = inputDinosaur.Feed;
    dinosaur.Height = inputDinosaur.Height;
    dinosaur.Length = inputDinosaur.Length;
    dinosaur.Weight = inputDinosaur.Weight;
    dinosaur.Location = inputDinosaur.Location;

    await db.SaveChangesAsync();
    
    // Retourner un code 204 "OK" pour indiquer que la modification a été effectuée avec succès
    return Results.Ok(dinosaur);
});

// Route DELETE pour supprimer un dinosaure par ID (protéger par authentification)
app.MapDelete("Delete dinosaurs by {id}", async (int id, DinosaurContext db, HttpContext context) =>
{

// Vérification si l'utilisateur est authentifié
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
    if (!context.User.Identity.IsAuthenticated)
    {
        return Results.Unauthorized(); // 401 Unauthorized
    }
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.

    // Recherche du dinosaure par ID
    var dinosaur = await db.Dinosaurs.FindAsync(id);
    
    // Si le dinosaure n'est pas trouvé, retourner une 404
    if (dinosaur is null)
    {
        return Results.NotFound("Dinosaur not found."); // 404 Not Found
    }

    // Suppression du dinosaure
    db.Dinosaurs.Remove(dinosaur);
    await db.SaveChangesAsync();
    
    // Retourner un code 204 "No Content" pour indiquer que la suppression a été effectuée avec succès
    return Results.NoContent(); 
});

app.Run(); // Exécuter l'application
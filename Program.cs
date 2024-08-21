using Microsoft.EntityFrameworkCore; // Assure-toi que cet espace de noms est présent

var builder = WebApplication.CreateBuilder(args);

// Ajouter le contexte de la base de données avec SQLite
builder.Services.AddDbContext<DinosaurContext>(options =>
    options.UseSqlite("Data Source=dinosapi.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Route GET pour obtenir tous les dinosaures
app.MapGet("/dinosaurs", async (DinosaurContext db) =>
{
    // Utilise le contexte de la base de données pour obtenir tous les dinosaures.
    // ToListAsync() récupère tous les dinosaures de la table 'Dinosaurs' et les convertit en liste.
    return await db.Dinosaurs.ToListAsync();
});

// Route GET pour obtenir un dinosaure spécifique par ID
app.MapGet("/dinosaurs/{id}", async (int id, DinosaurContext db) =>
{
    // Recherche un dinosaure par ID dans la base de données.
    // FindAsync(id) trouve le dinosaure avec l'ID spécifié.
    var dinosaur = await db.Dinosaurs.FindAsync(id);
    // Vérifie si le dinosaure est trouvé ou non.
    // Si trouvé, retourne une réponse HTTP 200 (OK) avec les détails du dinosaure.
    // Sinon, retourne une réponse HTTP 404 (Not Found).
    return dinosaur is not null ? Results.Ok(dinosaur) : Results.NotFound();
});

// Route POST pour ajouter un nouveau dinosaure
app.MapPost("/dinosaurs", async (Dinosaur dinosaur, DinosaurContext db) =>
{
    // Ajoute le dinosaure reçu dans la base de données.
    db.Dinosaurs.Add(dinosaur);
    // Sauvegarde les changements dans la base de données.
    await db.SaveChangesAsync();
    // Retourne une réponse HTTP 201 (Created) avec l'URL du nouvel objet et les détails du dinosaure.
    return Results.Created($"/dinosaurs/{dinosaur.Id}", dinosaur);
});

// Route PUT pour mettre à jour un dinosaure existant
app.MapPut("/dinosaurs/{id}", async (int id, Dinosaur inputDinosaur, DinosaurContext db) =>
{
    // Recherche un dinosaure par ID dans la base de données.
    var dinosaur = await db.Dinosaurs.FindAsync(id);
    // Vérifie si le dinosaure existe.
    // Si le dinosaure n'existe pas, retourne une réponse HTTP 404 (Not Found).
    if (dinosaur is null) return Results.NotFound();

    // Met à jour les propriétés du dinosaure avec les nouvelles valeurs fournies.
    dinosaur.Name = inputDinosaur.Name;
    dinosaur.Period = inputDinosaur.Period;
    dinosaur.Feed = inputDinosaur.Feed;
    dinosaur.Height = inputDinosaur.Height;
    dinosaur.Length = inputDinosaur.Length;
    dinosaur.Weight = inputDinosaur.Weight;
    dinosaur.Location = inputDinosaur.Location;

    // Sauvegarde les changements dans la base de données.
    await db.SaveChangesAsync();
    // Retourne une réponse HTTP 200 (OK) avec les détails du dinosaure mis à jour.
    return Results.Ok(dinosaur);
});

// Route DELETE pour supprimer un dinosaure par ID
app.MapDelete("/dinosaurs/{id}", async (int id, DinosaurContext db) =>
{
    // Recherche un dinosaure par ID dans la base de données.
    var dinosaur = await db.Dinosaurs.FindAsync(id);
    // Vérifie si le dinosaure existe.
    // Si le dinosaure n'existe pas, retourne une réponse HTTP 404 (Not Found).
    if (dinosaur is null) return Results.NotFound();

    // Supprime le dinosaure de la base de données.
    db.Dinosaurs.Remove(dinosaur);
    // Sauvegarde les changements dans la base de données.
    await db.SaveChangesAsync();
    // Retourne une réponse HTTP 204 (No Content) indiquant que la suppression a été effectuée avec succès.
    return Results.NoContent();
});

app.Run();
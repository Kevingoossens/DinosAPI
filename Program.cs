var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

// Liste de dinosaures avec leurs caractéristiques
var dinosaures = new[]
{
    new Dinosaur("T-rex", "Crétacé", "Carnivore", "4m", "14m", "7t", "North America"),
    new Dinosaur("Vélociraptor", "Crétacé", "Carnivore", "2m", "3m", "136kg", "USA, Canada, Mongolie"),
    new Dinosaur("Brachiosaurus", "Jurassique", "Herbivore", "20m", "30m", "60t", "North America, Portugal, Tanzani"),
    new Dinosaur("Dilophosaurus", "Jurassique", "Carnivore", "2m", "6m", "1t", "Arizona"),
    new Dinosaur("Gallimimus", "Crétacé", "Herbivore", "2m", "8m", "440kg", "Mongolie"),
    new Dinosaur("Triceratops", "Crétacé", "Herbivore", "3m", "10m", "10t", "North America"),
    new Dinosaur("Parasaurolophus", "Crétacé", "Herbivore", "4m", "10m", "5t", "North America"),
    new Dinosaur("Compsognathus", "Jurassique", "Carnivore", "30cm", "1m", "3,5kg", "Europe"),
    new Dinosaur("Stegosaurus", "Jurassique", "Herbivore", "4m", "9m", "4t", "Morrison Formation"),
    new Dinosaur("Spinosaurus", "Crétacé", "Carnivore", "7m", "15m", "10t", "North Africa"),
    new Dinosaur("Mosasaurus", "Crétacé", "Carnivore", "20m", "2m", "10t", "Sea of Europe, North America, Africa, Australia, New-Zealand"),
    new Dinosaur("Baryonyx", "Crétacé", "Carnivore", "2,5m", "9,5m", "2,5t", "England, Spain")  
};

// Intégration du paramètres de recherche "Get" avec recherche insensible à la casse
app.MapGet("/Dinosaures/search", (string? name, string? period, string? feed) =>
{
    var filteredDinos = dinosaures.AsEnumerable();

    if (!string.IsNullOrWhiteSpace(name))
    {
        filteredDinos = filteredDinos.Where(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    if (!string.IsNullOrWhiteSpace(period))
    {
        filteredDinos = filteredDinos.Where(d => d.Period.Equals(period, StringComparison.OrdinalIgnoreCase));
    }

    if (!string.IsNullOrWhiteSpace(feed))
    {
        filteredDinos = filteredDinos.Where(d => d.Feed.Equals(feed, StringComparison.OrdinalIgnoreCase));
    }

    return filteredDinos.Any() ? Results.Ok(filteredDinos) : Results.NotFound(new { Message = "No dinosaurs found with the given criteria" });
})

// Affiche les listes voulue pour facilité la recherhce
.WithName("SearchDinosaures")
.WithOpenApi();

app.MapGet("/Dinosaures/names", () =>
{
    var names = dinosaures.Select(d => d.Name).Distinct().ToList();
    return Results.Ok(names);
})
.WithName("GetDinosaurNames")
.WithOpenApi();

app.MapGet("/Dinosaures/periods", () =>
{
    var periods = dinosaures.Select(d => d.Period).Distinct().ToList();
    return Results.Ok(periods);
})
.WithName("GetDinosaurPeriods")
.WithOpenApi();

app.MapGet("/Dinosaures/feeds", () =>
{
    var feeds = dinosaures.Select(d => d.Feed).Distinct().ToList();
    return Results.Ok(feeds);
})
.WithName("GetDinosaurFeeds")
.WithOpenApi();

app.Run();
record Dinosaur(string Name, string Period, string Feed, string Height, string Length, string Weight, string Location);
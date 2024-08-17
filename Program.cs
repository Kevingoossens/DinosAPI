var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

// Liste de dinosaures avec leurs caractéristiques
var dinosaures = new[]
{
    new Dinosaur("T-rex", "Crétacé", "Carnivore", "4m", "14m", "7t", "North America"),
    new Dinosaur("Vélociraptor", "Crétacé", "Carnivore", "2m", "3m", "136kg", "USA, Canada, Mongolie"),
    new Dinosaur("Brachiosaures", "Jurassique", "Herbivore", "20m", "30m", "60t", "North America, Portugal, Tanzani"),
    new Dinosaur("Dilophosaure", "Jurassique", "Carnivore", "2m", "6m", "1t", "Arizona"),
    new Dinosaur("Gallimimus", "Crétacé", "Herbivore", "2m", "8m", "440kg", "Mongolie"),
    new Dinosaur("Triceratops", "Crétacé", "Herbivore", "3m", "10m", "10t", "North America"),
    new Dinosaur("Parasaurolophus", "Crétacé", "Herbivore", "4m", "10m", "5t", "North America"),
    new Dinosaur("Compsognathus", "Jurassique", "Carnivore", "30cm", "1m", "3,5kg", "Europe"),
    new Dinosaur("Stegosaurus", "Jurassique", "Herbivore", "4m", "9m", "4t", "Morrison Formation")
    // new Dinosaur("")
};

app.MapGet("/Dinosaures", () =>
{
    return dinosaures;
})
.WithName("Dinosaures")
.WithOpenApi();

app.Run();

// Modèle Dinosaur avec des propriétés pertinentes
record Dinosaur(string Name, string Period, string Feed, string Height, string Length, string Weight, string Location );
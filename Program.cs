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
    new Dinosaur("T-rex", "Crétacé", "12m"),
    new Dinosaur("Vélociraptor", "Crétacé", "2m"),
    new Dinosaur("Brachiosaures", "Jurassique", "21m"),
    new Dinosaur("Mosasaure", "Crétacé", "25m")
};

app.MapGet("/Dinosaures", () =>
{
    return dinosaures;
})
.WithName("Dinosaures")
.WithOpenApi();

app.Run();

// Modèle Dinosaur avec des propriétés pertinentes
record Dinosaur(string Name, string Period, string Size);
using Microsoft.EntityFrameworkCore;

public class DinosaurContext : DbContext
{
    public DinosaurContext(DbContextOptions<DinosaurContext> options) : base(options) { }

    public DbSet<Dinosaur> Dinosaurs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ajouter des données initiales
        modelBuilder.Entity<Dinosaur>().HasData(
            new Dinosaur { Id = 1, Name = "T-rex", Period = "Crétacé", Feed = "Carnivore", Height = "4m", Length = "14m", Weight = "7t", Location = "North America" },
            new Dinosaur { Id = 2, Name = "Vélociraptor", Period = "Crétacé", Feed = "Carnivore", Height = "2m", Length = "3m", Weight = "136kg", Location = "USA, Canada, Mongolie" },
            new Dinosaur { Id = 3, Name = "Brachiosaurus", Period = "Jurassique", Feed = "Herbivore", Height = "20m", Length = "30m", Weight = "60t", Location = "North America, Portugal, Tanzanie" },
            new Dinosaur { Id = 4, Name = "Dilophosaurus", Period = "Jurassique", Feed = "Carnivore", Height = "2m", Length = "6m", Weight = "1t", Location = "Arizona" },
            new Dinosaur { Id = 5, Name = "Gallimimus", Period = "Crétacé", Feed = "Herbivore", Height = "2m", Length = "8m", Weight = "440kg", Location = "Mongolie" },
            new Dinosaur { Id = 6, Name = "Triceratops", Period = "Crétacé", Feed = "Herbivore", Height = "3m", Length = "10m", Weight = "10t", Location = "North America" },
            new Dinosaur { Id = 7, Name = "Parasaurolophus", Period = "Crétacé", Feed = "Herbivore", Height = "4m", Length = "10m", Weight = "5t", Location = "North America" },
            new Dinosaur { Id = 8, Name = "Compsognathus", Period = "Jurassique", Feed = "Carnivore", Height = "30cm", Length = "1m", Weight = "3,5kg", Location = "Europe" },
            new Dinosaur { Id = 9, Name = "Stegosaurus", Period = "Jurassique", Feed = "Herbivore", Height = "4m", Length = "9m", Weight = "4t", Location = "Morrison Formation" },
            new Dinosaur { Id = 10, Name = "Spinosaurus", Period = "Crétacé", Feed = "Carnivore", Height = "7m", Length = "15m", Weight = "10t", Location = "North Africa" },
            new Dinosaur { Id = 11, Name = "Mosasaurus", Period = "Crétacé", Feed = "Carnivore", Height = "20m", Length = "2m", Weight = "10t", Location = "Sea of Europe, North America, Africa, Australia, New-Zealand" },
            new Dinosaur { Id = 12, Name = "Baryonyx", Period = "Crétacé", Feed = "Carnivore", Height = "2,5m", Length = "9,5m", Weight = "2,5t", Location = "England, Spain" }
        );
    }
}
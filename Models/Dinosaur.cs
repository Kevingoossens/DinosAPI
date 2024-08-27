public class Dinosaur
{
    // Identifiant unique pour chaque dinosaure
    public int Id { get; set; }

    // Nom du dinosaure (ex : T-rex, Vélociraptor)
    public string Name { get; set; }

    // Période géologique où le dinosaure a vécu (ex : Crétacé, Jurassique)
    public string Period { get; set; }

    // Type d'alimentation du dinosaure (ex : Carnivore, Herbivore)
    public string Feed { get; set; }

    // Hauteur du dinosaure (ex : 4m, 20m)
    public string Height { get; set; }

    // Longueur du dinosaure (ex : 14m, 30m)
    public string Length { get; set; }

    // Poids du dinosaure (ex : 7t, 60t)
    public string Weight { get; set; }

    // Localisation où le dinosaure a été trouvé (ex : North America, Mongolia)
    public string Location { get; set; }
}
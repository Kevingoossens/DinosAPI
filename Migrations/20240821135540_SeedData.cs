using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DinosAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Dinosaurs",
                columns: new[] { "Id", "Feed", "Height", "Length", "Location", "Name", "Period", "Weight" },
                values: new object[,]
                {
                    { 1, "Carnivore", "4m", "14m", "North America", "T-rex", "Crétacé", "7t" },
                    { 2, "Carnivore", "2m", "3m", "USA, Canada, Mongolie", "Vélociraptor", "Crétacé", "136kg" },
                    { 3, "Herbivore", "20m", "30m", "North America, Portugal, Tanzanie", "Brachiosaurus", "Jurassique", "60t" },
                    { 4, "Carnivore", "2m", "6m", "Arizona", "Dilophosaurus", "Jurassique", "1t" },
                    { 5, "Herbivore", "2m", "8m", "Mongolie", "Gallimimus", "Crétacé", "440kg" },
                    { 6, "Herbivore", "3m", "10m", "North America", "Triceratops", "Crétacé", "10t" },
                    { 7, "Herbivore", "4m", "10m", "North America", "Parasaurolophus", "Crétacé", "5t" },
                    { 8, "Carnivore", "30cm", "1m", "Europe", "Compsognathus", "Jurassique", "3,5kg" },
                    { 9, "Herbivore", "4m", "9m", "Morrison Formation", "Stegosaurus", "Jurassique", "4t" },
                    { 10, "Carnivore", "7m", "15m", "North Africa", "Spinosaurus", "Crétacé", "10t" },
                    { 11, "Carnivore", "20m", "2m", "Sea of Europe, North America, Africa, Australia, New-Zealand", "Mosasaurus", "Crétacé", "10t" },
                    { 12, "Carnivore", "2,5m", "9,5m", "England, Spain", "Baryonyx", "Crétacé", "2,5t" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dinosaurs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dinosaurs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Dinosaurs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Dinosaurs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Dinosaurs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Dinosaurs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Dinosaurs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Dinosaurs",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Dinosaurs",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Dinosaurs",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Dinosaurs",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Dinosaurs",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}

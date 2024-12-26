using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLL.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "UserName", "Password", "IsActive", "RoleId" },
                values: new object[] { 1, "admin", "admin", true, 1 });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Comedy" },
                    { 3, "Drama" },
                    { 4, "Science Fiction" },
                    { 5, "Horror" }
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Name", "Surname", "IsRetired" },
                values: new object[,]
                {
                    { 1, "Christopher", "Nolan", false },
                    { 2, "Martin", "Scorsese", false },
                    { 3, "Quentin", "Tarantino", false }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Name", "ReleaseDate", "TotalRevenue", "DirectorId" },
                values: new object[,]
                {
                    { 1, "Inception", new DateTime(2010, 7, 16), 836800000M, 1 },
                    { 2, "The Wolf of Wall Street", new DateTime(2013, 12, 25), 392000000M, 2 },
                    { 3, "Pulp Fiction", new DateTime(1994, 10, 14), 213900000M, 3 }
                });

            migrationBuilder.InsertData(
                table: "MovieGenres",
                columns: new[] { "Id", "MovieId", "GenreId" },
                values: new object[,]
                {
                    { 1, 1, 1 }, // Inception - Action
                    { 2, 1, 4 }, // Inception - Science Fiction
                    { 3, 2, 3 }, // Wolf of Wall Street - Drama
                    { 4, 2, 2 }, // Wolf of Wall Street - Comedy
                    { 5, 3, 1 }, // Pulp Fiction - Action
                    { 6, 3, 3 }  // Pulp Fiction - Drama
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove all seeded data
            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6 });

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3 });

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3 });

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2 });
        }
    }
}

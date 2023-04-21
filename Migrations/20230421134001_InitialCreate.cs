using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net_ef_videogame.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SoftwareHouses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareHouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videogames",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoftwareHouseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videogames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videogames_SoftwareHouses_SoftwareHouseId",
                        column: x => x.SoftwareHouseId,
                        principalTable: "SoftwareHouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videogames_SoftwareHouseId",
                table: "Videogames",
                column: "SoftwareHouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videogames");

            migrationBuilder.DropTable(
                name: "SoftwareHouses");
        }
    }
}

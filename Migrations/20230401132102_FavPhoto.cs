using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlannerApp.Migrations
{
    public partial class FavPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavouriteClientPhotograph",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    PhotographId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteClientPhotograph", x => new { x.ClientId, x.PhotographId });
                    table.ForeignKey(
                        name: "FK_FavouriteClientPhotograph_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteClientPhotograph_Photograph_PhotographId",
                        column: x => x.PhotographId,
                        principalTable: "Photograph",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteClientPhotograph_PhotographId",
                table: "FavouriteClientPhotograph",
                column: "PhotographId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteClientPhotograph");
        }
    }
}

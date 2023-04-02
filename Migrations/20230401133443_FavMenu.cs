using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlannerApp.Migrations
{
    public partial class FavMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavouriteClientMenu",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteClientMenu", x => new { x.ClientId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_FavouriteClientMenu_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteClientMenu_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteClientMenu_MenuId",
                table: "FavouriteClientMenu",
                column: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteClientMenu");
        }
    }
}

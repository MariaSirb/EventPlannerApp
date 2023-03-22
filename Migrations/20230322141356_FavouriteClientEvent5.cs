using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlannerApp.Migrations
{
    public partial class FavouriteClientEvent5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavouriteClientEvent",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    MyEventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteClientEvent", x => new { x.ClientId, x.MyEventId });
                    table.ForeignKey(
                        name: "FK_FavouriteClientEvent_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteClientEvent_MyEvent_MyEventId",
                        column: x => x.MyEventId,
                        principalTable: "MyEvent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteClientEvent_MyEventId",
                table: "FavouriteClientEvent",
                column: "MyEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteClientEvent");
        }
    }
}

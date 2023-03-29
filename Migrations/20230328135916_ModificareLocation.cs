using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlannerApp.Migrations
{
    public partial class ModificareLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientID",
                table: "Location",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_ClientID",
                table: "Location",
                column: "ClientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Client_ClientID",
                table: "Location",
                column: "ClientID",
                principalTable: "Client",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Client_ClientID",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_ClientID",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "ClientID",
                table: "Location");
        }
    }
}

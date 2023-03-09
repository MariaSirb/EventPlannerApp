using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlannerApp.Migrations
{
    public partial class Relatie2Location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationID",
                table: "MyEvent",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyEvent_LocationID",
                table: "MyEvent",
                column: "LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_MyEvent_Location_LocationID",
                table: "MyEvent",
                column: "LocationID",
                principalTable: "Location",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyEvent_Location_LocationID",
                table: "MyEvent");

            migrationBuilder.DropIndex(
                name: "IX_MyEvent_LocationID",
                table: "MyEvent");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "MyEvent");
        }
    }
}

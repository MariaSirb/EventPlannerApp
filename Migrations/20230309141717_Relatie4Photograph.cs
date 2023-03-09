using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlannerApp.Migrations
{
    public partial class Relatie4Photograph : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhotographID",
                table: "MyEvent",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyEvent_PhotographID",
                table: "MyEvent",
                column: "PhotographID");

            migrationBuilder.AddForeignKey(
                name: "FK_MyEvent_Photograph_PhotographID",
                table: "MyEvent",
                column: "PhotographID",
                principalTable: "Photograph",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyEvent_Photograph_PhotographID",
                table: "MyEvent");

            migrationBuilder.DropIndex(
                name: "IX_MyEvent_PhotographID",
                table: "MyEvent");

            migrationBuilder.DropColumn(
                name: "PhotographID",
                table: "MyEvent");
        }
    }
}

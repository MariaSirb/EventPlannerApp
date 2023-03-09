using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlannerApp.Migrations
{
    public partial class Relatie3Music : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MusicID",
                table: "MyEvent",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyEvent_MusicID",
                table: "MyEvent",
                column: "MusicID");

            migrationBuilder.AddForeignKey(
                name: "FK_MyEvent_Music_MusicID",
                table: "MyEvent",
                column: "MusicID",
                principalTable: "Music",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyEvent_Music_MusicID",
                table: "MyEvent");

            migrationBuilder.DropIndex(
                name: "IX_MyEvent_MusicID",
                table: "MyEvent");

            migrationBuilder.DropColumn(
                name: "MusicID",
                table: "MyEvent");
        }
    }
}

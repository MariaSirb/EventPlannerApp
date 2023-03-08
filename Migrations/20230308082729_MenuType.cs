using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlannerApp.Migrations
{
    public partial class MenuType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuTypeID",
                table: "Menu",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MenuType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuType", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menu_MenuTypeID",
                table: "Menu",
                column: "MenuTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_MenuType_MenuTypeID",
                table: "Menu",
                column: "MenuTypeID",
                principalTable: "MenuType",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_MenuType_MenuTypeID",
                table: "Menu");

            migrationBuilder.DropTable(
                name: "MenuType");

            migrationBuilder.DropIndex(
                name: "IX_Menu_MenuTypeID",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "MenuTypeID",
                table: "Menu");
        }
    }
}

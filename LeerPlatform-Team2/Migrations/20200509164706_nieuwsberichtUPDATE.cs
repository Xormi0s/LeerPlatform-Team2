using Microsoft.EntityFrameworkCore.Migrations;

namespace LeerPlatform_Team2.Migrations
{
    public partial class nieuwsberichtUPDATE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Titel",
                table: "Nieuwsberichten",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titel",
                table: "Nieuwsberichten");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace LeerPlatform_Team2.Migrations
{
    public partial class AaassnInscrijin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inschrijvingen_AspNetUsers_GebruikerId",
                table: "Inschrijvingen");

            migrationBuilder.DropForeignKey(
                name: "FK_Inschrijvingen_TblLessen_Lescode",
                table: "Inschrijvingen");

            migrationBuilder.DropIndex(
                name: "IX_Inschrijvingen_GebruikerId",
                table: "Inschrijvingen");

            migrationBuilder.DropIndex(
                name: "IX_Inschrijvingen_Lescode",
                table: "Inschrijvingen");

            migrationBuilder.DropColumn(
                name: "GebruikerId",
                table: "Inschrijvingen");

            migrationBuilder.AlterColumn<string>(
                name: "Lescode",
                table: "Inschrijvingen",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GebruikerNaam",
                table: "Inschrijvingen",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GebruikerNavigationId",
                table: "Inschrijvingen",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LescodeNavigationLescode",
                table: "Inschrijvingen",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijvingen_GebruikerNavigationId",
                table: "Inschrijvingen",
                column: "GebruikerNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijvingen_LescodeNavigationLescode",
                table: "Inschrijvingen",
                column: "LescodeNavigationLescode");

            migrationBuilder.AddForeignKey(
                name: "FK_Inschrijvingen_AspNetUsers_GebruikerNavigationId",
                table: "Inschrijvingen",
                column: "GebruikerNavigationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inschrijvingen_TblLessen_LescodeNavigationLescode",
                table: "Inschrijvingen",
                column: "LescodeNavigationLescode",
                principalTable: "TblLessen",
                principalColumn: "Lescode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inschrijvingen_AspNetUsers_GebruikerNavigationId",
                table: "Inschrijvingen");

            migrationBuilder.DropForeignKey(
                name: "FK_Inschrijvingen_TblLessen_LescodeNavigationLescode",
                table: "Inschrijvingen");

            migrationBuilder.DropIndex(
                name: "IX_Inschrijvingen_GebruikerNavigationId",
                table: "Inschrijvingen");

            migrationBuilder.DropIndex(
                name: "IX_Inschrijvingen_LescodeNavigationLescode",
                table: "Inschrijvingen");

            migrationBuilder.DropColumn(
                name: "GebruikerNaam",
                table: "Inschrijvingen");

            migrationBuilder.DropColumn(
                name: "GebruikerNavigationId",
                table: "Inschrijvingen");

            migrationBuilder.DropColumn(
                name: "LescodeNavigationLescode",
                table: "Inschrijvingen");

            migrationBuilder.AlterColumn<string>(
                name: "Lescode",
                table: "Inschrijvingen",
                type: "varchar(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GebruikerId",
                table: "Inschrijvingen",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijvingen_GebruikerId",
                table: "Inschrijvingen",
                column: "GebruikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijvingen_Lescode",
                table: "Inschrijvingen",
                column: "Lescode");

            migrationBuilder.AddForeignKey(
                name: "FK_Inschrijvingen_AspNetUsers_GebruikerId",
                table: "Inschrijvingen",
                column: "GebruikerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inschrijvingen_TblLessen_Lescode",
                table: "Inschrijvingen",
                column: "Lescode",
                principalTable: "TblLessen",
                principalColumn: "Lescode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

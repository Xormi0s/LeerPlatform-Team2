using Microsoft.EntityFrameworkCore.Migrations;

namespace LeerPlatform_Team2.Migrations
{
    public partial class tblPlanningStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Capaciteit",
                table: "TblLokalen",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "StudentenPerPlannings",
                columns: table => new
                {
                    PlanningStudentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gebruikersnaam = table.Column<string>(nullable: true),
                    PlanningID = table.Column<int>(nullable: false),
                    GebruikerNavigationId = table.Column<string>(nullable: true),
                    PlanningIDNavigationPlanningId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentenPerPlannings", x => x.PlanningStudentID);
                    table.ForeignKey(
                        name: "FK_StudentenPerPlannings_AspNetUsers_GebruikerNavigationId",
                        column: x => x.GebruikerNavigationId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentenPerPlannings_TblPlanning_PlanningIDNavigationPlanningId",
                        column: x => x.PlanningIDNavigationPlanningId,
                        principalTable: "TblPlanning",
                        principalColumn: "PlanningID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentenPerPlannings_GebruikerNavigationId",
                table: "StudentenPerPlannings",
                column: "GebruikerNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentenPerPlannings_PlanningIDNavigationPlanningId",
                table: "StudentenPerPlannings",
                column: "PlanningIDNavigationPlanningId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentenPerPlannings");

            migrationBuilder.AlterColumn<int>(
                name: "Capaciteit",
                table: "TblLokalen",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

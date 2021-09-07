using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeerPlatform_Team2.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblFunctionaliteiten",
                columns: table => new
                {
                    FunctionaliteitID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Beschrijving = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblFunctionaliteiten", x => x.FunctionaliteitID);
                });

            migrationBuilder.CreateTable(
                name: "TblLessen",
                columns: table => new
                {
                    Lescode = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
                    Titel = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Studiepunten = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblLessen", x => x.Lescode);
                });

            migrationBuilder.CreateTable(
                name: "TblLessenreeks",
                columns: table => new
                {
                    Reekscode = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
                    Titel = table.Column<string>(unicode: false, nullable: false),
                    Ingeschreven = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblLessenreeks", x => x.Reekscode);
                });

            migrationBuilder.CreateTable(
                name: "TblLokalen",
                columns: table => new
                {
                    Lokaalnummer = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
                    Locatie = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Capaciteit = table.Column<int>(nullable: false),
                    FunctionaliteitenID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblLokalen", x => x.Lokaalnummer);
                    table.ForeignKey(
                        name: "FK_TblFunctionaliteiten_TblLokalen",
                        column: x => x.FunctionaliteitenID,
                        principalTable: "TblFunctionaliteiten",
                        principalColumn: "FunctionaliteitID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblPlanning",
                columns: table => new
                {
                    PlanningID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lokaalnummer = table.Column<string>(unicode: false, maxLength: 4, nullable: false),
                    Lescode = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
                    Reekscode = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
                    Starttijdstip = table.Column<DateTime>(name: "Start tijdstip", type: "smalldatetime", nullable: false),
                    Eindtijdstip = table.Column<DateTime>(name: "Eind tijdstip", type: "smalldatetime", nullable: false),
                    Extrainfo = table.Column<string>(name: "Extra info", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPlanning", x => x.PlanningID);
                    table.ForeignKey(
                        name: "FK_TblLessen_TblPlanning",
                        column: x => x.Lescode,
                        principalTable: "TblLessen",
                        principalColumn: "Lescode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblLokalen_TblPlanning",
                        column: x => x.Lokaalnummer,
                        principalTable: "TblLokalen",
                        principalColumn: "Lokaalnummer",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblLessenreeks_TblPlanning",
                        column: x => x.Reekscode,
                        principalTable: "TblLessenreeks",
                        principalColumn: "Reekscode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblLokalen_FunctionaliteitenID",
                table: "TblLokalen",
                column: "FunctionaliteitenID");

            migrationBuilder.CreateIndex(
                name: "IX_TblPlanning_Lescode",
                table: "TblPlanning",
                column: "Lescode");

            migrationBuilder.CreateIndex(
                name: "IX_TblPlanning_Lokaalnummer",
                table: "TblPlanning",
                column: "Lokaalnummer");

            migrationBuilder.CreateIndex(
                name: "IX_TblPlanning_Reekscode",
                table: "TblPlanning",
                column: "Reekscode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblPlanning");

            migrationBuilder.DropTable(
                name: "TblLessen");

            migrationBuilder.DropTable(
                name: "TblLokalen");

            migrationBuilder.DropTable(
                name: "TblLessenreeks");

            migrationBuilder.DropTable(
                name: "TblFunctionaliteiten");
        }
    }
}

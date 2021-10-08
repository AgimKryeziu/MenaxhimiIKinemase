using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MenaxhimiIKinemase.Data.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emertimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pershkrimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventPicture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventiID);
                });

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    FilmiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulli = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Premiera = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Regjisori = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zhanri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pershkrimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoviePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieTrailer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.FilmiID);
                });

            migrationBuilder.CreateTable(
                name: "Orar",
                columns: table => new
                {
                    OrariID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilmiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orar", x => x.OrariID);
                    table.ForeignKey(
                        name: "FK_Orar_Film_FilmiID",
                        column: x => x.FilmiID,
                        principalTable: "Film",
                        principalColumn: "FilmiID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orar_FilmiID",
                table: "Orar",
                column: "FilmiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Orar");

            migrationBuilder.DropTable(
                name: "Film");
        }
    }
}

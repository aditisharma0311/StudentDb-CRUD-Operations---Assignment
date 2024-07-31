using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentDb.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    RollNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Class = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.RollNo);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    RollNo = table.Column<int>(type: "int", nullable: false),
                    Hindi = table.Column<int>(type: "int", nullable: false),
                    English = table.Column<int>(type: "int", nullable: false),
                    Science = table.Column<int>(type: "int", nullable: false),
                    History = table.Column<int>(type: "int", nullable: false),
                    GK = table.Column<int>(type: "int", nullable: false),
                    TotalMarks = table.Column<int>(type: "int", nullable: false, computedColumnSql: "[Hindi] + [English] + [Science] + [History] + [GK] PERSISTED")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.RollNo);
                    table.ForeignKey(
                        name: "FK_Results_Students_RollNo",
                        column: x => x.RollNo,
                        principalTable: "Students",
                        principalColumn: "RollNo",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}

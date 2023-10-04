using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace weHelpTax_.net6_.Migrations
{
    public partial class addingnewmodelTerms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    Term1 = table.Column<bool>(type: "bit", nullable: false),
                    Term2 = table.Column<bool>(type: "bit", nullable: false),
                    Term3 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.EmpId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Terms");
        }
    }
}

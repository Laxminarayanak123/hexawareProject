using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace weHelpTax_.net6_.Migrations
{
    public partial class addingnewmodelTermwiseTax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TermSalary",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    TaxTerm1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxTerm2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxTerm3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermSalary", x => x.EmpId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TermSalary");
        }
    }
}

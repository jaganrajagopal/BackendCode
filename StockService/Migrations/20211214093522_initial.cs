using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockService.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyCode = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyCEO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyTurnover = table.Column<double>(type: "float", nullable: false),
                    CompanyWebsite = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyCode);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockPrice = table.Column<double>(type: "float", nullable: false),
                    StockDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StockExchange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockId);
                    table.ForeignKey(
                        name: "FK_Stocks_Companies_CompanyCode",
                        column: x => x.CompanyCode,
                        principalTable: "Companies",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_CompanyCode",
                table: "Stocks",
                column: "CompanyCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}

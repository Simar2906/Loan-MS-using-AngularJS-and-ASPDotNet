using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Loan_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Loans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Logo = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    LoanAmount = table.Column<string>(type: "text", nullable: false),
                    InterestRates = table.Column<string>(type: "text", nullable: false),
                    MinCreditScore = table.Column<int>(type: "integer", nullable: false),
                    TermLength = table.Column<decimal>(type: "numeric", nullable: false),
                    ProcessingFee = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppliedLoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AppliedAmount = table.Column<int>(type: "integer", nullable: false),
                    AppliedRate = table.Column<int>(type: "integer", nullable: false),
                    TermLength = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DateApplied = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LoanId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedLoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppliedLoans_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppliedLoans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedLoans_LoanId",
                table: "AppliedLoans",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedLoans_UserId",
                table: "AppliedLoans",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedLoans");

            migrationBuilder.DropTable(
                name: "Loans");
        }
    }
}

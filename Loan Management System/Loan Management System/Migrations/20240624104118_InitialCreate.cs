using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Loan_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilePath = table.Column<string>(type: "character varying(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(40)", nullable: false),
                    MinCreditScore = table.Column<int>(type: "integer", nullable: false),
                    TermLength = table.Column<decimal>(type: "numeric", nullable: false),
                    ProcessingFee = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    LogoPictureId = table.Column<int>(type: "integer", nullable: false),
                    MinLoanAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    MaxLoanAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    MinInterestRate = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    MaxInterestRate = table.Column<decimal>(type: "numeric(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Files_LogoPictureId",
                        column: x => x.LogoPictureId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(255)", nullable: false),
                    Gender = table.Column<int>(type: "gender_enum", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", nullable: false),
                    Password = table.Column<string>(type: "character varying(128)", nullable: false),
                    Salt = table.Column<string>(type: "character varying(60)", nullable: false),
                    Role = table.Column<int>(type: "role_enum", nullable: false),
                    Salary = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Employer = table.Column<string>(type: "character varying(40)", nullable: false),
                    Designation = table.Column<string>(type: "character varying(40)", nullable: false),
                    ProfilePictureFileId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Files_ProfilePictureFileId",
                        column: x => x.ProfilePictureFileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppliedLoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AppliedAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AppliedRate = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LogoPictureId",
                table: "Loans",
                column: "LogoPictureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfilePictureFileId",
                table: "Users",
                column: "ProfilePictureFileId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedLoans");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourtDemoProject.CaseManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    CaseId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CourtName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CaseType = table.Column<int>(type: "int", nullable: false),
                    DateOfOffense = table.Column<DateOnly>(type: "date", nullable: false),
                    Verdict = table.Column<int>(type: "int", nullable: false),
                    Plead = table.Column<int>(type: "int", nullable: false),
                    CourtDates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.CaseId);
                });

            migrationBuilder.CreateTable(
                name: "CaseDetails",
                columns: table => new
                {
                    CaseDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaseDetailEntryDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DocketDetail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DocumentUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseEntityCaseId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseDetails", x => x.CaseDetailId);
                    table.ForeignKey(
                        name: "FK_CaseDetails_Cases_CaseEntityCaseId",
                        column: x => x.CaseEntityCaseId,
                        principalTable: "Cases",
                        principalColumn: "CaseId");
                });

            migrationBuilder.CreateTable(
                name: "CaseParticipants",
                columns: table => new
                {
                    CaseParticipantEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaseParticipantType = table.Column<int>(type: "int", nullable: false),
                    CaseParticipantFirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CaseParticipantMiddleName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CaseParticipantLastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CaseEntityCaseId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseParticipants", x => x.CaseParticipantEntityId);
                    table.ForeignKey(
                        name: "FK_CaseParticipants_Cases_CaseEntityCaseId",
                        column: x => x.CaseEntityCaseId,
                        principalTable: "Cases",
                        principalColumn: "CaseId");
                });

            migrationBuilder.CreateTable(
                name: "Charges",
                columns: table => new
                {
                    ChargeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChargeName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ChargeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ChargeType = table.Column<int>(type: "int", nullable: false),
                    JudgementType = table.Column<int>(type: "int", nullable: false),
                    FineAmount = table.Column<double>(type: "float", nullable: false),
                    SentenceLengthIndays = table.Column<int>(type: "int", nullable: false),
                    CaseEntityCaseId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charges", x => x.ChargeId);
                    table.ForeignKey(
                        name: "FK_Charges_Cases_CaseEntityCaseId",
                        column: x => x.CaseEntityCaseId,
                        principalTable: "Cases",
                        principalColumn: "CaseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseDetails_CaseEntityCaseId",
                table: "CaseDetails",
                column: "CaseEntityCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseParticipants_CaseEntityCaseId",
                table: "CaseParticipants",
                column: "CaseEntityCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_CaseEntityCaseId",
                table: "Charges",
                column: "CaseEntityCaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseDetails");

            migrationBuilder.DropTable(
                name: "CaseParticipants");

            migrationBuilder.DropTable(
                name: "Charges");

            migrationBuilder.DropTable(
                name: "Cases");
        }
    }
}

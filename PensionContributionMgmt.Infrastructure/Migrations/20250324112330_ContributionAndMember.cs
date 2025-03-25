using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PensionContributionMgmt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContributionAndMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "Employers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "contributionId",
                table: "Employers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Employers_contributionId",
                table: "Employers",
                column: "contributionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_MemberId",
                table: "Employers",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Contributions_contributionId",
                table: "Employers",
                column: "contributionId",
                principalTable: "Contributions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Members_MemberId",
                table: "Employers",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Contributions_contributionId",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Members_MemberId",
                table: "Employers");

            migrationBuilder.DropIndex(
                name: "IX_Employers_contributionId",
                table: "Employers");

            migrationBuilder.DropIndex(
                name: "IX_Employers_MemberId",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "contributionId",
                table: "Employers");
        }
    }
}

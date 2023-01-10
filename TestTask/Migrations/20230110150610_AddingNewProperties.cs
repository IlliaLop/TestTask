using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Incident_IncidentName",
                table: "Incident");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Name",
                table: "Account",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Account_Name",
                table: "Account");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_IncidentName",
                table: "Incident",
                column: "IncidentName",
                unique: true);
        }
    }
}

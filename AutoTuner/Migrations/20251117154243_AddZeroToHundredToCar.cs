using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTuner.Migrations
{
    /// <inheritdoc />
    public partial class AddZeroToHundredToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ZeroToHundred",
                table: "Cars",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "ZeroToHundred",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "ZeroToHundred",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "ZeroToHundred",
                value: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZeroToHundred",
                table: "Cars");
        }
    }
}

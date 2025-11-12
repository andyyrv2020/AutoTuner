using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoTuner.Data.Migrations
{
    public partial class StrategyEnhancements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Budget",
                table: "Recommendations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Goal",
                table: "Recommendations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeSafetyParts",
                table: "Recommendations",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSafetyCritical",
                table: "TuningParts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "TuningParts",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsSafetyCritical",
                value: false);

            migrationBuilder.UpdateData(
                table: "TuningParts",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsSafetyCritical",
                value: false);

            migrationBuilder.UpdateData(
                table: "TuningParts",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsSafetyCritical",
                value: false);

            migrationBuilder.UpdateData(
                table: "TuningParts",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsSafetyCritical",
                value: false);

            migrationBuilder.UpdateData(
                table: "TuningParts",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsSafetyCritical",
                value: false);

            migrationBuilder.UpdateData(
                table: "TuningParts",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsSafetyCritical",
                value: false);

            migrationBuilder.InsertData(
                table: "TuningParts",
                columns: new[] { "Id", "Category", "Cost", "Description", "EfficiencyImpact", "IsSafetyCritical", "Name", "PowerGain", "RecommendedForStyle", "TorqueGain" },
                values: new object[,]
                {
                    { 7, "Brakes", 1100m, "Six-piston calipers with semi-slick pads for confident stopping power.", -0.5, true, "Performance Brake Kit", 0, 1, 0 },
                    { 8, "Suspension", 1400m, "Height and damping adjustable coilovers tuned for spirited driving.", 2.0, true, "Adjustable Coilover Suspension", 0, 1, 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TuningParts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TuningParts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "Goal",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "IncludeSafetyParts",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "IsSafetyCritical",
                table: "TuningParts");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AutoTuner.Data.Migrations
{
    public partial class DomainSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    EngineType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HorsePower = table.Column<int>(type: "int", nullable: false),
                    Torque = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Drivetrain = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DrivingStyle = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TuningParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PowerGain = table.Column<int>(type: "int", nullable: false),
                    TorqueGain = table.Column<int>(type: "int", nullable: false),
                    EfficiencyImpact = table.Column<double>(type: "float", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecommendedForStyle = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TuningParts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workshops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    OldPower = table.Column<int>(type: "int", nullable: false),
                    NewPower = table.Column<int>(type: "int", nullable: false),
                    OldTorque = table.Column<int>(type: "int", nullable: false),
                    NewTorque = table.Column<int>(type: "int", nullable: false),
                    DateApplied = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceHistories_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    TuningPartId = table.Column<int>(type: "int", nullable: false),
                    PredictedPower = table.Column<int>(type: "int", nullable: false),
                    PredictedTorque = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateGenerated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recommendations_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recommendations_TuningParts_TuningPartId",
                        column: x => x.TuningPartId,
                        principalTable: "TuningParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b8c7553d-0b10-454f-8d1c-6cbf165168ff", 0, "DEMO-CONCURRENCY-STAMP", "demo@autotuner.ai", true, false, null, "DEMO@AUTOTUNER.AI", "DEMO@AUTOTUNER.AI", "AQEAAAAQJwAAEAAAAD0vHIqbDU5vESIzRFVmd4ggAAAAJMotm0/c8s7LQD6BsIbVrk5VdXVsXfA/pffr8XvOroU=", null, false, "DEMO-SECURITY-STAMP", false, "demo@autotuner.ai" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "DrivingStyle", "Drivetrain", "EngineType", "HorsePower", "Model", "Torque", "UserId", "Weight", "Year" },
                values: new object[,]
                {
                    { 1, "Volkswagen", 0, "FWD", "2.0 TSI", 245, "Golf GTI", 370, "b8c7553d-0b10-454f-8d1c-6cbf165168ff", 1440, 2021 },
                    { 2, "BMW", 1, "RWD", "3.0 Twin Turbo", 473, "M3", 550, "b8c7553d-0b10-454f-8d1c-6cbf165168ff", 1680, 2020 },
                    { 3, "Subaru", 1, "AWD", "2.0 Turbo", 268, "WRX", 350, "b8c7553d-0b10-454f-8d1c-6cbf165168ff", 1500, 2019 }
                });

            migrationBuilder.InsertData(
                table: "TuningParts",
                columns: new[] { "Id", "Category", "Cost", "Description", "EfficiencyImpact", "Name", "PowerGain", "RecommendedForStyle", "TorqueGain" },
                values: new object[,]
                {
                    { 1, "Intake", 450m, "High flow intake system for improved airflow.", 2.5, "Performance Air Intake", 15, 1, 12 },
                    { 2, "Exhaust", 980m, "Cat-back exhaust for enhanced sound and flow.", 1.5, "Sport Exhaust", 20, 1, 18 },
                    { 3, "Electronics", 650m, "Software tune optimized for premium fuel.", -1.0, "ECU Tune Stage 1", 40, 0, 60 },
                    { 4, "Chassis", 1200m, "Forged wheels reducing unsprung mass.", 3.5, "Lightweight Wheels", 0, 0, 0 },
                    { 5, "Fuel", 750m, "Supports higher boost levels with consistent fuel delivery.", -2.0, "High-Flow Fuel Pump", 25, 1, 28 },
                    { 6, "Electronics", 300m, "Economy focused tune for better mileage.", 6.0, "Eco Driving Chip", 5, 2, 8 }
                });

            migrationBuilder.InsertData(
                table: "Workshops",
                columns: new[] { "Id", "Address", "City", "Latitude", "Longitude", "Name", "Specialization" },
                values: new object[,]
                {
                    { 1, "123 Speed Ave", "Sofia", 42.6977, 23.3219, "Turbo Masters", "Forced Induction" },
                    { 2, "45 Track St", "Plovdiv", 42.1354, 24.7453, "Precision Tuners", "ECU Calibration" },
                    { 3, "78 Green Blvd", "Varna", 43.2141, 27.9147, "EcoDrive Labs", "Hybrid & Economy" }
                });

            migrationBuilder.InsertData(
                table: "PerformanceHistories",
                columns: new[] { "Id", "CarId", "DateApplied", "NewPower", "NewTorque", "OldPower", "OldTorque" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 3, 15, 0, 0, 0, DateTimeKind.Unspecified), 245, 370, 230, 350 },
                    { 2, 2, new DateTime(2023, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), 473, 550, 430, 500 },
                    { 3, 3, new DateTime(2023, 2, 2, 0, 0, 0, DateTimeKind.Unspecified), 268, 350, 250, 330 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_UserId",
                table: "Cars",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceHistories_CarId",
                table: "PerformanceHistories",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_CarId",
                table: "Recommendations",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_TuningPartId",
                table: "Recommendations",
                column: "TuningPartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerformanceHistories");

            migrationBuilder.DropTable(
                name: "Recommendations");

            migrationBuilder.DropTable(
                name: "Workshops");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "TuningParts");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b8c7553d-0b10-454f-8d1c-6cbf165168ff");
        }
    }
}

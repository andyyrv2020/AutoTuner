using AutoTuner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoTuner.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Cars => Set<Car>();

    public DbSet<TuningPart> TuningParts => Set<TuningPart>();

    public DbSet<Recommendation> Recommendations => Set<Recommendation>();

    public DbSet<Workshop> Workshops => Set<Workshop>();

    public DbSet<PerformanceHistory> PerformanceHistories => Set<PerformanceHistory>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var demoUser = new IdentityUser
        {
            Id = "b8c7553d-0b10-454f-8d1c-6cbf165168ff",
            UserName = "demo@autotuner.ai",
            NormalizedUserName = "DEMO@AUTOTUNER.AI",
            Email = "demo@autotuner.ai",
            NormalizedEmail = "DEMO@AUTOTUNER.AI",
            EmailConfirmed = true,
            SecurityStamp = "DEMO-SECURITY-STAMP",
            ConcurrencyStamp = "DEMO-CONCURRENCY-STAMP"
        };

        demoUser.PasswordHash = "AQEAAAAQJwAAEAAAAD0vHIqbDU5vESIzRFVmd4ggAAAAJMotm0/c8s7LQD6BsIbVrk5VdXVsXfA/pffr8XvOroU=";
        demoUser.LockoutEnabled = false;
        demoUser.PhoneNumberConfirmed = false;
        demoUser.TwoFactorEnabled = false;
        demoUser.AccessFailedCount = 0;

        builder.Entity<IdentityUser>().HasData(demoUser);

        builder.Entity<Car>().HasData(
            new Car
            {
                Id = 1,
                UserId = demoUser.Id,
                Brand = "Volkswagen",
                Model = "Golf GTI",
                Year = 2021,
                EngineType = "2.0 TSI",
                HorsePower = 245,
                Torque = 370,
                Weight = 1440,
                Drivetrain = "FWD",
                DrivingStyle = DrivingStyle.Daily
            },
            new Car
            {
                Id = 2,
                UserId = demoUser.Id,
                Brand = "BMW",
                Model = "M3",
                Year = 2020,
                EngineType = "3.0 Twin Turbo",
                HorsePower = 473,
                Torque = 550,
                Weight = 1680,
                Drivetrain = "RWD",
                DrivingStyle = DrivingStyle.Sport
            },
            new Car
            {
                Id = 3,
                UserId = demoUser.Id,
                Brand = "Subaru",
                Model = "WRX",
                Year = 2019,
                EngineType = "2.0 Turbo",
                HorsePower = 268,
                Torque = 350,
                Weight = 1500,
                Drivetrain = "AWD",
                DrivingStyle = DrivingStyle.Sport
            });

        builder.Entity<TuningPart>().HasData(
            new TuningPart
            {
                Id = 1,
                Name = "Performance Air Intake",
                Category = "Intake",
                Description = "High flow intake system for improved airflow.",
                PowerGain = 15,
                TorqueGain = 12,
                EfficiencyImpact = 2.5,
                Cost = 450m,
                RecommendedForStyle = DrivingStyle.Sport,
                IsSafetyCritical = false
            },
            new TuningPart
            {
                Id = 2,
                Name = "Sport Exhaust",
                Category = "Exhaust",
                Description = "Cat-back exhaust for enhanced sound and flow.",
                PowerGain = 20,
                TorqueGain = 18,
                EfficiencyImpact = 1.5,
                Cost = 980m,
                RecommendedForStyle = DrivingStyle.Sport,
                IsSafetyCritical = false
            },
            new TuningPart
            {
                Id = 3,
                Name = "ECU Tune Stage 1",
                Category = "Electronics",
                Description = "Software tune optimized for premium fuel.",
                PowerGain = 40,
                TorqueGain = 60,
                EfficiencyImpact = -1.0,
                Cost = 650m,
                RecommendedForStyle = DrivingStyle.Daily,
                IsSafetyCritical = false
            },
            new TuningPart
            {
                Id = 4,
                Name = "Lightweight Wheels",
                Category = "Chassis",
                Description = "Forged wheels reducing unsprung mass.",
                PowerGain = 0,
                TorqueGain = 0,
                EfficiencyImpact = 3.5,
                Cost = 1200m,
                RecommendedForStyle = DrivingStyle.Daily,
                IsSafetyCritical = false
            },
            new TuningPart
            {
                Id = 5,
                Name = "High-Flow Fuel Pump",
                Category = "Fuel",
                Description = "Supports higher boost levels with consistent fuel delivery.",
                PowerGain = 25,
                TorqueGain = 28,
                EfficiencyImpact = -2.0,
                Cost = 750m,
                RecommendedForStyle = DrivingStyle.Sport,
                IsSafetyCritical = false
            },
            new TuningPart
            {
                Id = 6,
                Name = "Eco Driving Chip",
                Category = "Electronics",
                Description = "Economy focused tune for better mileage.",
                PowerGain = 5,
                TorqueGain = 8,
                EfficiencyImpact = 6.0,
                Cost = 300m,
                RecommendedForStyle = DrivingStyle.Eco,
                IsSafetyCritical = false
            },
            new TuningPart
            {
                Id = 7,
                Name = "Performance Brake Kit",
                Category = "Brakes",
                Description = "Six-piston calipers with semi-slick pads for confident stopping power.",
                PowerGain = 0,
                TorqueGain = 0,
                EfficiencyImpact = -0.5,
                Cost = 1100m,
                RecommendedForStyle = DrivingStyle.Sport,
                IsSafetyCritical = true
            },
            new TuningPart
            {
                Id = 8,
                Name = "Adjustable Coilover Suspension",
                Category = "Suspension",
                Description = "Height and damping adjustable coilovers tuned for spirited driving.",
                PowerGain = 0,
                TorqueGain = 5,
                EfficiencyImpact = 2.0,
                Cost = 1400m,
                RecommendedForStyle = DrivingStyle.Sport,
                IsSafetyCritical = true
            });

        builder.Entity<Workshop>().HasData(
            new Workshop
            {
                Id = 1,
                Name = "Turbo Masters",
                City = "Sofia",
                Address = "123 Speed Ave",
                Latitude = 42.6977,
                Longitude = 23.3219,
                Specialization = "Forced Induction"
            },
            new Workshop
            {
                Id = 2,
                Name = "Precision Tuners",
                City = "Plovdiv",
                Address = "45 Track St",
                Latitude = 42.1354,
                Longitude = 24.7453,
                Specialization = "ECU Calibration"
            },
            new Workshop
            {
                Id = 3,
                Name = "EcoDrive Labs",
                City = "Varna",
                Address = "78 Green Blvd",
                Latitude = 43.2141,
                Longitude = 27.9147,
                Specialization = "Hybrid & Economy"
            });

        builder.Entity<PerformanceHistory>().HasData(
            new PerformanceHistory
            {
                Id = 1,
                CarId = 1,
                OldPower = 230,
                NewPower = 245,
                OldTorque = 350,
                NewTorque = 370,
                DateApplied = new DateTime(2023, 3, 15)
            },
            new PerformanceHistory
            {
                Id = 2,
                CarId = 2,
                OldPower = 430,
                NewPower = 473,
                OldTorque = 500,
                NewTorque = 550,
                DateApplied = new DateTime(2023, 5, 10)
            },
            new PerformanceHistory
            {
                Id = 3,
                CarId = 3,
                OldPower = 250,
                NewPower = 268,
                OldTorque = 330,
                NewTorque = 350,
                DateApplied = new DateTime(2023, 2, 2)
            });
    }
}

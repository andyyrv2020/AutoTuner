INSERT INTO [dbo].[TuningParts]
(Name, Category, Description, PowerGain, TorqueGain, EfficiencyImpact, Cost, RecommendedForStyle, IsSafetyCritical)
VALUES
-- 1
('Stage 1 ECU Remap', 'ECU', 'Mild ECU optimization for better performance and response.', 20, 30, 0.04, 350.00, 1, 0),
-- 2
('Stage 2 ECU Remap', 'ECU', 'More aggressive tune requiring supporting mods.', 40, 60, 0.03, 500.00, 1, 0),
-- 3
('Eco Fuel Map Tune', 'ECU', 'Improves fuel economy at low and mid RPM.', 0, 0, 0.08, 320.00, 2, 0),
-- 4
('Cold Air Intake', 'Intake', 'Improves airflow and reduces intake temps.', 8, 5, 0.02, 280.00, 0, 0),
-- 5
('High-Flow Air Filter', 'Intake', 'Drop-in filter with improved airflow.', 3, 2, 0.01, 90.00, 0, 0),
-- 6
('Stage 1 Turbo Inlet', 'Intake', 'More efficient airflow to turbo.', 5, 6, 0.02, 250.00, 1, 0),
-- 7
('Performance Downpipe', 'Exhaust', 'Improves flow and turbo spool.', 12, 18, -0.01, 480.00, 1, 0),
-- 8
('High-Flow Sports Exhaust', 'Exhaust', 'Less restrictive exhaust with improved tone.', 10, 8, 0.00, 750.00, 1, 0),
-- 9
('Cat-Back Exhaust System', 'Exhaust', 'Enhanced flow and sound quality.', 5, 4, 0.01, 600.00, 0, 0),
-- 10
('Big Turbo Upgrade', 'Turbo', 'Large turbo for major HP gains.', 80, 100, -0.10, 2100.00, 1, 0),
-- 11
('Hybrid Turbo Upgrade', 'Turbo', 'Balanced turbo upgrade for daily + sport.', 45, 55, -0.04, 1500.00, 1, 0),
-- 12
('High-Pressure Fuel Pump', 'Fuel', 'Supports higher fuel demands.', 5, 10, 0, 380.00, 1, 0),
-- 13
('High-Flow Injectors', 'Fuel', 'Required for Stage 2+ power builds.', 15, 18, -0.02, 450.00, 1, 0),
-- 14
('E85 Conversion Kit', 'Fuel', 'Allows engine to run on ethanol.', 25, 30, -0.05, 300.00, 1, 0),
-- 15
('Upgraded Fuel Rail', 'Fuel', 'Improved fuel delivery consistency.', 2, 3, 0.00, 280.00, 1, 0),
-- 16
('Front-Mount Intercooler', 'Cooling', 'Lowers intake temps for stable performance.', 10, 15, 0.03, 650.00, 1, 0),
-- 17
('Upgraded Radiator', 'Cooling', 'Better engine cooling for demanding use.', 0, 0, 0.04, 400.00, 0, 1),
-- 18
('Oil Cooler Kit', 'Cooling', 'Lowers oil temps under load.', 0, 0, 0.02, 350.00, 1, 1),
-- 19
('Performance Camshaft', 'Engine', 'Higher lift/duration for high RPM power.', 18, 10, -0.03, 900.00, 1, 0),
-- 20
('Lightweight Pulleys', 'Engine', 'Reduces engine drag and improves response.', 5, 4, 0.02, 300.00, 2, 0),
-- 21
('Forged Pistons', 'Engine', 'Stronger internal components for high boost.', 0, 0, 0, 1200.00, 1, 0),
-- 22
('Forged Connecting Rods', 'Engine', 'Reinforces bottom-end durability.', 0, 0, 0, 1100.00, 1, 0),
-- 23
('Lightweight Flywheel', 'Drivetrain', 'Faster rev response.', 0, 0, 0.03, 400.00, 1, 1),
-- 24
('Stage 3 Clutch Kit', 'Drivetrain', 'Handles increased torque.', 0, 0, 0, 650.00, 1, 1),
-- 25
('Limited Slip Differential (LSD)', 'Drivetrain', 'Improves traction.', 0, 0, 0.03, 900.00, 1, 1),
-- 26
('Short Shifter Kit', 'Drivetrain', 'Shorter and faster gear shifts.', 0, 0, 0.01, 200.00, 1, 0),
-- 27
('Performance Driveshaft', 'Drivetrain', 'Reduces rotational mass and improves reliability.', 0, 0, 0.01, 800.00, 1, 1),
-- 28
('Racing Coilovers', 'Suspension', 'Adjustable suspension for handling.', 0, 0, 0.05, 1100.00, 1, 1),
-- 29
('Sport Springs', 'Suspension', 'Lower center of gravity for better control.', 0, 0, 0.03, 300.00, 1, 1),
-- 30
('Adjustable Sway Bars', 'Suspension', 'Reduces body roll.', 0, 0, 0.04, 250.00, 1, 1),
-- 31
('Strut Tower Brace', 'Suspension', 'Increases chassis rigidity.', 0, 0, 0.02, 180.00, 1, 1),
-- 32
('Sport Brake Kit', 'Brakes', 'Better stopping power and heat resistance.', 0, 0, 0, 800.00, 1, 1),
-- 33
('Performance Brake Pads', 'Brakes', 'Improved braking response.', 0, 0, 0, 160.00, 1, 1),
-- 34
('Big Brake Kit', 'Brakes', 'Large rotors + calipers for track use.', 0, 0, 0, 1400.00, 1, 1),
-- 35
('Stainless Steel Brake Lines', 'Brakes', 'Improves pedal feel and reliability.', 0, 0, 0, 120.00, 1, 1),
-- 36
('Racing Tires', 'Tires', 'Maximum grip for performance driving.', 0, 0, -0.02, 650.00, 1, 1),
-- 37
('Eco Low-Rolling Tires', 'Tires', 'Better fuel economy with reduced resistance.', 0, 0, 0.06, 300.00, 2, 1),
-- 38
('All-Season Tires', 'Tires', 'Balanced grip for everyday driving.', 0, 0, 0, 350.00, 0, 1),
-- 39
('Lightweight Alloy Wheels', 'Body', 'Reduces unsprung weight.', 0, 0, 0.03, 700.00, 1, 0),
-- 40
('Carbon Fiber Hood', 'Body', 'Reduces front-end weight.', 0, 0, 0.04, 900.00, 1, 0),
-- 41
('Rear Wing Spoiler', 'Body', 'Adds downforce at high speeds.', 0, 0, -0.01, 350.00, 1, 0),
-- 42
('Front Splitter', 'Body', 'Improves aerodynamics.', 0, 0, -0.01, 280.00, 1, 0),
-- 43
('Oil Catch Can', 'Engine', 'Prevents carbon buildup in intake.', 0, 0, 0.01, 150.00, 0, 0),
-- 44
('High-Performance Battery', 'Electrical', 'Lightweight battery with stronger output.', 0, 0, 0.02, 250.00, 0, 0),
-- 45
('Upgraded Alternator', 'Electrical', 'Improves power delivery to accessories.', 0, 0, 0.01, 300.00, 0, 0),
-- 46
('Short Ram Intake', 'Intake', 'Compact intake for quick throttle response.', 5, 4, -0.01, 190.00, 1, 0),
-- 47
('Dual Catch Can System', 'Engine', 'Improved crankcase ventilation.', 0, 0, 0.02, 220.00, 0, 0),
-- 48
('Thermal Intake Manifold Spacer', 'Engine', 'Reduces heat soak.', 2, 2, 0.02, 120.00, 2, 0),
-- 49
('Underdrive Crank Pulley', 'Engine', 'Reduces parasitic losses.', 4, 3, 0.02, 240.00, 2, 0),
-- 50
('Engine Mount Stiffeners', 'Engine', 'Reduces engine movement for better response.', 0, 0, 0.01, 180.00, 1, 1);

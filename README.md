🏎️ AutoTuner
A Web Application for Managing Cars, Tuning Parts, and Performance Analytics

Built with ASP.NET Core MVC · Entity Framework Core · Microsoft SQL Server

📌 Overview

AutoTuner is a web application designed for car enthusiasts, tuners, and mechanics.
It allows users to register their vehicles, track installed tuning parts, calculate performance gains, and maintain a detailed history of modifications.
The system also provides AI-assisted recommendations based on driving style (Daily / Sport).

The project leverages GenAI technologies (ChatGPT, GitHub Copilot, Codex) to assist during development:

generating boilerplate code

resolving errors

optimizing queries

designing models and diagrams

generating sample seed data

AutoTuner demonstrates how modern AI tools can support software development without replacing the developer.

🚀 Features
🔧 Car Management

Add, edit, delete, and list cars

Store brand, model, year, base horsepower, torque, fuel type

User-specific data (Identity authentication)

🛠️ Tuning Parts Management

Full catalog of tuning parts (intake, exhaust, turbo, remap, etc.)

Each part contains:

Power Gain

Torque Gain

Efficiency Impact

Cost

Category & Description

📈 Performance Calculations

Automatic recalculation of total horsepower & torque

Tracks all changes over time

Built-in Performance History system

🧠 AI-Based Recommendations

Suggests tuning parts based on driving style:

Daily

Sport

Useful for beginners who want guidance on which upgrades to install

🗺️ Workshops Module

Records automotive workshops with:

Name

City

Address

GPS coordinates

Specialization

Prepared for map integration (Google Maps / OpenStreetMap)

🗄️ Database Structure (ERD Overview)

The system uses SQL Server with the following main tables:

Cars

TuningParts

CarTuning (many-to-many join table)

PerformanceHistories

Workshops

The relationships:

One Car → many PerformanceHistory entries

Many Cars ↔ many TuningParts (via CarTuning)

🧱 Technologies Used
Backend

ASP.NET Core MVC (.NET 8 / 9)

Entity Framework Core

Microsoft SQL Server

Frontend

Bootstrap 5

Razor Views

Custom CSS

Authentication

ASP.NET Core Identity

Dev Tools

Visual Studio 2022

GitHub Copilot / OpenAI ChatGPT

MermaidJS for diagrams

🧭 Project Structure
/AutoTuner
 ├── Controllers/
 ├── Models/
 ├── Data/
 │    ├── ApplicationDbContext.cs
 │    └── Migrations/
 ├── Views/
 ├── Services/
 ├── wwwroot/
 └── README.md

🏁 Getting Started
1. Clone the repository:
git clone https://github.com/<your-username>/AutoTuner.git

2. Configure the database:

Update your appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AutoTunerDb;Trusted_Connection=True;"
}

3. Apply migrations:
dotnet ef database update

4. Run the application:
dotnet run

📷 Screenshots (Optional Section)

Add images of your pages here: Dashboard, Cars list, Tuning view, Workshop list, Performance History.

🤖 GenAI Usage

This project was developed with support from:

ChatGPT – generating and explaining code, fixing errors, creating diagrams

GitHub Copilot / Codex – assisting with C# and EF Core logic

AI was used as a helper, not as a replacement for development.

📌 Future Improvements

OBD-II log import (CSV / JSON)

Real-time analytics

Mobile version / PWA

Live tuning recommendations

Map integration for workshops

📜 License

MIT License (or your preferred license)

⭐ Support the Project

If you like the project, consider giving it a star ⭐ on GitHub!

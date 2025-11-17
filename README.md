# 🏎️ AutoTuner
### A Web Application for Managing Cars, Tuning Parts, and Performance Analytics
Built with ASP.NET Core MVC · Entity Framework Core · Microsoft SQL Server

## Overview
AutoTuner is a web application designed for car enthusiasts, tuners, and mechanics.  
It allows users to register their vehicles, track installed tuning parts, calculate performance gains, and maintain a detailed history of modifications.  
The system also provides AI-assisted recommendations based on driving style (Daily / Sport).

## Features
### Car Management
- Add, edit, delete, and list cars  
- Store brand, model, year, base horsepower, torque, fuel type  
- User-specific data (Identity authentication)

### Tuning Parts Management
- Full catalog of tuning parts (intake, exhaust, turbo, remap, etc.)  
- Each part contains: Power Gain, Torque Gain, Efficiency Impact, Cost, Category & Description

### Performance Calculations
- Automatic recalculation of total horsepower & torque  
- Tracks all changes over time  
- Built‑in Performance History system

### AI‑Based Recommendations
- Suggests tuning parts based on driving style (Daily / Sport)

### Workshops Module
- Stores workshop name, city, address, GPS coordinates, specialization  
- Ready for map integration

## Technologies Used
- ASP.NET Core MVC (.NET 8/9)
- Entity Framework Core
- Microsoft SQL Server
- Bootstrap 5
- Razor Views
- ASP.NET Identity
- MermaidJS for diagrams
- GitHub Copilot / ChatGPT for development support

## Getting Started
### 1. Clone the repository:
git clone https://github.com/<your-username>/AutoTuner.git

### 2. Configure the database:
Edit appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AutoTunerDb;Trusted_Connection=True;"
}

### 3. Apply migrations:
dotnet ef database update

### 4. Run the application:
dotnet run

## Future Improvements
- OBD-II log import  
- Real‑time analytics  
- Mobile/PWA version  
- Advanced AI tuning suggestions  
- Workshop map integration

## License
MIT License

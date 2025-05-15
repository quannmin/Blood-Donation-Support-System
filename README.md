
https://img.shields.io/badge/version-1.0.0-blue.svg
https://img.shields.io/badge/license-MIT-green.svg
https://img.shields.io/badge/.NET-6.0-purple.svg
<p align="center">
  <img src="https://via.placeholder.com/200x200?text=Blood+Donation" alt="Blood Donation System Logo" width="200" height="200"/>
</p>

Connecting donors to recipients. Saving lives one donation at a time.


✨ Overview
The Blood Donation Support System is a comprehensive platform designed to revolutionize how medical centers manage blood donations. By leveraging modern technology, we're creating a seamless connection between donors, recipients, and medical staff – making the life-saving process of blood donation more efficient than ever.
🚀 Key Features
FeatureDescription👥 User ManagementIntuitive profiles for donors, recipients, and staff🧬 Blood MatchingAdvanced compatibility algorithms for safe transfusions📱 Emergency AlertsReal-time notifications for urgent blood needs📍 Proximity SearchLocation-based donor finding for time-critical situations📊 Inventory ControlComplete tracking of blood units with expiry monitoring📋 Health ScreeningDigital health check recording and monitoring📈 Analytics DashboardComprehensive reporting on donations and usage🔔 Smart RemindersAutomatic notifications for eligible donors
💻 Technology Stack
<p align="center">
  <img src="https://via.placeholder.com/80x80?text=.NET" alt=".NET" width="80" height="80"/>
  <img src="https://via.placeholder.com/80x80?text=EF+Core" alt="Entity Framework Core" width="80" height="80"/>
  <img src="https://via.placeholder.com/80x80?text=SQL" alt="SQL Server" width="80" height="80"/>
  <img src="https://via.placeholder.com/80x80?text=C%23" alt="C#" width="80" height="80"/>
  <img src="https://via.placeholder.com/80x80?text=Razor" alt="Razor Pages" width="80" height="80"/>
</p>

Backend: ASP.NET Core 6.0
Database: SQL Server with Entity Framework Core
Frontend: Razor Pages / MVC with Bootstrap
Authentication: ASP.NET Core Identity
Location Services: Geolocation API for proximity features

🔧 Installation
Prerequisites

Visual Studio 2022 or higher
.NET 6.0 SDK
SQL Server
Git

Quick Start
bash# Clone the repository
git clone https://github.com/yourusername/PRN232_Blood_Donation_Support_System.git

# Navigate to project folder
cd PRN232_Blood_Donation_Support_System

# Restore dependencies
dotnet restore

# Update database with migrations
dotnet ef database update

# Run the application
dotnet run
📊 Database Schema
<p align="center">
  <img src="https://via.placeholder.com/600x400?text=Database+Schema" alt="Database Schema" width="600"/>
</p>
Our system uses a Code-First approach with carefully designed entities:

Users - Core user information
DonorProfiles - Comprehensive donor details
BloodTypes - Blood classification system
BloodRequests - Donation request management
BloodInventory - Stock management system
DonationProcess - Complete workflow tracking

🔄 Workflow Visualization
<p align="center">
  <img src="https://via.placeholder.com/700x300?text=Donation+Workflow" alt="Donation Workflow" width="700"/>
</p>

📋 Request - Blood request is created
🔍 Inventory Check - System checks available stock
👥 Donor Matching - Compatible donors are identified
🔔 Notification - Potential donors are alerted
📅 Scheduling - Donation appointments are arranged
🩺 Health Screening - Donor eligibility is confirmed
💉 Donation - Blood is collected and processed
🏥 Transfusion - Recipient receives needed blood
📊 Records - System updates all information

🛠️ Core Components
User Management
Robust system for managing user accounts with role-based access control.
Blood Matching System
Sophisticated algorithms ensure safe and compatible blood matching for all transfusion types.
Donation Workflow Engine
Streamlined process management from request to fulfillment with complete tracking.
Inventory Control Center
Comprehensive blood unit tracking with expiration monitoring and automatic alerts.
Emergency Response System
Rapid-response system for urgent blood needs with proximity-based donor matching.
👨‍💻 Development
The project follows a clean architecture pattern with separation of concerns:
PRN232_Blood_Donation_Support_System/
├── 🎮 Controllers/
├── 💾 Data/
├── 📋 Models/
├── 🔧 Services/
├── 🖥️ Views/
└── 📄 Program.cs
👥 Contributing
We welcome contributions to make this system even better!

Fork the repository
Create your feature branch: git checkout -b amazing-feature
Commit your changes: git commit -m 'Add amazing feature'
Push to the branch: git push origin amazing-feature
Open a Pull Request

📜 License
This project is licensed under the MIT License - see the LICENSE file for details.
🙏 Acknowledgments

Medical professionals who provided domain expertise
The open-source community for valuable libraries and tools
All blood donors worldwide who save lives every day


<p align="center">
  <b>❤️ Every donation matters. Every life is precious. ❤️</b>
</p>
<p align="center">
  Made with passion by <a href="mailto:your-email@example.com">Your Team</a>
</p>
Blood Donation Support System
Overview
The Blood Donation Support System is an application designed to facilitate blood donation processes for a medical center. This system connects blood donors with recipients, manages blood inventory, and streamlines the entire donation workflow from request to transfusion.
Features

User Management: Registration and profiles for donors and recipients
Blood Type Compatibility: Advanced matching based on blood types and transfusion types
Donation Process Tracking: Complete workflow management from request to transfusion
Emergency Notification: Critical blood need alerts to potential donors
Inventory Management: Track available blood units, expiry dates, and usage
Proximity Search: Find donors/recipients based on geographical distance
Health Check Tracking: Record and monitor donor health metrics
Reporting & Dashboard: Analytics on donations, usage, and inventory levels

Technology Stack

Backend: ASP.NET Core 6.0
ORM: Entity Framework Core 6.0
Database: SQL Server
Frontend: Razor Pages / MVC
Authentication: ASP.NET Core Identity
Mapping: Location-based services for proximity search

Getting Started
Prerequisites

Visual Studio 2022 or higher
.NET 6.0 SDK
SQL Server (Local or Express)
Git

Installation

Clone the repository:
git clone https://github.com/yourusername/PRN232_Blood_Donation_Support_System.git

Open the solution in Visual Studio.
Update the connection string in appsettings.json:
json"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BloodDonationDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}

Run database migrations:
Add-Migration InitialCreate
Update-Database

Run the application:
dotnet run


Database Schema
The system uses a Code-First approach with Entity Framework Core. Key entities include:

Users: Base user information (donors, recipients, staff)
DonorProfiles: Detailed donor information and preferences
RecipientProfiles: Recipient information and medical conditions
BloodTypes: Blood type definitions (A+, A-, B+, etc.)
BloodCompatibility: Rules for blood type compatibility
BloodRequests: Blood donation requests from recipients
BloodDonations: Records of completed donations
BloodInventory: Available blood units in storage
DonationProcess: Workflow tracking for each donation
EmergencyNotifications: Urgent blood need alerts

System Workflow

Request Creation: Recipient or staff creates a blood request
Inventory Check: System checks for available matching blood units
Donor Matching: If no inventory available, system finds matching donors
Notification: Potential donors are notified based on compatibility and location
Scheduling: Donor accepts and schedules donation appointment
Health Check: Donor's health is verified before donation
Donation: Blood is collected and processed
Transfusion: Blood is provided to the recipient
Records & Analytics: System updates records and generates reports

Project Structure
PRN232_Blood_Donation_Support_System/
├── Controllers/            # MVC Controllers
├── Data/                   # Database context and migrations
├── Models/                 # Entity models
│   ├── BloodDonation.cs
│   ├── BloodInventory.cs
│   ├── BloodRequest.cs
│   ├── BloodType.cs
│   ├── DonorProfile.cs
│   └── ...
├── Services/               # Business logic services
├── Views/                  # MVC Views
├── wwwroot/                # Static files (CSS, JS)
├── Program.cs              # Application startup
└── appsettings.json        # Configuration
Core Components
1. User Management

Registration and authentication
Donor and recipient profile management
Role-based access control (Admin, Staff, Member)

2. Blood Matching System

Comprehensive blood compatibility management
Support for whole blood and component transfusions
Intelligent donor-recipient matching algorithms

3. Workflow Management

Process tracking from request to fulfillment
Status updates and notifications
Audit logging of all process steps

4. Inventory Control

Blood unit tracking with expiry monitoring
Component separation records
Stock level monitoring and alerts

5. Emergency Response

Urgent request handling
Location-based donor finding
Priority notification system

Contribution Guidelines

Fork the repository
Create a feature branch (git checkout -b feature/amazing-feature)
Commit your changes (git commit -m 'Add some amazing feature')
Push to the branch (git push origin feature/amazing-feature)
Open a Pull Request

License
This project is licensed under the MIT License - see the LICENSE file for details.
Acknowledgments

Entity Framework Core
ASP.NET Core
Medical professionals who provided insights on blood donation processes

Contact
Project Maintainer - your-email@example.com

Note: This README provides a high-level overview of the Blood Donation Support System. Detailed documentation for specific components can be found in the project's Wiki or code documentation.

# Smart Error Detective System

## Overview

Smart Error Detective System is a web-based application built using ASP.NET Core MVC and SQL Server that helps developers and support teams efficiently track, analyze, and manage application errors.

The system automatically categorizes errors, predicts severity levels, detects duplicate issues, generates suggested fixes, and provides analytical insights through an interactive dashboard.

---

## Features

### Core Features

* Error Logging System
* Smart Error Categorization
* Severity Detection
* Duplicate Error Detection
* Analytics Dashboard
* Search & Filtering
* Responsive User Interface

### Bonus Features

* AI-Style Fix Suggestions
* Error Analytics Charts
* Dark / Light Mode
* Excel Export
* Dashboard Statistics

---

## Technology Stack

### Backend

* ASP.NET Core MVC (.NET 8)
* Entity Framework Core
* SQL Server

### Frontend

* Razor Views
* Bootstrap 5
* HTML5
* CSS3
* JavaScript
* Chart.js

### Database

* SQL Server LocalDB
* Entity Framework Core Migrations

---

## System Architecture

### Layers

#### Presentation Layer

* Razor Views
* Bootstrap UI
* Dashboard & Forms

#### Business Logic Layer

* ErrorAnalyzerService
* Category Detection
* Severity Prediction
* Suggested Fix Generation

#### Data Access Layer

* Entity Framework Core
* ApplicationDbContext
* SQL Server Database

---

## Database Structure

### ErrorLogs

| Field           | Description        |
| --------------- | ------------------ |
| Id              | Primary Key        |
| Title           | Error Title        |
| Description     | Error Description  |
| StackTrace      | Error Stack Trace  |
| ModuleName      | Application Module |
| Status          | Error Status       |
| CategoryId      | Foreign Key        |
| SeverityId      | Foreign Key        |
| SuggestedFix    | Auto Generated Fix |
| OccurrenceCount | Duplicate Tracking |

### Categories

* Database
* API
* Authentication
* UI
* Network

### Severities

* Low
* Medium
* High
* Critical

---

## Smart Features

### Automatic Categorization

Errors are automatically categorized based on stack trace analysis.

Example:

* SQL Exception → Database
* Authentication Error → Authentication
* API Timeout → API

### Severity Prediction

Severity is automatically determined from error patterns.

Example:

* Null Reference → High
* Database Failure → Critical
* Validation Error → Medium

### Duplicate Detection

If the same error is reported multiple times:

* New record is not created
* Occurrence count is increased

### Suggested Fix Generation

The system generates possible fixes based on the error title and stack trace.

---

## Dashboard

The dashboard provides:

* Total Errors
* Critical Errors
* High Severity Errors
* Duplicate Errors
* Severity Distribution Chart
* Category Distribution Chart

---

## Search & Filtering

Users can search errors by:

* Error Title
* Category
* Severity
* Status

---

## Export Functionality

The application supports:

* Excel Export (.xlsx)

Users can export all error records for reporting and analysis.

---

## Dark Mode

The application includes:

* Light Theme
* Dark Theme
* Theme Persistence using Local Storage

---

## Project Structure

SmartError.API

├── Controllers

├── Models

├── DTOs

├── Services

├── ViewModels

├── Data

├── Views

├── wwwroot

├── Migrations

└── Program.cs

---

## Setup Instructions

### Clone Repository

git clone <repository-url>

### Open Project

Open the solution in Visual Studio 2022.

### Configure Database

Update connection string in:

appsettings.json

### Run Migrations

Add-Migration InitialCreate

Update-Database

### Run Application

Press:

F5

or

Ctrl + F5

---

## Default Workflow

1. Open Dashboard
2. Add New Error
3. System Categorizes Error
4. Severity Is Predicted
5. Suggested Fix Is Generated
6. Dashboard Updates Analytics
7. Export Reports When Needed

---

## Future Enhancements

* Real-Time Monitoring
* PDF Export
* Firebase Integration
* Flutter Mobile Application
* Voice-Based Bug Reporting
* Email Notifications

---

## Author

Ayushi Thummar

Smart Error Detective System

---

## Conclusion

Smart Error Detective System demonstrates practical software engineering concepts including error management, automated analysis, dashboard reporting, data visualization, duplicate detection, and modern ASP.NET Core development practices.

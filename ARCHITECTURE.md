# Smart Error Detective System – Architecture Document

## 1. System Overview

The Smart Error Detective System is a web-based error management platform built using ASP.NET Core MVC and SQL Server.

The system captures application errors, automatically analyzes them, categorizes issues, predicts severity levels, detects duplicate errors, generates suggested fixes, and provides analytical insights through a dashboard.

---

## 2. High-Level Architecture

```text
┌─────────────────────┐
│      User UI        │
│  Razor + Bootstrap  │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│    MVC Controllers  │
│ DashboardController │
│ ErrorController     │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│   Business Layer    │
│ ErrorAnalyzerService│
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│ Data Access Layer   │
│ ApplicationDbContext│
│ Entity Framework    │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│    SQL Server DB    │
└─────────────────────┘
```

---


```md
## Automatic Error Capture Flow

```text
Application Exception
         │
         ▼
+--------------------+
| Error Capture Layer|
+--------------------+
         │
         ▼
+--------------------+
| ErrorAnalyzer      |
| Service            |
+--------------------+
         │
         ▼
+--------------------+
| Duplicate Detector |
+--------------------+
         │
         ▼
+--------------------+
| SQL Server         |
+--------------------+
         │
         ▼
+--------------------+
| Dashboard & Charts |
+--------------------+

```
## 3. Layered Architecture

### Presentation Layer

Responsible for:

* User Interface
* Forms
* Dashboard
* Search & Filtering
* Charts
* Dark/Light Mode

Technologies:

* Razor Views
* Bootstrap 5
* HTML
* CSS
* JavaScript
* Chart.js

Files:

```text
Views/
Views/Dashboard/
Views/Error/
Views/Shared/
wwwroot/css/

```
---

### Controller Layer

Acts as communication bridge between UI and business logic.

Controllers:

#### DashboardController

Responsibilities:

* Dashboard Statistics
* Chart Data Generation
* Analytics Display

#### ErrorController

Responsibilities:

* Create Error
* Edit Error
* Delete Error
* Search Errors
* Export Excel
* Error Details

---

### Business Logic Layer

Implemented using:

```text
Services/
ErrorAnalyzerService.cs
```

Responsibilities:

#### Error Categorization

Example:

```text
SQL Exception
→ Database Category

API Timeout
→ API Category
```

#### Severity Prediction

Example:

```text
Database Failure
→ Critical

Null Reference Exception
→ High
```

#### Suggested Fix Generation

Example:

```text
NullReferenceException
→ Check object initialization before use.
```

---

### Data Access Layer

Implemented using:

```text
ApplicationDbContext
Entity Framework Core
```

Responsibilities:

* Database Operations
* CRUD Operations
* Relationship Management
* Query Execution

---

## 4. Database Architecture

### ErrorLogs Table

| Column          | Purpose                 |
| --------------- | ----------------------- |
| Id              | Primary Key             |
| Title           | Error Name              |
| Description     | Error Description       |
| StackTrace      | Stack Information       |
| ModuleName      | Source Module           |
| Status          | Current Status          |
| CategoryId      | Category Reference      |
| SeverityId      | Severity Reference      |
| SuggestedFix    | Auto Generated Solution |
| OccurrenceCount | Duplicate Tracking      |

---

### Categories Table

Stores error categories.

Examples:

* Database
* API
* Authentication
* UI
* Network

---

### Severities Table

Stores severity levels.

Examples:

* Low
* Medium
* High
* Critical

---

## 5. Entity Relationships

```text
Categories (1)
      │
      │
      ▼
ErrorLogs (Many)
      ▲
      │
      │
Severities (1)
```

Relationship Type:

* One Category → Many Errors
* One Severity → Many Errors

---

## 6. Error Processing Workflow

### Step 1

An application exception occurs OR a user reports an error.

### Step 2

Error is captured by the system.

Sources:

* Automatic Exception Capture
* Manual Error Reporting Form

### Step 3

ErrorAnalyzerService analyzes the error.

Tasks:

* Category Detection
* Severity Prediction
* Suggested Fix Generation

### Step 4

Duplicate Detection executes.

Comparison:

* Error Title
* Stack Trace

If duplicate:

OccurrenceCount++

Else:

Create New Error Record

### Step 5

Error is stored in SQL Server.

### Step 6

Dashboard statistics and charts update automatically.


## 7. Analytics Dashboard

Dashboard provides:

### Statistics

* Total Errors
* Critical Errors
* High Severity Errors
* Duplicate Errors

### Visual Analytics

#### Pie Chart

Severity Distribution

#### Bar Chart

Category Distribution

Implemented using:

```text
Chart.js
```

---

## 8. Export Module

Implemented Feature:

### Excel Export

Technology:

```text
ClosedXML
```

Output:

```text
ErrorReport.xlsx
```

Contains:

* Error Title
* Category
* Severity
* Status
* Occurrence Count

---

## 9. UI Features

### Responsive Design

Implemented using Bootstrap.

### Dark Mode

Features:

* Theme Toggle
* Local Storage Persistence
* User Preference Retention

### Search & Filtering

Filter by:

* Title
* Category
* Severity
* Status

---

## 10. Future Enhancements

Potential improvements:

* Real-Time Monitoring
* Firebase Notifications
* PDF Export
* Flutter Mobile App
* Voice-Based Bug Reporting
* Machine Learning Severity Prediction

---

## Conclusion

The Smart Error Detective System follows a clean layered architecture using ASP.NET Core MVC, Entity Framework Core, and SQL Server. The design separates presentation, business logic, and data access concerns, making the application scalable, maintainable, and suitable for real-world error management scenarios.

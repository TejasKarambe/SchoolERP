# SchoolApi — Complete Documentation

> **Version:** 2.0 (Full Implementation)
> **Framework:** ASP.NET Core 9.0
> **Database:** MySQL (via Pomelo EF Core)
> **Auth:** JWT Bearer Tokens + BCrypt password hashing
> **Last Updated:** March 2026

---

## Table of Contents

1. [Project Overview](#1-project-overview)
2. [Technology Stack](#2-technology-stack)
3. [Project Structure](#3-project-structure)
4. [Architecture](#4-architecture)
5. [Database Schema & Entities](#5-database-schema--entities)
6. [Authentication & Authorization](#6-authentication--authorization)
7. [API Reference — All Endpoints](#7-api-reference--all-endpoints)
   - [Auth](#71-auth-api)
   - [Academic Years](#72-academic-years-api)
   - [Programs](#73-programs-api)
   - [Classes](#74-classes-api)
   - [Sections](#75-sections-api)
   - [Subjects](#76-subjects-api)
   - [Students](#77-students-api)
   - [Staff](#78-staff-api)
   - [Student Enrollments](#79-student-enrollments-api)
   - [Staff Assignments](#710-staff-assignments-api)
   - [Student Attendance](#711-student-attendance-api)
   - [Staff Attendance](#712-staff-attendance-api)
   - [Fees](#713-fees-api)
   - [Exams & Results](#714-exams--results-api)
   - [Notifications](#715-notifications-api)
   - [Reports](#716-reports-api)
8. [Roles & Permissions](#8-roles--permissions)
9. [Configuration & Setup](#9-configuration--setup)
10. [Docker Support](#10-docker-support)
11. [EF Core Migrations](#11-ef-core-migrations)
12. [Coding Conventions](#12-coding-conventions)
13. [NuGet Packages](#13-nuget-packages)

---

## 1. Project Overview

**SchoolApi** is a fully-featured, production-ready RESTful Web API built with **ASP.NET Core 9.0**. It serves as a complete backend for a **School Management System**, covering every aspect of school operations from student admissions to exam results, attendance tracking, fee management, and staff coordination.

### Key Features

| Feature                  | Status |
|--------------------------|--------|
| JWT Authentication       | ✅ Done |
| Role-based Authorization | ✅ Done |
| Academic Year Management | ✅ Done |
| Program/Class/Section CRUD | ✅ Done |
| Subject Management       | ✅ Done |
| Student Management       | ✅ Done |
| Staff Management         | ✅ Done |
| Student Enrollment       | ✅ Done |
| Staff Assignment         | ✅ Done |
| Student Attendance       | ✅ Done (incl. Bulk) |
| Staff Attendance         | ✅ Done |
| Fee Structure & Payments | ✅ Done |
| Exam Management          | ✅ Done |
| Exam Results             | ✅ Done (incl. Bulk) |
| Notifications            | ✅ Done |
| Reports & Analytics      | ✅ Done |
| Docker Support           | ✅ Done |
| Swagger/OpenAPI UI       | ✅ Done |

---

## 2. Technology Stack

| Component             | Technology                              |
|-----------------------|-----------------------------------------|
| Framework             | ASP.NET Core 9.0 Web API               |
| Language              | C# (.NET 9)                            |
| ORM                   | Entity Framework Core 9.0              |
| Database              | MySQL 8.x                              |
| MySQL EF Provider     | Pomelo.EntityFrameworkCore.MySql 9.0.0 |
| Authentication        | JWT Bearer (JwtBearer 9.0.0)           |
| Password Hashing      | BCrypt.Net-Next 4.0.3                  |
| API Documentation     | Swagger (Swashbuckle.AspNetCore 10.x)  |
| Containerization      | Docker (Linux containers)              |

---

## 3. Project Structure

```
SchoolApi/
│
├── Controllers/                          # HTTP endpoints
│   ├── AuthController.cs
│   ├── AcademicYearsController.cs
│   ├── ProgramsController.cs
│   ├── ClassesController.cs
│   ├── SectionsController.cs
│   ├── SubjectsController.cs
│   ├── StudentsController.cs
│   ├── StaffController.cs
│   ├── StudentEnrollmentsController.cs
│   ├── StaffAssignmentsController.cs
│   ├── StudentAttendanceController.cs
│   ├── StaffAttendanceController.cs
│   ├── FeesController.cs
│   ├── ExamsController.cs
│   ├── NotificationsController.cs
│   └── ReportsController.cs
│
├── DTOs/                                  # Request/response shapes
│   ├── Auth/AuthDtos.cs
│   ├── AcademicYear/
│   ├── Programs/
│   ├── Classes/
│   ├── Sections/SectionDtos.cs
│   ├── Subjects/SubjectDtos.cs
│   ├── Students/StudentDtos.cs
│   ├── Staff/StaffDtos.cs
│   ├── Enrollments/EnrollmentDtos.cs
│   ├── StaffAssignments/StaffAssignmentDtos.cs
│   ├── Attendance/AttendanceDtos.cs
│   ├── Fees/FeeDtos.cs
│   ├── Exams/ExamDtos.cs
│   ├── Notifications/NotificationDtos.cs
│   └── Reports/ReportDtos.cs
│
├── Helpers/
│   └── JwtHelper.cs                       # JWT token generation helper
│
├── Models/
│   ├── Data/
│   │   ├── Configurations/               # EF Core Fluent API
│   │   │   ├── StudentConfiguration.cs
│   │   │   ├── ClassConfiguration.cs
│   │   │   ├── SectionConfiguration.cs
│   │   │   └── StaffConfiguration.cs
│   │   └── SchoolDbContext.cs
│   └── Entities/                         # Domain models
│       ├── BaseEntity.cs
│       ├── AcademicYear.cs
│       ├── Program.cs (ProgramEntity)
│       ├── Class.cs
│       ├── Section.cs
│       ├── Subject.cs
│       ├── Student.cs
│       ├── Staff.cs
│       ├── User.cs
│       ├── StudentEnrollment.cs
│       ├── StaffAssignment.cs
│       ├── StudentAttendance.cs
│       ├── StaffAttendance.cs
│       ├── FeeStructure.cs
│       ├── FeePayment.cs
│       ├── Exam.cs
│       ├── ExamResult.cs
│       └── Notification.cs
│
├── Services/
│   ├── Interfaces/                       # Contracts
│   │   ├── IAcademicYearService.cs
│   │   ├── IProgramService.cs
│   │   ├── IClassService.cs
│   │   ├── ISectionService.cs
│   │   ├── ISubjectService.cs
│   │   ├── IStudentService.cs
│   │   ├── IStaffService.cs
│   │   ├── IStudentEnrollmentService.cs
│   │   ├── IStaffAssignmentService.cs
│   │   ├── IStudentAttendanceService.cs
│   │   ├── IStaffAttendanceService.cs
│   │   ├── IAuthService.cs
│   │   ├── IFeeService.cs
│   │   ├── IExamService.cs
│   │   ├── INotificationService.cs
│   │   └── IReportService.cs
│   └── Implementations/                  # Business logic
│       ├── AcademicYearService.cs
│       ├── ProgramService.cs
│       ├── ClassService.cs
│       ├── SectionService.cs
│       ├── SubjectService.cs
│       ├── StudentService.cs
│       ├── StaffService.cs
│       ├── StudentEnrollmentService.cs
│       ├── StaffAssignmentService.cs
│       ├── StudentAttendanceService.cs
│       ├── StaffAttendanceService.cs
│       ├── AuthService.cs
│       ├── FeeService.cs
│       ├── ExamService.cs
│       ├── NotificationService.cs
│       └── ReportService.cs
│
├── Migrations/                           # EF Core migration history
├── appsettings.json                      # Configuration
├── appsettings.Development.json
├── Dockerfile
├── Program.cs                            # App bootstrap & DI
└── SchoolApi.csproj
```

---

## 4. Architecture

```
HTTP Request
     │
     ▼ [JWT Middleware validates Bearer token]
     │
     ▼
┌─────────────┐
│ Controllers │  ← Route, validate input, call service
└──────┬──────┘
       │
       ▼
┌─────────────┐
│  Services   │  ← Business logic (Interfaces + Implementations)
└──────┬──────┘
       │
       ▼
┌─────────────┐
│  DbContext  │  ← EF Core → MySQL
└─────────────┘
```

### Dependency Injection Registration

All services are **Scoped** (one per HTTP request):

```csharp
// Helpers
builder.Services.AddScoped<JwtHelper>();

// Core
builder.Services.AddScoped<IAcademicYearService, AcademicYearService>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IClassService, ClassService>();

// People
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStaffService, StaffService>();

// Academic Structure
builder.Services.AddScoped<ISectionService, SectionService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IStudentEnrollmentService, StudentEnrollmentService>();
builder.Services.AddScoped<IStaffAssignmentService, StaffAssignmentService>();

// Attendance
builder.Services.AddScoped<IStudentAttendanceService, StudentAttendanceService>();
builder.Services.AddScoped<IStaffAttendanceService, StaffAttendanceService>();

// Auth, Finance, Exams
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFeeService, FeeService>();
builder.Services.AddScoped<IExamService, ExamService>();

// Notifications & Reports
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IReportService, ReportService>();
```

---

## 5. Database Schema & Entities

### BaseEntity (Inherited by all entities)

| Column      | Type        | Description             |
|-------------|-------------|-------------------------|
| `Id`        | INT PK AI   | Primary key             |
| `CreatedAt` | DATETIME    | Auto-set to UTC now     |
| `UpdatedAt` | DATETIME?   | Set on update           |

---

### Entity Relationship Overview

```
ProgramEntity
    └──► Class
              └──► Section
                       ├──► Student
                       └──► Staff (ClassTeacher)

AcademicYear
    ├──► StudentEnrollment (Student × Section × AcademicYear)
    ├──► FeeStructure (Program × Class × AcademicYear)
    └──► Exam (Class × Subject × AcademicYear)

Staff
    ├──► StaffAssignment (Section × Subject)
    └──► StaffAttendance

Student
    ├──► StudentEnrollment
    ├──► StudentAttendance
    ├──► FeePayment
    └──► ExamResult

Exam
    └──► ExamResult (Student × Exam)

User
    └──► Staff (linked staff member)

Notification
    └──► User (CreatedBy)
```

---

### All Entity Tables

| Entity             | Table                 | Key Fields                                          |
|--------------------|----------------------|------------------------------------------------------|
| `ProgramEntity`    | `Programs`           | Name, Description                                   |
| `Class`            | `Classes`            | ProgramId (FK), Name, DisplayOrder                  |
| `Section`          | `Sections`           | ClassId (FK), Name, ClassTeacherId? (FK→Staff)      |
| `Subject`          | `Subjects`           | Name, Code                                          |
| `AcademicYear`     | `AcademicYears`      | Name, StartDate, EndDate, IsActive                  |
| `Student`          | `Students`           | AdmissionNumber (unique), FirstName, SectionId (FK) |
| `Staff`            | `Staff`              | StaffCode, FirstName, Role, Email                   |
| `User`             | `Users`              | Username, Email, PasswordHash, Role, StaffId? (FK)  |
| `StudentEnrollment`| `StudentEnrollments` | StudentId, SectionId, AcademicYearId               |
| `StaffAssignment`  | `StaffAssignments`   | StaffId, SectionId, SubjectId?, IsClassTeacher      |
| `StudentAttendance`| `StudentAttendances` | StudentId, SectionId, Date, Status, MarkedByStaffId |
| `StaffAttendance`  | `StaffAttendances`   | StaffId, Date, CheckIn?, CheckOut?, Status          |
| `FeeStructure`     | `FeeStructures`      | ProgramId, ClassId?, AcademicYearId, FeeType, Amount|
| `FeePayment`       | `FeePayments`        | StudentId, FeeStructureId, Amount, PaymentMode, Status|
| `Exam`             | `Exams`              | Name, Type, ClassId, SubjectId, AcademicYearId, TotalMarks|
| `ExamResult`       | `ExamResults`        | ExamId, StudentId, MarksObtained, Grade, IsAbsent   |
| `Notification`     | `Notifications`      | Title, Body, TargetAudience, IsPublished, CreatedByUserId|

---

## 6. Authentication & Authorization

### How It Works

1. **Register** a user via `POST /api/auth/register` (Admin only)
2. **Login** via `POST /api/auth/login` → receives `accessToken` + `refreshToken`
3. Include the token in every subsequent request:
   ```
   Authorization: Bearer <accessToken>
   ```
4. **Refresh** the token via `POST /api/auth/refresh` when it expires
5. **Logout** to revoke the refresh token

### Roles

| Role      | Access Level                                         |
|-----------|------------------------------------------------------|
| `Admin`   | Full access to all resources including user management |
| `Teacher` | Can mark student attendance and enter exam results   |
| `Staff`   | Can read all data, update own profile                |
| `Parent`  | Read-only access to their child's data               |

### JWT Configuration (`appsettings.json`)

```json
{
  "Jwt": {
    "Key": "SchoolApi_SuperSecretKey_2026_MustBe32CharsOrLonger!",
    "Issuer": "SchoolApi",
    "Audience": "SchoolApiClients",
    "ExpiryMinutes": 60,
    "RefreshExpiryDays": 7
  }
}
```

> ⚠️ Change the `Key` to a strong, random secret in production. Use **User Secrets** or environment variables — never commit real secrets to version control.

---

## 7. API Reference — All Endpoints

> **Base URL (Dev):** `https://localhost:{port}`
> **Swagger UI:** `https://localhost:{port}/swagger`
> **Auth required:** ✅ (all routes except `POST /api/auth/login` and `POST /api/auth/refresh`)

---

### 7.1 Auth API — `api/auth`

| Method  | Endpoint                          | Auth            | Description                      |
|---------|-----------------------------------|-----------------|----------------------------------|
| `POST`  | `/api/auth/login`                 | ❌ None          | Login, get JWT tokens            |
| `POST`  | `/api/auth/register`              | ✅ Admin         | Create new user account          |
| `POST`  | `/api/auth/refresh`               | ❌ None          | Refresh access token             |
| `POST`  | `/api/auth/logout`                | ✅ Any           | Revoke refresh token             |
| `GET`   | `/api/auth/me`                    | ✅ Any           | Get current user info            |
| `POST`  | `/api/auth/change-password`       | ✅ Any           | Change own password              |
| `GET`   | `/api/auth/users`                 | ✅ Admin         | List all users                   |
| `PATCH` | `/api/auth/users/{id}/toggle-status` | ✅ Admin     | Enable / Disable a user          |

**Login Request:**
```json
POST /api/auth/login
{ "email": "admin@school.com", "password": "Admin@123" }
```

**Login Response:**
```json
{
  "accessToken": "eyJhbGc...",
  "refreshToken": "abc123...",
  "expiresAt": "2026-03-17T23:00:00Z",
  "username": "admin",
  "email": "admin@school.com",
  "role": "Admin",
  "userId": 1
}
```

---

### 7.2 Academic Years API — `api/AcademicYears`

| Method   | Endpoint                  | Auth     | Description          |
|----------|---------------------------|----------|----------------------|
| `GET`    | `/api/AcademicYears`      | ✅ Any   | Get all              |
| `GET`    | `/api/AcademicYears/{id}` | ✅ Any   | Get by ID            |
| `POST`   | `/api/AcademicYears`      | ✅ Any   | Create               |
| `PATCH`  | `/api/AcademicYears/{id}` | ✅ Any   | Update               |
| `DELETE` | `/api/AcademicYears/{id}` | ✅ Any   | Delete               |

---

### 7.3 Programs API — `api/programs`

| Method   | Endpoint              | Auth     | Description |
|----------|-----------------------|----------|-------------|
| `GET`    | `/api/programs`       | ✅ Any   | Get all (includes Classes) |
| `GET`    | `/api/programs/{id}`  | ✅ Any   | Get by ID   |
| `POST`   | `/api/programs`       | ✅ Any   | Create      |
| `PATCH`  | `/api/programs/{id}`  | ✅ Any   | Update      |
| `DELETE` | `/api/programs/{id}`  | ✅ Any   | Delete      |

---

### 7.4 Classes API — `api/classes`

| Method   | Endpoint              | Auth     | Description |
|----------|-----------------------|----------|-------------|
| `GET`    | `/api/classes`        | ✅ Any   | Get all (includes Program) |
| `GET`    | `/api/classes/{id}`   | ✅ Any   | Get by ID   |
| `POST`   | `/api/classes`        | ✅ Any   | Create      |
| `PATCH`  | `/api/classes/{id}`   | ✅ Any   | Update      |
| `DELETE` | `/api/classes/{id}`   | ✅ Any   | Delete      |

---

### 7.5 Sections API — `api/sections`

| Method   | Endpoint                        | Auth         | Description        |
|----------|---------------------------------|--------------|--------------------|
| `GET`    | `/api/sections`                 | ✅ Any       | Get all            |
| `GET`    | `/api/sections/{id}`            | ✅ Any       | Get by ID (+students) |
| `GET`    | `/api/sections/class/{classId}` | ✅ Any       | Get by class       |
| `POST`   | `/api/sections`                 | ✅ Admin     | Create             |
| `PATCH`  | `/api/sections/{id}`            | ✅ Admin     | Update             |
| `DELETE` | `/api/sections/{id}`            | ✅ Admin     | Delete             |

---

### 7.6 Subjects API — `api/subjects`

| Method   | Endpoint               | Auth     | Description |
|----------|------------------------|----------|-------------|
| `GET`    | `/api/subjects`        | ✅ Any   | Get all     |
| `GET`    | `/api/subjects/{id}`   | ✅ Any   | Get by ID   |
| `POST`   | `/api/subjects`        | ✅ Admin | Create      |
| `PATCH`  | `/api/subjects/{id}`   | ✅ Admin | Update      |
| `DELETE` | `/api/subjects/{id}`   | ✅ Admin | Delete      |

---

### 7.7 Students API — `api/students`

| Method   | Endpoint                            | Auth      | Description          |
|----------|-------------------------------------|-----------|----------------------|
| `GET`    | `/api/students`                     | ✅ Any    | Get all (with Section/Class/Program)|
| `GET`    | `/api/students/{id}`                | ✅ Any    | Get by ID            |
| `GET`    | `/api/students/section/{sectionId}` | ✅ Any    | Get by section       |
| `POST`   | `/api/students`                     | ✅ Admin  | Admit new student    |
| `PATCH`  | `/api/students/{id}`                | ✅ Admin  | Update details       |
| `DELETE` | `/api/students/{id}`                | ✅ Admin  | Remove student       |

**Create Student Request:**
```json
POST /api/students
{
  "admissionNumber": "STU-2026-001",
  "firstName": "Rahul",
  "lastName": "Sharma",
  "gender": "Male",
  "dateOfBirth": "2010-05-15",
  "admissionDate": "2026-06-01",
  "sectionId": 1,
  "phoneNumber": "9876543210"
}
```

---

### 7.8 Staff API — `api/staff`

| Method   | Endpoint           | Auth      | Description        |
|----------|--------------------|-----------|--------------------|
| `GET`    | `/api/staff`       | ✅ Any    | Get all staff      |
| `GET`    | `/api/staff/{id}`  | ✅ Any    | Get by ID          |
| `POST`   | `/api/staff`       | ✅ Admin  | Add new staff      |
| `PATCH`  | `/api/staff/{id}`  | ✅ Admin  | Update details     |
| `DELETE` | `/api/staff/{id}`  | ✅ Admin  | Remove staff       |

---

### 7.9 Student Enrollments API — `api/enrollments`

| Method   | Endpoint                                     | Auth     | Description                  |
|----------|----------------------------------------------|----------|------------------------------|
| `GET`    | `/api/enrollments`                           | ✅ Any   | Get all enrollments          |
| `GET`    | `/api/enrollments/{id}`                      | ✅ Any   | Get by ID                    |
| `GET`    | `/api/enrollments/student/{studentId}`       | ✅ Any   | All enrollments for a student|
| `GET`    | `/api/enrollments/academic-year/{yearId}`    | ✅ Any   | All enrollments in a year    |
| `GET`    | `/api/enrollments/section/{sectionId}`       | ✅ Any   | All students in a section    |
| `POST`   | `/api/enrollments`                           | ✅ Admin | Enroll a student             |
| `DELETE` | `/api/enrollments/{id}`                      | ✅ Admin | Remove enrollment            |

---

### 7.10 Staff Assignments API — `api/staff-assignments`

| Method   | Endpoint                                       | Auth     | Description                   |
|----------|------------------------------------------------|----------|-------------------------------|
| `GET`    | `/api/staff-assignments`                       | ✅ Any   | All assignments               |
| `GET`    | `/api/staff-assignments/{id}`                  | ✅ Any   | Get by ID                     |
| `GET`    | `/api/staff-assignments/staff/{staffId}`       | ✅ Any   | Assignments for a staff member|
| `GET`    | `/api/staff-assignments/section/{sectionId}`   | ✅ Any   | Teachers of a section         |
| `POST`   | `/api/staff-assignments`                       | ✅ Admin | Create assignment             |
| `PATCH`  | `/api/staff-assignments/{id}`                  | ✅ Admin | Update assignment             |
| `DELETE` | `/api/staff-assignments/{id}`                  | ✅ Admin | Remove assignment             |

---

### 7.11 Student Attendance API — `api/student-attendance`

| Method   | Endpoint                                          | Auth               | Description                     |
|----------|---------------------------------------------------|--------------------|---------------------------------|
| `GET`    | `/api/student-attendance`                         | ✅ Any             | All records                     |
| `GET`    | `/api/student-attendance/{id}`                    | ✅ Any             | Get by ID                       |
| `GET`    | `/api/student-attendance/student/{studentId}`     | ✅ Any             | History for a student           |
| `GET`    | `/api/student-attendance/section/{id}/date/{date}`| ✅ Any             | Section attendance by date      |
| `POST`   | `/api/student-attendance`                         | ✅ Admin, Teacher  | Mark single attendance          |
| `POST`   | `/api/student-attendance/bulk`                    | ✅ Admin, Teacher  | Bulk mark for whole section     |
| `PATCH`  | `/api/student-attendance/{id}`                    | ✅ Admin, Teacher  | Correct attendance status       |
| `DELETE` | `/api/student-attendance/{id}`                    | ✅ Admin           | Delete record                   |

**Bulk Attendance Request:**
```json
POST /api/student-attendance/bulk
{
  "sectionId": 1,
  "date": "2026-03-17",
  "markedByStaffId": 5,
  "entries": [
    { "studentId": 1, "status": "Present" },
    { "studentId": 2, "status": "Absent" },
    { "studentId": 3, "status": "Leave" }
  ]
}
```

> **Note:** Submitting bulk attendance for a section/date that already has records will **replace** the existing records.

---

### 7.12 Staff Attendance API — `api/staff-attendance`

| Method   | Endpoint                              | Auth     | Description                   |
|----------|---------------------------------------|----------|-------------------------------|
| `GET`    | `/api/staff-attendance`               | ✅ Any   | Get all records               |
| `GET`    | `/api/staff-attendance/{id}`          | ✅ Any   | Get by ID                     |
| `GET`    | `/api/staff-attendance/staff/{staffId}`| ✅ Any  | History for a staff member    |
| `GET`    | `/api/staff-attendance/date/{date}`   | ✅ Any   | All staff attendance on a date|
| `POST`   | `/api/staff-attendance`               | ✅ Admin | Mark attendance               |
| `PATCH`  | `/api/staff-attendance/{id}`          | ✅ Admin | Update (e.g., add checkout)   |
| `DELETE` | `/api/staff-attendance/{id}`          | ✅ Admin | Delete record                 |

**Status values:** `Present`, `Absent`, `Leave`, `HalfDay`

---

### 7.13 Fees API — `api/fees`

#### Fee Structures

| Method   | Endpoint                                      | Auth     | Description               |
|----------|-----------------------------------------------|----------|---------------------------|
| `GET`    | `/api/fees/structures`                        | ✅ Any   | All fee structures        |
| `GET`    | `/api/fees/structures/{id}`                   | ✅ Any   | Get by ID                 |
| `GET`    | `/api/fees/structures/program/{programId}`    | ✅ Any   | By program                |
| `POST`   | `/api/fees/structures`                        | ✅ Admin | Create fee structure      |
| `PATCH`  | `/api/fees/structures/{id}`                   | ✅ Admin | Update                    |
| `DELETE` | `/api/fees/structures/{id}`                   | ✅ Admin | Delete                    |

#### Fee Payments

| Method   | Endpoint                                   | Auth     | Description               |
|----------|--------------------------------------------|----------|---------------------------|
| `GET`    | `/api/fees/payments`                       | ✅ Any   | All payments              |
| `GET`    | `/api/fees/payments/{id}`                  | ✅ Any   | Get by ID                 |
| `GET`    | `/api/fees/payments/student/{studentId}`   | ✅ Any   | Student payment history   |
| `GET`    | `/api/fees/payments/pending`               | ✅ Any   | Pending/partial payments  |
| `POST`   | `/api/fees/payments`                       | ✅ Admin | Record payment            |
| `PATCH`  | `/api/fees/payments/{id}`                  | ✅ Admin | Update payment            |
| `DELETE` | `/api/fees/payments/{id}`                  | ✅ Admin | Delete payment            |

**Fee Types:** `Tuition`, `Library`, `Lab`, `Transport`, `Exam`
**Payment Modes:** `Cash`, `Online`, `Cheque`, `BankTransfer`
**Payment Status:** `Paid`, `Partial`, `Pending`

---

### 7.14 Exams & Results API — `api/exams`

#### Exams

| Method   | Endpoint                                  | Auth     | Description             |
|----------|-------------------------------------------|----------|-------------------------|
| `GET`    | `/api/exams`                              | ✅ Any   | All exams               |
| `GET`    | `/api/exams/{id}`                         | ✅ Any   | Get by ID               |
| `GET`    | `/api/exams/class/{classId}`              | ✅ Any   | Exams for a class       |
| `GET`    | `/api/exams/academic-year/{yearId}`       | ✅ Any   | Exams in a year         |
| `POST`   | `/api/exams`                              | ✅ Admin | Create exam             |
| `PATCH`  | `/api/exams/{id}`                         | ✅ Admin | Update exam             |
| `DELETE` | `/api/exams/{id}`                         | ✅ Admin | Delete exam             |

#### Results

| Method   | Endpoint                                  | Auth               | Description              |
|----------|-------------------------------------------|--------------------|--------------------------|
| `GET`    | `/api/exams/{examId}/results`             | ✅ Any             | All results for an exam  |
| `GET`    | `/api/exams/results/student/{studentId}`  | ✅ Any             | All results for a student|
| `POST`   | `/api/exams/results`                      | ✅ Admin, Teacher  | Add single result        |
| `POST`   | `/api/exams/results/bulk`                 | ✅ Admin, Teacher  | Bulk enter results       |
| `PATCH`  | `/api/exams/results/{id}`                 | ✅ Admin, Teacher  | Update result            |
| `DELETE` | `/api/exams/results/{id}`                 | ✅ Admin           | Delete result            |

**Exam Types:** `UnitTest`, `MidTerm`, `Final`, `Internal`

**Bulk Result Request:**
```json
POST /api/exams/results/bulk
{
  "examId": 1,
  "results": [
    { "studentId": 1, "marksObtained": 85, "grade": "A", "isAbsent": false },
    { "studentId": 2, "marksObtained": 0,  "grade": null, "isAbsent": true }
  ]
}
```

---

### 7.15 Notifications API — `api/notifications`

| Method   | Endpoint                              | Auth     | Description               |
|----------|---------------------------------------|----------|---------------------------|
| `GET`    | `/api/notifications`                  | ✅ Any   | All notifications         |
| `GET`    | `/api/notifications/{id}`             | ✅ Any   | Get by ID                 |
| `GET`    | `/api/notifications/audience/{aud}`   | ✅ Any   | By audience (published only)|
| `POST`   | `/api/notifications`                  | ✅ Admin | Create notification       |
| `PATCH`  | `/api/notifications/{id}`             | ✅ Admin | Update notification       |
| `DELETE` | `/api/notifications/{id}`             | ✅ Admin | Delete notification       |

**Target Audiences:** `All`, `Students`, `Staff`, `Parents`

---

### 7.16 Reports API — `api/reports`

| Method | Endpoint                                                       | Auth        | Description                      |
|--------|----------------------------------------------------------------|-------------|----------------------------------|
| `GET`  | `/api/reports/attendance/student/{studentId}`                  | ✅ Any      | Student attendance summary       |
| `GET`  | `/api/reports/attendance/section/{sectionId}/date/{date}`      | ✅ Any      | Section report on a date         |
| `GET`  | `/api/reports/attendance/section/{sectionId}/range?from=&to=`  | ✅ Any      | Section attendance over a range  |
| `GET`  | `/api/reports/attendance/staff/{staffId}`                      | ✅ Any      | Staff attendance summary         |
| `GET`  | `/api/reports/exams/{examId}/results`                          | ✅ Any      | Full exam result report          |
| `GET`  | `/api/reports/students/{studentId}/results`                    | ✅ Any      | All results for a student        |
| `GET`  | `/api/reports/fees/academic-year/{yearId}`                     | ✅ Admin    | Fee collection summary           |
| `GET`  | `/api/reports/fees/student/{studentId}/academic-year/{yearId}` | ✅ Any      | Student fee dues/paid summary    |

**Query Params for Attendance (optional):** `?from=2026-01-01&to=2026-03-31`

---

## 8. Roles & Permissions

| Endpoint / Action          | Admin | Teacher | Staff | Parent |
|----------------------------|:-----:|:-------:|:-----:|:------:|
| Login / Refresh            | ✅    | ✅      | ✅    | ✅     |
| Register User              | ✅    | ❌      | ❌    | ❌     |
| Toggle User Status         | ✅    | ❌      | ❌    | ❌     |
| Create Student/Staff       | ✅    | ❌      | ❌    | ❌     |
| Read Students/Staff        | ✅    | ✅      | ✅    | ✅     |
| Mark Attendance            | ✅    | ✅      | ❌    | ❌     |
| Enter Exam Results         | ✅    | ✅      | ❌    | ❌     |
| Manage Fees                | ✅    | ❌      | ❌    | ❌     |
| Create Notifications       | ✅    | ❌      | ❌    | ❌     |
| View Reports               | ✅    | ✅      | ✅    | ✅     |
| View Fee Collection Report | ✅    | ❌      | ❌    | ❌     |

---

## 9. Configuration & Setup

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- MySQL Server 8.x
- Visual Studio 2022+ or VS Code

### Step-by-Step Setup

```bash
# 1. Clone the project
git clone <repository-url>
cd SchoolApi

# 2. Set database connection (edit appsettings.json or use secrets)
dotnet user-secrets set "ConnectionStrings:DefaultConnection" \
  "server=localhost;database=school_db;user=root;password=yourpassword"

# 3. Set JWT Key (important in production!)
dotnet user-secrets set "Jwt:Key" "YourRandomSecretKeyAtLeast32Chars!"

# 4. Run migrations to create all database tables
dotnet ef database update

# 5. Run the application
dotnet run

# 6. Open Swagger UI
# Navigate to: https://localhost:{port}/swagger
```

### First-Time Setup

Since all endpoints require auth, you'll need to seed an admin user directly in the database to bootstrap:

```sql
-- Run in MySQL after applying migrations
INSERT INTO Users (Username, Email, PasswordHash, Role, IsActive, CreatedAt)
VALUES (
  'admin',
  'admin@school.com',
  '$2a$11$...', -- BCrypt hash of your password
  'Admin',
  1,
  NOW()
);
```

Or add a seed endpoint in development mode.

---

## 10. Docker Support

```dockerfile
# Build and run
docker build -t school-api .

docker run -d -p 8080:8080 \
  -e ConnectionStrings__DefaultConnection="server=host.docker.internal;database=school_db;user=root;password=root" \
  -e Jwt__Key="YourProductionSecretKey32CharsPlus!" \
  -e Jwt__Issuer="SchoolApi" \
  -e Jwt__Audience="SchoolApiClients" \
  --name school-api \
  school-api
```

Exposed ports: `8080` (HTTP), `8081` (HTTPS)

---

## 11. EF Core Migrations

```bash
# Add a new migration
dotnet ef migrations add <MigrationName>

# Apply to database
dotnet ef database update

# Rollback one migration
dotnet ef database update <PreviousMigrationName>

# Remove last unapplied migration
dotnet ef migrations remove

# List migrations
dotnet ef migrations list

# Generate SQL script
dotnet ef migrations script
```

### Migration History

| Migration Name            | Description                                      |
|---------------------------|--------------------------------------------------|
| `Initial` (or previous)   | Programs, Classes, Sections, Subjects, Students  |
| `AddFullSchoolManagement` | Users, FeeStructure, FeePayment, Exam, ExamResult, Notification |

---

## 12. Coding Conventions

| Convention         | Standard                                                              |
|--------------------|-----------------------------------------------------------------------|
| Naming             | PascalCase for classes/methods; camelCase for local vars              |
| Async              | All service and controller methods use `async Task<>`                 |
| HTTP Verbs         | GET=read, POST=create, PATCH=partial update, DELETE=remove            |
| Not Found          | Returns `NotFound()` when entity not found by ID                      |
| Timestamps         | `CreatedAt` set at creation; `UpdatedAt` set in `UpdateAsync()`       |
| DTOs               | Separate Create and Update DTOs per entity                            |
| Namespace          | Matches folder path: `SchoolApi.Controllers`, etc.                    |
| EF Configs         | Entity constraints defined via Fluent API in `Configurations/`        |
| JSON Cycles        | `ReferenceHandler.IgnoreCycles` prevents circular serialization issues|
| Auth               | `[Authorize]` on controllers; `[Authorize(Roles="...")]` on endpoints |

---

## 13. NuGet Packages

| Package                                         | Version  | Purpose                               |
|-------------------------------------------------|----------|---------------------------------------|
| `Microsoft.EntityFrameworkCore`                 | 9.0.0    | ORM core                              |
| `Microsoft.EntityFrameworkCore.Design`          | 9.0.0    | EF migrations design-time tooling     |
| `Microsoft.EntityFrameworkCore.Tools`           | 9.0.0    | EF CLI tools                          |
| `Pomelo.EntityFrameworkCore.MySql`              | 9.0.0    | MySQL database provider               |
| `Microsoft.AspNetCore.Authentication.JwtBearer` | 9.0.0    | JWT middleware                        |
| `BCrypt.Net-Next`                               | 4.0.3    | Password hashing                      |
| `Swashbuckle.AspNetCore`                        | 10.1.5   | Swagger / OpenAPI UI                  |
| `Microsoft.VisualStudio.Azure.Containers.Tools` | 1.22.1   | Docker/container tooling for VS       |

---

*Documentation version 2.0 — Full Implementation*
*SchoolApi Development Team — March 2026*

# SchoolApi Documentation

> **Version:** 1.0 (In Progress)
> **Framework:** ASP.NET Core 9.0
> **Database:** MySQL (via Pomelo EF Core)
> **Last Updated:** March 2026

---

## Table of Contents

1. [Project Overview](#1-project-overview)
2. [Technology Stack](#2-technology-stack)
3. [Project Structure](#3-project-structure)
4. [Architecture](#4-architecture)
5. [Database Schema](#5-database-schema)
6. [Implemented APIs](#6-implemented-apis)
   - [Academic Years](#61-academic-years-api)
   - [Programs](#62-programs-api)
   - [Classes](#63-classes-api)
7. [DTOs Reference](#7-dtos-reference)
8. [Configuration & Setup](#8-configuration--setup)
9. [Docker Support](#9-docker-support)
10. [Planned Features (Not Yet Developed)](#10-planned-features-not-yet-developed)
    - [Student Management](#101-student-management)
    - [Staff Management](#102-staff-management)
    - [Section Management](#103-section-management)
    - [Subject Management](#104-subject-management)
    - [Student Enrollment](#105-student-enrollment)
    - [Staff Assignment](#106-staff-assignment)
    - [Student Attendance](#107-student-attendance)
    - [Staff Attendance](#108-staff-attendance)
    - [Authentication & Authorization](#109-authentication--authorization)
    - [Reporting Module](#1010-reporting-module)
    - [Fee Management](#1011-fee-management)
    - [Exam & Results](#1012-exam--results)
    - [Notifications](#1013-notifications)
11. [Coding Conventions](#11-coding-conventions)
12. [EF Core Migrations](#12-ef-core-migrations)

---

## 1. Project Overview

**SchoolApi** is a RESTful Web API backend built with ASP.NET Core 9.0, designed to power a complete School/Institution Management System. The API manages all core entities of a school — Programs, Classes, Sections, Students, Staff, Attendance, Enrollments — with full CRUD support.

The project follows a clean **Controller → Service → Repository (EF Core DbContext)** layered architecture, promoting separation of concerns, testability, and scalability.

---

## 2. Technology Stack

| Component             | Technology                              |
|-----------------------|-----------------------------------------|
| Framework             | ASP.NET Core 9.0 Web API               |
| Language              | C# (.NET 9)                            |
| ORM                   | Entity Framework Core 9.0              |
| Database              | MySQL 8.x                              |
| MySQL EF Provider     | Pomelo.EntityFrameworkCore.MySql 9.0.0 |
| API Documentation     | Swagger (Swashbuckle.AspNetCore 10.x)  |
| Containerization      | Docker (Linux containers)              |
| IDE                   | Visual Studio / VS Code                |

### NuGet Packages

| Package                                         | Version  | Purpose                            |
|-------------------------------------------------|----------|------------------------------------|
| `Microsoft.EntityFrameworkCore`                 | 9.0.0    | ORM core                           |
| `Microsoft.EntityFrameworkCore.Design`          | 9.0.0    | EF migrations design-time tooling  |
| `Microsoft.EntityFrameworkCore.Tools`           | 9.0.0    | EF CLI tools                       |
| `Pomelo.EntityFrameworkCore.MySql`              | 9.0.0    | MySQL database provider            |
| `Swashbuckle.AspNetCore`                        | 10.1.5   | Swagger / OpenAPI UI               |
| `Microsoft.VisualStudio.Azure.Containers.Tools` | 1.22.1   | Docker/container tooling for VS    |

---

## 3. Project Structure

```
SchoolApi/
├── Controllers/                        # API controllers (HTTP layer)
│   ├── AcademicYearsController.cs
│   ├── ClassesController.cs
│   └── ProgramsController.cs
│
├── DTOs/                               # Data Transfer Objects (request/response shapes)
│   ├── AcademicYear/
│   │   ├── CreateAcademicYearDto.cs
│   │   └── UpdateAcademicYearDto.cs
│   ├── Classes/
│   │   ├── CreateClassDto.cs
│   │   └── UpdateClassDto.cs
│   └── Programs/
│       ├── CreateProgramDto.cs
│       └── UpdateProgramDto.cs
│
├── Models/
│   ├── Data/
│   │   ├── Configurations/             # EF Core Fluent API configurations
│   │   │   ├── ClassConfiguration.cs
│   │   │   ├── SectionConfiguration.cs
│   │   │   ├── StaffConfiguration.cs
│   │   │   └── StudentConfiguration.cs
│   │   └── SchoolDbContext.cs          # EF Core DbContext
│   └── Entities/                       # Domain / Database models
│       ├── BaseEntity.cs
│       ├── AcademicYear.cs
│       ├── Class.cs
│       ├── Program.cs (ProgramEntity)
│       ├── Section.cs
│       ├── Staff.cs
│       ├── StaffAssignment.cs
│       ├── StaffAttendance.cs
│       ├── Student.cs
│       ├── StudentAttendance.cs
│       ├── StudentEnrollment.cs
│       └── Subject.cs
│
├── Services/
│   ├── Implementations/                # Concrete service logic
│   │   ├── AcademicYearService.cs
│   │   ├── ClassService.cs
│   │   └── ProgramService.cs
│   └── Interfaces/                     # Service contracts/abstractions
│       ├── IAcademicYearService.cs
│       ├── IClassService.cs
│       └── IProgramService.cs
│
├── Migrations/                         # EF Core migration files
├── Properties/
│   └── launchSettings.json
├── appsettings.json                    # App configuration (DB connection, logging)
├── appsettings.Development.json        # Dev-specific overrides
├── Dockerfile                          # Docker container definition
├── Program.cs                          # App bootstrap and DI configuration
└── SchoolApi.csproj                    # Project file
```

---

## 4. Architecture

The project uses a **3-layer architecture**:

```
HTTP Request
     │
     ▼
┌─────────────┐
│ Controllers │  ← Receives HTTP requests, validates input, delegates to Service
└──────┬──────┘
       │
       ▼
┌─────────────┐
│  Services   │  ← Business logic layer (Interfaces + Implementations)
└──────┬──────┘
       │
       ▼
┌─────────────┐
│  DbContext  │  ← EF Core talks to MySQL database
└─────────────┘
```

### Dependency Injection Setup (`Program.cs`)

All services are registered as **Scoped** (per HTTP request):

```csharp
builder.Services.AddScoped<IAcademicYearService, AcademicYearService>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IClassService, ClassService>();
```

EF Core `DbContext` is configured to use MySQL with auto server-version detection:

```csharp
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(connectionString)
    )
);
```

---

## 5. Database Schema

### BaseEntity (Abstract — all entities inherit this)

| Column      | Type         | Description                     |
|-------------|--------------|----------------------------------|
| `Id`        | INT (PK, AI) | Primary Key, auto-increment      |
| `CreatedAt` | DATETIME     | Defaults to `DateTime.UtcNow`    |
| `UpdatedAt` | DATETIME?    | Nullable, set on update          |

---

### ProgramEntity (Table: `Programs`)

| Column        | Type         | Description                          |
|---------------|--------------|--------------------------------------|
| `Id`          | INT (PK)     | Inherited from BaseEntity            |
| `Name`        | VARCHAR      | Name of the program (e.g., "Science")|
| `Description` | TEXT         | Description of the program           |

**Relationships:**
- Has many → `Classes`

---

### Class (Table: `Classes`)

| Column         | Type     | Description                              |
|----------------|----------|------------------------------------------|
| `Id`           | INT (PK) | Primary Key                              |
| `ProgramId`    | INT (FK) | Foreign key to `Programs`                |
| `Name`         | VARCHAR  | Class name (e.g., "Class 10")            |
| `DisplayOrder` | INT      | For sorting/display purposes             |

**Relationships:**
- Belongs to → `ProgramEntity`
- Has many → `Sections`

---

### Section (Table: `Sections`)

| Column           | Type      | Description                             |
|------------------|-----------|-----------------------------------------|
| `Id`             | INT (PK)  | Primary Key                             |
| `ClassId`        | INT (FK)  | FK to `Classes`                         |
| `Name`           | VARCHAR   | Section name (e.g., "A", "B")           |
| `ClassTeacherId` | INT? (FK) | Nullable FK to `Staff` (class teacher)  |

**Relationships:**
- Belongs to → `Class`
- Belongs to → `Staff` (ClassTeacher, optional)
- Has many → `Students`

---

### Student (Table: `Students`)

| Column            | Type     | Description                         |
|-------------------|----------|-------------------------------------|
| `Id`              | INT (PK) | Primary Key                         |
| `AdmissionNumber` | VARCHAR(50) | Unique admission number          |
| `FirstName`       | VARCHAR(100) | Student first name              |
| `LastName`        | VARCHAR  | Student last name                   |
| `Gender`          | VARCHAR  | Gender                              |
| `DateOfBirth`     | DATETIME | Date of birth                       |
| `AdmissionDate`   | DATETIME | Date of admission                   |
| `SectionId`       | INT (FK) | FK to `Sections`                    |
| `PhoneNumber`     | VARCHAR  | Contact number                      |

**Constraints:**
- `AdmissionNumber` is **required**, max 50 chars, **unique index**
- `FirstName` is **required**, max 100 chars

**Relationships:**
- Belongs to → `Section`

---

### Staff (Table: `Staff`)

| Column        | Type     | Description                      |
|---------------|----------|----------------------------------|
| `Id`          | INT (PK) | Primary Key                      |
| `StaffCode`   | VARCHAR  | Unique staff identifier code     |
| `FirstName`   | VARCHAR  | First name                       |
| `LastName`    | VARCHAR  | Last name                        |
| `Role`        | VARCHAR  | Role (e.g., "Teacher", "Admin")  |
| `Phone`       | VARCHAR  | Phone number                     |
| `Email`       | VARCHAR  | Email address                    |
| `JoiningDate` | DATETIME | Date of joining                  |

**Relationships:**
- Has many → `Sections` (as ClassTeacher)

---

### Subject (Table: `Subjects`)

| Column | Type     | Description          |
|--------|----------|----------------------|
| `Id`   | INT (PK) | Primary Key          |
| `Name` | VARCHAR  | Subject name         |
| `Code` | VARCHAR  | Subject code         |

---

### AcademicYear (Table: `AcademicYears`)

| Column      | Type     | Description                       |
|-------------|----------|-----------------------------------|
| `Id`        | INT (PK) | Primary Key                       |
| `Name`      | VARCHAR  | E.g., "2025-2026"                 |
| `StartDate` | DATETIME | Academic year start date          |
| `EndDate`   | DATETIME | Academic year end date            |
| `IsActive`  | BIT      | Whether this year is currently active |

**Relationships:**
- Has many → `StudentEnrollments`

---

### StudentEnrollment (Table: `StudentEnrollments`)

| Column           | Type     | Description                     |
|------------------|----------|---------------------------------|
| `Id`             | INT (PK) | Primary Key                     |
| `StudentId`      | INT (FK) | FK to `Students`                |
| `SectionId`      | INT (FK) | FK to `Sections`                |
| `AcademicYearId` | INT (FK) | FK to `AcademicYears`           |

**Relationships:**
- Belongs to → `Student`, `Section`, `AcademicYear`

---

### StudentAttendance (Table: `StudentAttendances`)

| Column            | Type     | Description                               |
|-------------------|----------|-------------------------------------------|
| `Id`              | INT (PK) | Primary Key                               |
| `StudentId`       | INT (FK) | FK to `Students`                          |
| `SectionId`       | INT (FK) | FK to `Sections`                          |
| `Date`            | DATETIME | Attendance date                           |
| `Status`          | VARCHAR  | `"Present"` / `"Absent"` / `"Leave"`      |
| `MarkedByStaffId` | INT (FK) | FK to `Staff` (who marked the attendance) |

**Relationships:**
- Belongs to → `Student`, `Section`, `Staff`

---

### StaffAttendance (Table: `StaffAttendances`)

| Column        | Type      | Description                       |
|---------------|-----------|-----------------------------------|
| `Id`          | INT (PK)  | Primary Key                       |
| `StaffId`     | INT (FK)  | FK to `Staff`                     |
| `Date`        | DATETIME  | Attendance date                   |
| `CheckInTime` | TIMESPAN? | Optional check-in time            |
| `CheckOutTime`| TIMESPAN? | Optional check-out time           |
| `Status`      | VARCHAR   | Attendance status                 |

**Relationships:**
- Belongs to → `Staff`

---

### StaffAssignment (Table: `StaffAssignments`)

| Column          | Type      | Description                               |
|-----------------|-----------|-------------------------------------------|
| `Id`            | INT (PK)  | Primary Key                               |
| `StaffId`       | INT (FK)  | FK to `Staff`                             |
| `SectionId`     | INT (FK)  | FK to `Sections`                          |
| `SubjectId`     | INT? (FK) | Optional FK to `Subjects`                 |
| `IsClassTeacher`| BIT       | Whether staff is the class teacher        |

**Relationships:**
- Belongs to → `Staff`, `Section`, `Subject`

---

### Entity Relationship Diagram

```
ProgramEntity
    └── has many ──► Class
                       └── has many ──► Section
                                           ├── has many ──► Student
                                           └── has one  ──► Staff (ClassTeacher)

AcademicYear
    └── has many ──► StudentEnrollment
                         ├── belongs to ──► Student
                         └── belongs to ──► Section

StaffAssignment
    ├── belongs to ──► Staff
    ├── belongs to ──► Section
    └── belongs to ──► Subject

StudentAttendance
    ├── belongs to ──► Student
    ├── belongs to ──► Section
    └── belongs to ──► Staff (MarkedBy)

StaffAttendance
    └── belongs to ──► Staff
```

---

## 6. Implemented APIs

> **Base URL (Development):** `https://localhost:{port}`

> **Swagger UI:** `https://localhost:{port}/swagger`

---

### 6.1 Academic Years API

**Route:** `api/AcademicYears`

| Method   | Endpoint                  | Description                        | Request Body              | Responses         |
|----------|---------------------------|------------------------------------|---------------------------|-------------------|
| `GET`    | `/api/AcademicYears`      | Get all academic years             | None                      | `200 OK` + list   |
| `GET`    | `/api/AcademicYears/{id}` | Get single academic year by ID     | None                      | `200 OK` / `404`  |
| `POST`   | `/api/AcademicYears`      | Create a new academic year         | `CreateAcademicYearDto`   | `200 OK` + entity |
| `PATCH`  | `/api/AcademicYears/{id}` | Update an existing academic year   | `UpdateAcademicYearDto`   | `200 OK` / `404`  |
| `DELETE` | `/api/AcademicYears/{id}` | Delete an academic year by ID      | None                      | `200 OK` / `404`  |

**Example Request — Create Academic Year:**
```json
POST /api/AcademicYears
{
  "name": "2025-2026",
  "startDate": "2025-06-01T00:00:00Z",
  "endDate": "2026-03-31T00:00:00Z",
  "isActive": true
}
```

---

### 6.2 Programs API

**Route:** `api/programs`

| Method   | Endpoint              | Description              | Request Body        | Responses         |
|----------|-----------------------|--------------------------|---------------------|-------------------|
| `GET`    | `/api/programs`       | Get all programs         | None                | `200 OK` + list   |
| `GET`    | `/api/programs/{id}`  | Get program by ID        | None                | `200 OK` / `404`  |
| `POST`   | `/api/programs`       | Create a new program     | `CreateProgramDto`  | `200 OK` + entity |
| `PATCH`  | `/api/programs/{id}`  | Update program by ID     | `UpdateProgramDto`  | `200 OK` / `404`  |
| `DELETE` | `/api/programs/{id}`  | Delete program by ID     | None                | `200 OK` / `404`  |

> **Note:** Program GET responses include the list of associated `Classes` (eager-loaded via `Include`).

**Example Request — Create Program:**
```json
POST /api/programs
{
  "name": "Science",
  "description": "Science stream with Physics, Chemistry, Biology"
}
```

---

### 6.3 Classes API

**Route:** `api/classes`

| Method   | Endpoint              | Description             | Request Body      | Responses         |
|----------|-----------------------|-------------------------|-------------------|-------------------|
| `GET`    | `/api/classes`        | Get all classes         | None              | `200 OK` + list   |
| `GET`    | `/api/classes/{id}`   | Get class by ID         | None              | `200 OK` / `404`  |
| `POST`   | `/api/classes`        | Create a new class      | `CreateClassDto`  | `200 OK` + entity |
| `PATCH`  | `/api/classes/{id}`   | Update class by ID      | `UpdateClassDto`  | `200 OK` / `404`  |
| `DELETE` | `/api/classes/{id}`   | Delete class by ID      | None              | `200 OK` / `404`  |

> **Note:** Class GET responses include the associated `Program` (eager-loaded via `Include`).

**Example Request — Create Class:**
```json
POST /api/classes
{
  "programId": 1,
  "name": "Class 10",
  "displayOrder": 10
}
```

---

## 7. DTOs Reference

### AcademicYear DTOs

**`CreateAcademicYearDto` / `UpdateAcademicYearDto`**

| Field       | Type     | Required |
|-------------|----------|----------|
| `Name`      | `string` | ✅       |
| `StartDate` | `DateTime`| ✅      |
| `EndDate`   | `DateTime`| ✅      |
| `IsActive`  | `bool`   | ✅       |

---

### Program DTOs

**`CreateProgramDto` / `UpdateProgramDto`**

| Field         | Type     | Required |
|---------------|----------|----------|
| `Name`        | `string` | ✅       |
| `Description` | `string` | ✅       |

---

### Class DTOs

**`CreateClassDto` / `UpdateClassDto`**

| Field          | Type     | Required |
|----------------|----------|----------|
| `ProgramId`    | `int`    | ✅       |
| `Name`         | `string` | ✅       |
| `DisplayOrder` | `int`    | ✅       |

---

## 8. Configuration & Setup

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- MySQL Server 8.x (running locally or remote)
- Visual Studio 2022 / VS Code (with C# extension)

### Environment Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd SchoolApi
   ```

2. **Configure the database connection**

   Edit `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "server=localhost;database=school_db;user=root;password=yourpassword"
     }
   }
   ```
   
   > ⚠️ **Never commit passwords to source control.** Use `appsettings.Development.json` or **User Secrets** for local dev credentials.

3. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

4. **Apply EF Core Migrations** (create/update the database schema)
   ```bash
   dotnet ef database update
   ```

5. **Run the API**
   ```bash
   dotnet run
   ```

6. **Open Swagger UI**
   
   Navigate to: `https://localhost:{port}/swagger`

### User Secrets (Recommended for Development)

Instead of putting credentials in `appsettings.json`, use user secrets:
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=localhost;database=school_db;user=root;password=yourpassword"
```

---

## 9. Docker Support

The project includes a multi-stage `Dockerfile` targeting Linux containers.

### Dockerfile Stages

| Stage     | Base Image                        | Purpose                          |
|-----------|-----------------------------------|----------------------------------|
| `base`    | `mcr.microsoft.com/dotnet/aspnet:9.0` | Runtime base image           |
| `build`   | `mcr.microsoft.com/dotnet/sdk:9.0`    | Restore & build                |
| `publish` | `build`                               | Publish the release artifact   |
| `final`   | `base`                                | Final production image         |

**Exposed Ports:**
- `8080` (HTTP)
- `8081` (HTTPS)

### Build & Run with Docker

```bash
# Build the Docker image
docker build -t school-api .

# Run the container
docker run -d -p 8080:8080 \
  -e ConnectionStrings__DefaultConnection="server=host.docker.internal;database=school_db;user=root;password=root" \
  --name school-api \
  school-api
```

---

## 10. Planned Features (Not Yet Developed)

This section documents all major modules and APIs that are **defined in the data model but not yet implemented** with controllers, services, or business logic. These represent the development roadmap.

---

### 10.1 Student Management

**Status:** ❌ Not Implemented (Entity exists, no Controller/Service)

**Planned Endpoints:**

| Method   | Endpoint                          | Description                         |
|----------|-----------------------------------|-------------------------------------|
| `GET`    | `/api/students`                   | Get all students (with pagination)  |
| `GET`    | `/api/students/{id}`              | Get student by ID                   |
| `GET`    | `/api/students/section/{sectionId}` | Get all students in a section     |
| `POST`   | `/api/students`                   | Admit a new student                 |
| `PATCH`  | `/api/students/{id}`              | Update student details              |
| `DELETE` | `/api/students/{id}`              | Remove a student                    |

**Planned DTOs:**
```csharp
// CreateStudentDto
{
  string AdmissionNumber,
  string FirstName,
  string LastName,
  string Gender,
  DateTime DateOfBirth,
  DateTime AdmissionDate,
  int SectionId,
  string PhoneNumber
}
```

**Required Files to Create:**
- `DTOs/Students/CreateStudentDto.cs`
- `DTOs/Students/UpdateStudentDto.cs`
- `Services/Interfaces/IStudentService.cs`
- `Services/Implementations/StudentService.cs`
- `Controllers/StudentsController.cs`

---

### 10.2 Staff Management

**Status:** ❌ Not Implemented (Entity exists, no Controller/Service)

**Planned Endpoints:**

| Method   | Endpoint               | Description                       |
|----------|------------------------|-----------------------------------|
| `GET`    | `/api/staff`           | Get all staff members             |
| `GET`    | `/api/staff/{id}`      | Get staff by ID                   |
| `POST`   | `/api/staff`           | Add a new staff member            |
| `PATCH`  | `/api/staff/{id}`      | Update staff details              |
| `DELETE` | `/api/staff/{id}`      | Remove a staff member             |

**Planned DTOs:**
```csharp
// CreateStaffDto
{
  string StaffCode,
  string FirstName,
  string LastName,
  string Role,
  string Phone,
  string Email,
  DateTime JoiningDate
}
```

**Required Files to Create:**
- `DTOs/Staff/CreateStaffDto.cs`
- `DTOs/Staff/UpdateStaffDto.cs`
- `Services/Interfaces/IStaffService.cs`
- `Services/Implementations/StaffService.cs`
- `Controllers/StaffController.cs`

---

### 10.3 Section Management

**Status:** ❌ Not Implemented (Entity exists, no Controller/Service)

Sections are subdivisions of a Class (e.g., Class 10 → Section A, B, C).

**Planned Endpoints:**

| Method   | Endpoint                           | Description                          |
|----------|------------------------------------|--------------------------------------|
| `GET`    | `/api/sections`                    | Get all sections                     |
| `GET`    | `/api/sections/{id}`               | Get section by ID                    |
| `GET`    | `/api/sections/class/{classId}`    | Get all sections of a class          |
| `POST`   | `/api/sections`                    | Create a new section                 |
| `PATCH`  | `/api/sections/{id}`               | Update section (e.g., assign teacher)|
| `DELETE` | `/api/sections/{id}`               | Delete a section                     |

**Required Files to Create:**
- `DTOs/Sections/CreateSectionDto.cs`
- `DTOs/Sections/UpdateSectionDto.cs`
- `Services/Interfaces/ISectionService.cs`
- `Services/Implementations/SectionService.cs`
- `Controllers/SectionsController.cs`

---

### 10.4 Subject Management

**Status:** ❌ Not Implemented (Entity exists, no CRUD APIs)

**Planned Endpoints:**

| Method   | Endpoint               | Description            |
|----------|------------------------|------------------------|
| `GET`    | `/api/subjects`        | Get all subjects       |
| `GET`    | `/api/subjects/{id}`   | Get subject by ID      |
| `POST`   | `/api/subjects`        | Create a new subject   |
| `PATCH`  | `/api/subjects/{id}`   | Update a subject       |
| `DELETE` | `/api/subjects/{id}`   | Delete a subject       |

**Required Files to Create:**
- `DTOs/Subjects/CreateSubjectDto.cs`
- `DTOs/Subjects/UpdateSubjectDto.cs`
- `Services/Interfaces/ISubjectService.cs`
- `Services/Implementations/SubjectService.cs`
- `Controllers/SubjectsController.cs`

---

### 10.5 Student Enrollment

**Status:** ❌ Not Implemented (Entity exists, no CRUD APIs)

Student Enrollment links a Student to a Section and an AcademicYear. A student is enrolled each academic year.

**Planned Endpoints:**

| Method   | Endpoint                                      | Description                              |
|----------|-----------------------------------------------|------------------------------------------|
| `GET`    | `/api/enrollments`                            | Get all enrollments                      |
| `GET`    | `/api/enrollments/{id}`                       | Get enrollment by ID                     |
| `GET`    | `/api/enrollments/student/{studentId}`        | Get all enrollments for a student        |
| `GET`    | `/api/enrollments/year/{academicYearId}`      | Get all enrollments in an academic year  |
| `POST`   | `/api/enrollments`                            | Enroll a student for an academic year    |
| `DELETE` | `/api/enrollments/{id}`                       | Cancel/remove an enrollment              |

**Required Files to Create:**
- `DTOs/Enrollments/CreateStudentEnrollmentDto.cs`
- `Services/Interfaces/IStudentEnrollmentService.cs`
- `Services/Implementations/StudentEnrollmentService.cs`
- `Controllers/StudentEnrollmentsController.cs`

---

### 10.6 Staff Assignment

**Status:** ❌ Not Implemented (Entity exists, no CRUD APIs)

Staff assignment defines which teacher teaches which subject in which section, and who the class teacher is.

**Planned Endpoints:**

| Method   | Endpoint                                  | Description                                |
|----------|-------------------------------------------|--------------------------------------------|
| `GET`    | `/api/staff-assignments`                  | Get all assignments                        |
| `GET`    | `/api/staff-assignments/{id}`             | Get assignment by ID                       |
| `GET`    | `/api/staff-assignments/staff/{staffId}`  | Get all assignments for a staff member     |
| `GET`    | `/api/staff-assignments/section/{sectionId}` | Get all teachers of a section           |
| `POST`   | `/api/staff-assignments`                  | Assign staff to a section/subject          |
| `PATCH`  | `/api/staff-assignments/{id}`             | Update an assignment                       |
| `DELETE` | `/api/staff-assignments/{id}`             | Remove an assignment                       |

**Required Files to Create:**
- `DTOs/StaffAssignments/CreateStaffAssignmentDto.cs`
- `DTOs/StaffAssignments/UpdateStaffAssignmentDto.cs`
- `Services/Interfaces/IStaffAssignmentService.cs`
- `Services/Implementations/StaffAssignmentService.cs`
- `Controllers/StaffAssignmentsController.cs`

---

### 10.7 Student Attendance

**Status:** ❌ Not Implemented (Entity exists, no CRUD APIs)

Tracks daily student attendance per section. Attendance can be `Present`, `Absent`, or `Leave`.

**Planned Endpoints:**

| Method   | Endpoint                                           | Description                              |
|----------|----------------------------------------------------|------------------------------------------|
| `GET`    | `/api/student-attendance`                          | Get all attendance records               |
| `GET`    | `/api/student-attendance/section/{sectionId}/date/{date}` | Get attendance for a section on a date |
| `GET`    | `/api/student-attendance/student/{studentId}`      | Get attendance history for a student     |
| `POST`   | `/api/student-attendance`                          | Mark attendance for a student            |
| `POST`   | `/api/student-attendance/bulk`                     | Mark attendance for all students in bulk |
| `PATCH`  | `/api/student-attendance/{id}`                     | Correct/update an attendance record      |

**Required Files to Create:**
- `DTOs/Attendance/CreateStudentAttendanceDto.cs`
- `DTOs/Attendance/BulkStudentAttendanceDto.cs`
- `Services/Interfaces/IStudentAttendanceService.cs`
- `Services/Implementations/StudentAttendanceService.cs`
- `Controllers/StudentAttendanceController.cs`

---

### 10.8 Staff Attendance

**Status:** ❌ Not Implemented (Entity exists, no CRUD APIs)

Tracks daily staff attendance with optional check-in/check-out times.

**Planned Endpoints:**

| Method   | Endpoint                                      | Description                                  |
|----------|-----------------------------------------------|----------------------------------------------|
| `GET`    | `/api/staff-attendance`                       | Get all staff attendance records             |
| `GET`    | `/api/staff-attendance/staff/{staffId}`       | Get attendance history of a staff member     |
| `GET`    | `/api/staff-attendance/date/{date}`           | Get all staff attendance on a date           |
| `POST`   | `/api/staff-attendance`                       | Mark staff attendance                        |
| `PATCH`  | `/api/staff-attendance/{id}`                  | Update attendance (e.g., add check-out time) |

**Required Files to Create:**
- `DTOs/Attendance/CreateStaffAttendanceDto.cs`
- `DTOs/Attendance/UpdateStaffAttendanceDto.cs`
- `Services/Interfaces/IStaffAttendanceService.cs`
- `Services/Implementations/StaffAttendanceService.cs`
- `Controllers/StaffAttendanceController.cs`

---

### 10.9 Authentication & Authorization

**Status:** ❌ Not Implemented

**Description:** Secure the API with JWT-based authentication and role-based authorization.

**Planned Roles:**
- `Admin` — Full access to all resources
- `Teacher` — Can mark attendance, view assigned sections/students
- `Staff` — Limited access (view own profile, apply leave)
- `Parent/Guardian` — View their child's attendance, results (optional mobile-facing API)

**Planned Endpoints:**

| Method | Endpoint              | Description                           |
|--------|-----------------------|---------------------------------------|
| `POST` | `/api/auth/login`     | Login with credentials, get JWT token |
| `POST` | `/api/auth/refresh`   | Refresh expired token                 |
| `POST` | `/api/auth/logout`    | Invalidate/revoke token               |
| `GET`  | `/api/auth/me`        | Get current logged-in user profile    |

**Required NuGet Packages to Add:**
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `System.IdentityModel.Tokens.Jwt`

**Required Files to Create:**
- `Models/Entities/User.cs` (with role, password hash)
- `DTOs/Auth/LoginDto.cs`
- `DTOs/Auth/TokenResponseDto.cs`
- `Services/Interfaces/IAuthService.cs`
- `Services/Implementations/AuthService.cs`
- `Controllers/AuthController.cs`

---

### 10.10 Reporting Module

**Status:** ❌ Not Planned / Not Yet Designed

Reporting endpoints to generate summary data.

**Planned Endpoints:**

| Method | Endpoint                                                | Description                           |
|--------|----------------------------------------------------------|---------------------------------------|
| `GET`  | `/api/reports/attendance/student/{studentId}`           | Full attendance report for a student  |
| `GET`  | `/api/reports/attendance/section/{sectionId}/summary`   | Section-level attendance summary      |
| `GET`  | `/api/reports/attendance/staff/{staffId}`               | Staff attendance report               |
| `GET`  | `/api/reports/students/section/{sectionId}`             | Student list report for a section     |

---

### 10.11 Fee Management

**Status:** ❌ Not Yet Designed (No entity defined)

A future module to manage student fees, payment records, and dues.

**Planned Entities (to be designed):**
- `FeeStructure` — Define fee types per program/class
- `FeePayment` — Track payments by student per academic year

**Planned Endpoints:**

| Method   | Endpoint                          | Description                          |
|----------|-----------------------------------|--------------------------------------|
| `GET`    | `/api/fee-structure`              | Get fee structure list               |
| `POST`   | `/api/fee-structure`              | Create a fee structure               |
| `POST`   | `/api/fee-payments`               | Record a fee payment for a student   |
| `GET`    | `/api/fee-payments/student/{id}`  | Get payment history of a student     |
| `GET`    | `/api/fee-payments/pending`       | Get list of students with due fees   |

---

### 10.12 Exam & Results

**Status:** ❌ Not Yet Designed (No entity defined)

A module to manage exams, question papers, marks/results.

**Planned Entities (to be designed):**
- `Exam` — Exam metadata (name, date, type)
- `ExamResult` — Marks per student per subject

**Planned Endpoints:**

| Method   | Endpoint                              | Description                      |
|----------|---------------------------------------|----------------------------------|
| `GET`    | `/api/exams`                          | Get all exams                    |
| `POST`   | `/api/exams`                          | Create an exam                   |
| `POST`   | `/api/exam-results`                   | Add results for an exam          |
| `GET`    | `/api/exam-results/student/{id}`      | Get all results for a student    |
| `GET`    | `/api/exam-results/exam/{examId}`     | Get all results for an exam      |

---

### 10.13 Notifications

**Status:** ❌ Not Yet Designed

A module to manage announcements, school notices, and event alerts.

**Planned Entities (to be designed):**
- `Notification` — Title, body, target audience, created date

**Planned Endpoints:**

| Method   | Endpoint                    | Description                         |
|----------|-----------------------------|-------------------------------------|
| `GET`    | `/api/notifications`        | Get all notifications               |
| `POST`   | `/api/notifications`        | Create/send a notification          |
| `DELETE` | `/api/notifications/{id}`   | Delete a notification               |

---

## 11. Coding Conventions

| Convention              | Detail                                                                 |
|-------------------------|------------------------------------------------------------------------|
| **Naming**              | PascalCase for classes, methods. camelCase for local variables.        |
| **DTOs**                | Separate `Create` and `Update` DTOs per entity stored in `DTOs/`       |
| **Service Interfaces**  | All services defined in `Services/Interfaces/` using `I` prefix        |
| **Async**               | All service and controller methods are `async Task<>`                  |
| **HTTP Methods**        | `GET` = read, `POST` = create, `PATCH` = partial update, `DELETE` = remove |
| **Error Responses**     | Return `NotFound()` when entity by ID doesn't exist                   |
| **Timestamps**          | `CreatedAt` set at entity creation. `UpdatedAt` set in service `UpdateAsync`. |
| **EF Configurations**   | Entity constraints (indexes, required fields, max lengths) defined in `Data/Configurations/` using Fluent API |
| **Namespace**           | `SchoolApi.` prefix for all namespaces, matching folder structure      |

---

## 12. EF Core Migrations

Migrations are stored in the `/Migrations` folder.

### Common Commands

```bash
# Add a new migration
dotnet ef migrations add <MigrationName>

# Apply pending migrations to the database
dotnet ef database update

# Rollback to a previous migration
dotnet ef database update <PreviousMigrationName>

# Remove the last (unapplied) migration
dotnet ef migrations remove

# List all migrations
dotnet ef migrations list

# Generate SQL script for migrations
dotnet ef migrations script
```

> **Note:** Always run migrations from the project root directory where `SchoolApi.csproj` is located.

---

*Documentation generated: March 2026*
*Author: SchoolApi Development Team*

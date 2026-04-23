# TaskFlow MVC

TaskFlow MVC is a web-based task and board management system inspired by Kanban workflows and Trello-style organization. The application is built with ASP.NET Core MVC, Entity Framework Core, SQLite, and ASP.NET Identity. It allows authenticated users to access the workspace, while administrative users can manage boards and tasks, track progress, and monitor deadlines through a simple dashboard.

## Overview

This project was developed as a school assignment for building a layered MVC application with authentication, authorization, persistent storage, LINQ queries, and validation. The system focuses on organizing work through boards and task items with priority, status, and deadline tracking.

## Main Features

- User registration and login with ASP.NET Identity
- Role-based authorization with `Admin` and `User` roles
- Automatic role seeding on application startup
- Board management with create, read, update, and delete operations
- Task management with create, read, update, and delete operations
- Task fields for title, description, priority, status, deadline, and board assignment
- Task grouping by status with priority-based filtering
- Dashboard with task statistics
- Upcoming deadline tracking
- Overdue task monitoring
- Entity relationships between users, boards, and tasks
- Server-side validation through Data Annotations
- Razor Views with Bootstrap-based UI

## Current Functional Scope

### Authentication and Security

- Guests can access the public pages and account screens
- Authenticated users can access boards, tasks, and dashboard pages
- `Admin`-only authorization protects create, edit, and delete actions for boards and tasks
- New registered accounts are assigned the `User` role by default

### Boards

- View all boards
- Open board details
- Create a new board
- Edit board title
- Delete a board
- Each board is linked to a specific user

### Tasks

- View all tasks
- Open task details
- Create a task and assign it to a board
- Edit task information
- Delete tasks
- Filter tasks by priority and view them grouped by status

### Dashboard and Reporting

- Total completed tasks
- Total pending tasks
- Overdue task counter
- Completion percentage
- List of upcoming deadlines for the next 7 days
- Quick navigation to main management pages

## Technology Stack

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQLite
- ASP.NET Core Identity
- Razor Views
- Bootstrap

## Project Structure

```text
.
├── Controllers/     MVC controllers for boards, tasks, accounts, dashboard, and home
├── Data/            Database context and EF Core configuration
├── Migrations/      Entity Framework Core migrations
├── Models/          Domain models, enums, and base entities
├── Services/        Business logic and service interfaces
├── Views/           Razor UI pages
├── wwwroot/         Static assets such as CSS, JS, and libraries
├── Program.cs       Application startup and service registration
└── appsettings.json Application configuration
```

## Data Model

The application uses a simple relational structure:

- One `ApplicationUser` can own many `Board` entities
- One `Board` can contain many `TaskItem` entities
- `TaskItem` inherits common fields such as `Id` and `CreatedAt` from `BaseEntity`

This design demonstrates:

- One-to-many database relationships
- Code-first modeling with EF Core
- Encapsulation and inheritance
- Service abstraction through interfaces such as `ITaskService` and `IBoardService`

## LINQ and Business Logic

The project includes LINQ-based queries for:

- Filtering tasks by priority
- Filtering tasks by status
- Filtering tasks by board
- Grouping tasks by status for reporting and visualization
- Extracting upcoming and overdue deadlines for the dashboard

## Validation

Validation is handled through Data Annotations in the models:

- Required fields for board titles and task titles
- Maximum length limits for titles and descriptions
- Typed deadline input for task scheduling

## Requirements

- .NET 9 SDK or later

Check your installation with:

```bash
dotnet --version
```

## Getting Started

1. Restore the dependencies:

```bash
dotnet restore
```

2. Build the project:

```bash
dotnet build
```

3. Run the application:

```bash
dotnet run
```

4. Open the local URL shown in the terminal.

On startup, the app will:

- apply the existing EF Core migrations
- create the SQLite database if needed
- seed the `Admin` and `User` roles

## Default Database

The project uses SQLite with the local file:

```text
app.db
```

The connection string is configured in `appsettings.json`.

## Notes About Roles

The application seeds the `Admin` and `User` roles automatically, but it does not create a default administrator account. After registration, users receive the `User` role. If you want full admin access for protected actions, an account must be promoted to the `Admin` role manually.

## Example User Flow

1. Register a new account
2. Sign in
3. Promote the account to `Admin` if full management access is required
4. Create a board
5. Add tasks to the board
6. Set priority, status, and deadline
7. Track progress in the dashboard
8. Review grouped tasks by status and priority

## Planned Improvements

The following features would make the system closer to a full Trello-style platform:

- List-based workflow inside each board
- Drag-and-drop task movement between statuses
- Better per-user access control so standard users only manage their own data
- Weekly productivity statistics by user
- Deadline alerts and notifications
- Search and sorting for boards and tasks
- Comments or collaboration on tasks
- DTO/ViewModel layer for cleaner separation between domain and UI
- Automated tests for services and controllers
- Seeded demo data for easier first-time review

## Educational Goals Covered

This project demonstrates:

- MVC architecture
- Dependency Injection
- EF Core code-first persistence
- Entity relationships
- OOP concepts such as inheritance and abstraction
- Identity-based authentication and authorization
- LINQ querying
- Validation and form handling

## Troubleshooting

- If the application does not start, verify the installed .NET SDK version
- If the database schema is out of sync, delete the local database and run the app again to recreate it from migrations
- If admin-only actions are blocked, check whether the logged-in account has the `Admin` role
- If static styling does not load, ensure the `wwwroot` assets are present

## Future README Additions

This README can be extended further with:

- screenshots of the dashboard and task pages
- ER diagram or architecture diagram
- deployment instructions
- test instructions
- sample accounts or seed strategy

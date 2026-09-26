# 📓 Notesphere

A full-featured student productivity web application that helps students manage notes, plan schedules, track tasks, and collaborate with peers — all in one place.

> Built as a collaborative academic project at Sheridan College, Fall 2025.

---

## Key Features

- **Notes** — Create and organize notes with tags, version history, and templates
- **Planner** — Weekly event planner with recurring events and real-time conflict detection
- **Dashboard** — Personal workspace with reminders and quick actions
- **Sharing** — Collaborate and share notes with other users
- **Productivity** — Task manager with priorities, statuses, due dates, and checklists that automatically update each task's progress
- **Authentication** — Secure login and registration

---

## Tech Stack

- ASP.NET Core MVC
- C#
- SQLite
- Entity Framework Core
- HTML / CSS / JavaScript

---

## Getting Started

### Prerequisites
- Visual Studio 2022 or later
- .NET 8 SDK

### Running the Project

1. Clone the repository
```bash
   git clone https://github.com/Ali-Talha/Notesphere-Application.git
```
2. Open `Notesphere Application.sln` in Visual Studio and set **Notesphere.Operations** as the startup project
3. Apply database migrations (optional, as the repository includes an up-to-date SQLite database)
```bash
   dotnet ef database update --project Notesphere.Services --startup-project Notesphere.Operations
```
4. Press **F5** to run

---

## Project Structure

```
Notesphere/
├── Notesphere.Entities/          # Entity models (Notes, Planner, Dashboard, Sharing, Productivity)
├── Notesphere.Services/          # Business logic & repositories
│   ├── NotesphereDataAccessLayer/  # NotesphereDbContext (database context)
│   └── Migrations/               # EF Core migrations
└── Notesphere.Operations/        # ASP.NET Core MVC web application
    ├── Controllers/              # MVC Controllers
    ├── Models/                   # View models
    ├── Views/                    # Razor views
    └── Data/                     # SQLite database
```

---

## Team

Collaborative academic project — Sheridan College, Fall 2025.

| Name | Role |
|------|------|
| Mamin Khan | Notes & Dashboard Module |
| Malika Muskan | Planner Module |
| Talha Ali | Productivity Module |
| Saad Kifayat | Sharing & Collaboration Module |


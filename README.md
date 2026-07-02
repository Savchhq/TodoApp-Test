# 📋 Todo Application

Full-stack To-Do application with ASP.NET Core backend and Angular frontend, featuring JWT authentication, task management, and category organization.

## ✨ Features

- **Authentication** — User registration and login with JWT tokens
- **Task Management** — Create, read, update, delete tasks with full CRUD operations
- **Categories** — Organize tasks by categories
- **Search & Filter** — Search tasks by title and filter by category
- **Pagination** — Navigate large task lists efficiently
- **Clean Architecture** — 4-layer backend design for maintainability

## 🏗️ Technology Stack

| Layer | Technologies |
|-------|--------------|
| **Frontend** | Angular 19+, TypeScript, HTML, CSS |
| **Backend API** | ASP.NET Core (.NET 10), C# |
| **Business Logic** | AutoMapper, DTOs, Service pattern |
| **Data Access** | Entity Framework Core, PostgreSQL |
| **Authentication** | JWT (JSON Web Tokens) |

## 📁 Project Structure

```
TodoApp/
├── TodoApp.Api/           # REST API controllers and entry point
├── TodoApp.BLL/           # Business logic, services, DTOs, and mapping profiles
├── TodoApp.DAL/           # Entity Framework Core, database context, repositories
├── TodoApp.Core/          # Domain models, interfaces, and contracts
├── todo-frontend/         # Angular single-page application
├── Dockerfile             # Docker configuration for containerization
└── TodoApp.slnx          # Solution file
```

### Backend Layers

- **TodoApp.Api** — REST endpoints and HTTP request handling
- **TodoApp.BLL** — Business logic services and data transfer objects (DTOs)
- **TodoApp.DAL** — Database access layer with repositories and EF Core migrations
- **TodoApp.Core** — Domain entities and service interfaces

## 🚀 Getting Started

### Prerequisites

- .NET 10 SDK
- Node.js 18+ and npm
- PostgreSQL database (or use Docker)

### Backend Setup

1. Navigate to the API project:
   ```bash
   cd TodoApp.Api
   ```

2. Configure the database connection in `appsettings.Development.json` (if not using default):
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=TodoApp;Username=postgres;Password=your_password"
   }
   ```

3. Apply database migrations:
   ```bash
   dotnet ef database update --project TodoApp.DAL
   ```

4. Run the API:
   ```bash
   dotnet run --project TodoApp.Api
   ```
   
   API will be available at **http://localhost:5188**

### Frontend Setup

1. Navigate to the frontend folder:
   ```bash
   cd todo-frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm start
   ```

   Frontend will be available at **http://localhost:4200**

### Docker Setup

Build and run the application in a container:

```bash
docker build -t todoapp .
docker run -p 5188:5188 -p 4200:4200 todoapp
```

## 📊 API Endpoints

- `POST /api/auth/register` — Register a new user
- `POST /api/auth/login` — Login with credentials
- `GET /api/tasks` — Get all tasks
- `POST /api/tasks` — Create a new task
- `PUT /api/tasks/{id}` — Update a task
- `DELETE /api/tasks/{id}` — Delete a task
- `GET /api/categories` — Get all categories
- `POST /api/categories` — Create a new category

## 🔧 Development

### Running Tests

```bash
# Backend
dotnet test

# Frontend
npm test
```

### Database Migrations

Create a new migration:
```bash
dotnet ef migrations add MigrationName --project TodoApp.DAL
```

Apply migrations:
```bash
dotnet ef database update --project TodoApp.DAL
```

## 📝 License

This project is provided as-is for educational and development purposes.

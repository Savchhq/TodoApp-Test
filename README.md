# Todo Application

Full-stack To-Do application built with ASP.NET Core and Angular.

## 🚀 Features Implemented
- User authentication (register/login) using JWT
- CRUD operations for tasks and categories
- Pagination for task lists
- Search by task title and filtering by category
- 4-layer backend architecture: Controllers, Services, Interfaces, Data Access

## 🛠 Technologies Used
- **Backend:** ASP.NET Core, Entity Framework Core, SQL Server
- **Frontend:** Angular, HTML, CSS

## ⚙️ How to Run Locally

### Backend Setup
1. Navigate to the solution root.
2. Update the database connection string in [TodoApp.Api/appsettings.Development.json](TodoApp.Api/appsettings.Development.json) if needed.
3. Apply database migrations:
   ```bash
   dotnet ef database update --project TodoApp.Api
   ```
4. Run the API:
   ```bash
   dotnet run --project TodoApp.Api
   ```
   The API will run at http://localhost:5188.

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
4. Open your browser at http://localhost:4200.

## 📁 Project Structure
- [TodoApp.Api](TodoApp.Api) — REST API layer
- [TodoApp.BLL](TodoApp.BLL) — business logic and DTOs
- [TodoApp.DAL](TodoApp.DAL) — EF Core data access and repositories
- [TodoApp.Core](TodoApp.Core) — domain models and interfaces
- [todo-frontend](todo-frontend) — Angular client application

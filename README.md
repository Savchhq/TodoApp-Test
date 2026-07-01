# Todo Application (Test Assignment for SoftPlus)

Full-stack To-Do application built with ASP.NET Core and Angular.

## 🚀 Features Implemented
* User Authentication (Login / Registration) via JWT
* CRUD operations for Tasks and Categories
* Pagination for the list of tasks
* Searching by task name and filtering by categories
* 4-layer backend architecture (Controllers, Services, Interfaces, Data Access)

## 🛠 Technologies Used
* **Backend:** ASP.NET Core 8 (REST API), Entity Framework Core, SQL Server (або напиши SQLite/PostgreSQL, якщо використовував їх)
* **Frontend:** Angular 17, HTML/CSS (Bootstrap/Tailwind - вкажи своє)

## ⚙️ How to Run Locally

### Backend Setup
1. Navigate to the backend folder: `cd TodoApp.Backend`
2. Update the database connection string in `appsettings.json` if necessary.
3. Apply database migrations: `dotnet ef database update`
4. Run the API: `dotnet run` (runs on http://localhost:5188)

### Frontend Setup
1. Navigate to the frontend folder: `cd todoapp-frontend`
2. Install dependencies: `npm install`
3. Start the development server: `ng serve`
4. Open your browser and go to `http://localhost:4200`

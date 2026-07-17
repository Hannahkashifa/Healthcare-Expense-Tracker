# Personal Healthcare Expense Tracker

A full-stack web application to track your income, expenses, healthcare visits, and medicines.

## Tech Stack
- **Backend:** ASP.NET Core 6.0 Web API
- **Frontend:** ASP.NET Core 6.0 MVC
- **Database:** SQL Server
- **Auth:** JWT (JSON Web Token)

## Features
- User Registration & Login
- Dashboard with income/expense/healthcare summary
- Manage Expenses (CRUD)
- Manage Income (CRUD)
- Healthcare Visits tracking
- Medicine tracking linked to visits
- User-specific data (each user sees only their own data)

## How to Run

### Prerequisites
- .NET 6.0 SDK
- SQL Server

### Step 1: Setup Database
1. Open SQL Server Management Studio
2. Create a database named `PersonalHealthcareExpenseDB`
3. Update the connection string in `backend/appsettings.json`

### Step 2: Run Backend
```
cd backend
dotnet restore
dotnet run
```
Backend runs at `https://localhost:7230`

### Step 3: Update Frontend Config
Open `frontend/appsettings.json` and set the API URL:
```json
"ApiBaseUrl": "https://localhost:7230"
```

### Step 4: Run Frontend
```
cd frontend
dotnet restore
dotnet run
```
Frontend runs at `https://localhost:7080`

### Step 5: Use the App
1. Open the frontend URL
2. Register a new account
3. Login and start tracking!

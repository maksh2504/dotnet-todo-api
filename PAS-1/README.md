# PAS-1 — ASP.NET Core Web API

## 📋 Description

This is the backend of a simple ToDo application built with ASP.NET Core Web API. It supports:

- User registration and login
- JWT authentication with access and refresh tokens
- Route protection
- CRUD operations for tasks (ToDos)
- PostgreSQL database
- Swagger (OpenAPI)
- CORS configuration

---

## ⚙️ Tech Stack

- ASP.NET Core 8
- Entity Framework Core (PostgreSQL)
- PostgreSQL
- JWT Authentication
- Swagger / OpenAPI
- CORS

---

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/your-username/PAS-1.git
cd PAS-1
```

### 2. Update DB connection (PAS-1/appsettings.json)
```
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=pas;Username=postgres;Password=root"
  },
```

### 3. Run database migrations
```
dotnet tool install --global dotnet-ef
```
Then apply migrations:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Run the project
```
dotnet run
```

### Available at:

HTTPS: https://localhost:7175

HTTP: http://localhost:5274

### Swagger UI

Available at: https://localhost:7175/swagger

### Notes

For protected routes, pass the access token in the Authorization header like:

```
Authorization: Bearer <your_token>
```
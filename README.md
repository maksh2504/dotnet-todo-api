# TodoApp.Server — ASP.NET Core Web API

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
git clone https://github.com/maksh2504/dotnet-todo-api.git
cd dotnet-todo-api
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



## Requests examples

### Auth:

#### Register
```
POST /api/auth/register
{
  "username": "string",
  "password": "string"
}
```

#### Login
```
POST /api/auth/login
{
  "username": "string",
  "password": "string"
}
```

#### Refresh token
```
POST /api/auth/refresh
{
  "refreshToken": "string"
}
```

### Users:

#### Get current user
```
GET /api/users/current
```

### ToDos:

#### Get ToDos list
```
GET /api/api/todos
Query: {
    "sort": "asc" | "desc",
    "finished": "empty" | "true" | "false"
}
```

#### Create ToDo
```
POST /api/api/todos
{
  "title": "string",
  "description": "string",
  "finished": true
}
```

#### Get ToDo
```
GET /api/api/todos/{id}
```

#### Update ToDo
```
PATCH /api/api/todos
{
  "title": "string",
  "description": "string",
  "finished": true
}
```

#### Delete ToDo
```
Delete /api/api/todos/{id}
```

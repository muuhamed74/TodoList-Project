# TodoApi (.NET 8)

##  User Features

Once registered and authenticated, a user can:

- **Log in** using email and password.
- **Create** todo items (with title, description, status, etc).
- **View** their own todo items.
- **Update** or **delete** any of their existing todo items.
- All requests must include a valid **JWT token** in the `Authorization` header to access secured endpoints.

## Project Structure

The project is organized into multiple layers:

- **TodoApi**: The entry point of the API (Controllers, Startup config).
- **Todo.Domain**: Contains domain entities, interfaces, and core business logic contracts.
- **Todo.Repo**: Implementation of repositories using Entity Framework Core (DbContext, Migrations).
- **Todo.Serv**: Services that contain the business logic, working between Controllers and Repositories.
- **TodoApi.Test**: (Unit Testing layer using Moq & xUnit – not implemented in this project).

---

## Core Features

-  **JWT Authentication & ASP.NET Identity**
-  **CRUD operations for Todo Items**
-  **EF Core with Code-First Migrations**
-  **AutoMapper for mapping DTOs and Entities**
-  **Separation of Concerns with DI and interfaces**

---

##  Request Flow

1. Client sends HTTP request.
2. API Controller calls Service Layer.
3. Service Layer handles logic, maps DTOs, and calls Repository.
4. Repository interacts with DB using EF Core.
5. Response flows back to the client.

---

##  Summary

This project demonstrates:

-  Secure authentication
-  Clean architecture
-  Scalable CRUD structure
-  Modern .NET development practices




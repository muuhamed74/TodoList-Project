# TodoApi (.NET 8)

## Architecture & Structure

This project follows a **clean, layered architecture**:

- **TodoApi (API Layer):** Entry point, receives HTTP requests.
- **Todo.Serv (Service Layer):** Business logic.
- **Todo.Repo (Repository Layer):** Database access using EF Core.
- **Todo.Domain (Domain Layer):** Entities, DTOs, and Interfaces.

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




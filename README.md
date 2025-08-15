# Repository Pattern & Unit of Work Example

![.NET](https://img.shields.io/badge/.NET-6-blue)
![C#](https://img.shields.io/badge/C%23-Modern-green)
![License](https://img.shields.io/badge/License-MIT-lightgrey)

This project demonstrates the implementation of the **Repository Pattern** and **Unit of Work (UoW) Pattern** in a .NET application, providing a clean architecture and maintainable code.

---

## 🚀 Overview

- **Repository Pattern:** Abstracts data access, providing reusable CRUD operations.  
- **Unit of Work:** Groups multiple repository operations in a single transaction for consistency.  
- Promotes clean separation of concerns and testable code.

---

## ✨ Features

- Generic and optional specific repositories.  
- Unit of Work for transactional consistency.  
- Easily maintainable and extendable codebase.  

---

## 📂 Project Structure

RepositoryPatternUoW/
├── Data/ # DbContext & Unit of Work
├── Repositories/ # Generic & specific repositories
├── Models/ # Entities
├── Services/ # Business logic
└── Program.cs # Entry point & DI configuration

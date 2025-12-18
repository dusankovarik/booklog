# BookLog

**BookLog** is a web application designed for managing books and their reviews.  
It can be used, for example, as a personal reading log or a simple book review system.

The application allows users to manage books, authors, and literary genres, and to write reviews for individual books.

---

## Features

- Browse, create, edit, and delete books
- Manage authors and literary genres
- View detailed information about each book
- Add reviews with a rating (1–5) and text
- Display reviews directly on the book details page
- User authentication and role-based authorization

---

## User Roles

The application supports the following roles:

### User

- View and edit books, authors, and literary genres
- Add book reviews

**Username:** `reader`

### Administrator

- All permissions of a regular user
- Manage application users
- Manage user roles

**Username:** `admin`

---

## Password Format

For demonstration purposes, the password format is predefined:

- first four letters of the alphabet (with the first letter capitalized),
- followed by an underscore,
- followed by the first four positive integers.

---

## Technologies Used

- **ASP.NET Core MVC**
- **Entity Framework Core**
- **ASP.NET Core Identity**
- **MS SQL Server**
- **Bootstrap** (basic styling)

---

## Project Structure

The application follows a layered architecture:

- **Controllers** – handle HTTP requests and responses
- **Services** – contain business logic and database access
- **Models** – represent database entities
- **DTOs / ViewModels** – transfer data between layers and views
- **Views** – Razor views for the user interface

---

## Authentication & Authorization

- Authentication is handled using **ASP.NET Core Identity**
- Authorization is role-based
- Only authenticated users can add reviews
- Administrative functionality is restricted to users with the `Admin` role

---

## Purpose of the Project

This application was created as an individual project within the  
**Object-Oriented Programming** course at  
**VŠB – Technical University of Ostrava**.

The main goal of the project was to demonstrate:

- object-oriented design,
- work with relational data using Entity Framework,
- MVC architecture,
- authentication and authorization using ASP.NET Identity.

---

## Notes

- User registration is not publicly available.
- Users and roles are managed by an administrator.
- The application is intended for educational purposes.

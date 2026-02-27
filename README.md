# KEshop – ASP.NET Core E-Commerce Platform

![.NET](https://img.shields.io/badge/.NET-9.0-blue)
![C#](https://img.shields.io/badge/C%23-Backend-green)
![Architecture](https://img.shields.io/badge/Architecture-3Tier-orange)
![JWT](https://img.shields.io/badge/Auth-JWT-red)
![Stripe](https://img.shields.io/badge/Payments-Stripe-purple)

---

## 📌 Overview

 a full-featured, multi-layered E-Commerce web application built using **ASP.NET Core (.NET 9)** and **Entity Framework Core**.

The system follows modern backend architecture principles including:

- 3-Tier Architecture (PL, BLL, DAL)
- Repository Pattern
- Service Layer Pattern
- DTO Pattern
- ASP.NET Core Identity
- Stripe Payment Integration
- JWT Authentication
- Clean Architecture Principles

This project demonstrates real-world backend development practices and scalable system design.

---

## Architecture

The application follows a structured **3-Layer Architecture**:

```

KEshop
│
├── .PL → Presentation Layer (Web / MVC)
├── .BLL → Business Logic Layer (Services)
├── .DAL → Data Access Layer (EF Core + Repositories)
└── .sln

```

---

##  Architecture Diagram

```mermaid
flowchart TD

User["User"]
HTTP["HTTP Request"]

PL["Presentation (PL)"]
BLL["Business Logic (BLL)"]
DAL["Data Access (DAL)"]
EF["Entity Framework"]
DB[(SQL Server)]

User --> HTTP
HTTP --> PL
PL -->|"Calls Services"| BLL
BLL -->|"Uses Repositories"| DAL
DAL --> EF
EF --> DB

style PL fill:#E3F2FD,stroke:#1E88E5,stroke-width:2px
style BLL fill:#FFF3E0,stroke:#FB8C00,stroke-width:2px
style DAL fill:#E8F5E9,stroke:#43A047,stroke-width:2px
style DB fill:#F3E5F5,stroke:#8E24AA,stroke-width:2px

```
---

## Project Structure

### Presentation Layer – `Ecom.PL`
#### Handles:
- API Controllers
- Identity Configuration
- JWT Authentication
- Stripe configuration
- CORS Policies
- Email settings
- Static files (`wwwroot/images`)

### Business Logic Layer – Ecom.BLL
Implements application business rules using service classes and interfaces.

####  Example Services
- AuthenticationService
- ProductService
- CategoryService
- BrandService
- CartService
- CheckOutService
- OrderService
- ReviewService
- ReportService
- UserService
- FileService

#### This layer ensures:
- Validation
- Business rules enforcement
- Separation from database logic

### Data Access Layer – Ecom.DAL
#### Implements:
- Entity Framework Core
- ApplicationDbContext
- Repository Pattern
- DTOs (Requests / Responses)
- Seed Data
- Migrations

#### Core Models
- ApplicationUser
- Product
- Category
- Brand
- Cart
- Order
- OrderItem
- ProductImage
- Review
---

## Key Features

### Authentication & Authorization
- ASP.NET Core Identity
- JWT Bearer Authentication
- Role-Based Authorization
- Email Confirmation Required
- Account Lockout Protection

### Product & Catalog Management
- Create / Update / Delete Products
- Category & Brand Management
- Product Image Upload

### Shopping Cart
- Add to Cart
- Update Quantity
- Cart Summary

### Checkout & Payment
- Stripe Integration
- Secure Payment Session
- Order Creation After Payment

### Order Management
- Order Tracking
- Order Items
- Admin Order Control
- Order Status Handling

### Reviews System
- Add Reviews
- Product Rating System

### PDF Reporting
- Generate dynamic PDF reports using QuestPDF

### File Upload System
- Single & Multiple Image Upload
- Stores files inside `wwwroot/images`
- Uses GUID-based naming to prevent duplication
- Asynchronous file handling

---

##  JWT Authentication

This API uses **JWT Bearer Authentication** for securing endpoints.

After a successful login, the API returns a JWT token.

To access protected endpoints, include the token in the request header:

```
Authorization: Bearer {your_token}

```
Token validation includes:

- Lifetime validation
- Signing key validation
- Role-based authorization

---

## System Design

### Architectural Style
The system uses Layered Architecture (3-Tier) to ensure:
- Separation of Concerns
- Maintainability
- Testability
- Scalability

---
### Design Patterns Used

- **Repository Pattern**  
  Abstracts database operations from business logic.

- **Service Layer Pattern**  
  Encapsulates business logic inside services.

- **DTO Pattern**  
  Separates domain models from request/response contracts.

- **Dependency Injection**  
  Uses built-in ASP.NET Core DI container.

- **Identity Pattern**  
  Implements authentication and role management.

---

### Database Relationships
- One Category → Many Products
- One Product → Many Reviews
- One Order → Many OrderItems
- One User → Many Orders
- One Product → Many ProductImages

---

### Payment Flow (Stripe)
1. Customer adds items to cart
2. Proceeds to checkout
3. Stripe session is created
4. Payment processed
5. Order stored in database
6. Cart cleared

---

## Technologies Used

### Backend
- ASP.NET Core (.NET 9)
- C#
- Entity Framework Core
- SQL Server

### Security
- ASP.NET Core Identity
- JWT Bearer Authentication

### Payments & Reports
- Stripe.net
- QuestPDF

### Architecture & Patterns
- Repository Pattern
- Service Layer Pattern
- DTO Pattern
- Dependency Injection

---
## Prerequisites

Make sure you have the following installed:

- .NET 9 SDK
- SQL Server
- Visual Studio 2022 (17.8+)
- Entity Framework CLI Tools (`dotnet tool install --global dotnet-ef`)

---



## 🌐 API Documentation & Preview

This project is a RESTful API with interactive documentation.

When running in development mode, you can access:

- OpenAPI Specification:  
  https://localhost:5001/openapi/v1.json

- Scalar API Documentation UI:  
  https://localhost:5001/scalar

You can test all endpoints directly from the Scalar UI.
## Screenshots

### API Documentation (Scalar)

Full API documentation available using Scalar (OpenAPI).

![Scalar Overview](images/ScalarOverview.png)


---

### User Registration

New user can register using email, username, full name, phone number and password.

![Register](images/New-Register.png)

### Email Confirmation

After registration, user must confirm email before logging in.

![Email Confirmation](images/successful-confirm.png)

---

## User Login

Registered users can login and receive JWT token.

![Login](images/login.png)

## Admin Login

Admin can login and access protected admin endpoints.

![Admin Login](images/login-asAdmin.png)

---

## Get Products (Admin)

Admin can retrieve products list with pagination.

![Get Products](images/Get-Product.png)

---

## Add Product to Cart

Authenticated user can add product to cart.

![Create Cart](images/Create-Cart.png)

---

## Create Checkout Session (Stripe)

User can create Stripe checkout session.

![Checkout API](images/CheckOuts.png)

## Stripe Hosted Payment Page

User is redirected to Stripe secure payment page.

![Stripe URL](images/stripe-url.png)

---

## Generate Products PDF Report

Admin can generate PDF report of products.

![PDF Report](images/pdf-product.png)

---

## Security Considerations
- JWT Authentication
- Role-based authorization
- Password Policy Enforcement
- DTO validation
- Secure configuration management
- Stripe secure API usage


## Develped by:

**Omar Alhamdan**

Computer Systems Engineering Student

ASP.NET Core Developer

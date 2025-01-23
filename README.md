# UserPanelAPIASP
# User Panel Microservice API

This repository contains the source code for a user panel microservice API built with ASP.NET Core 9.0. It provides functionalities for user authentication, ticket management (for both users and admins), and real-time chat (follow-up chats).

## Project Structure

The solution is divided into several projects:

*   **UserPanel.API:** The main API project responsible for handling HTTP requests, routing, and business logic related to tickets and follow-up chats.
*   **UserPanel.Auth:** Responsible for user authentication and authorization, including registration, login, and token generation.
*   **UserPanel.Data:** Contains Entity Framework Core models and data access logic for interacting with the database.
*   **UserPanel.Shared:** A shared library containing common interfaces, models, and DTOs used across other projects.
*   **UserPanel.Seeder:** Contains logic for seeding the database with initial data.

├── UserPanel.API/
│   ├── Controllers/
│   │   ├── FollowUpChatsController.cs
│   │   └── TicketsController.cs
│   ├── Profiles/
│   │   └── MappingProfile.cs
│   ├── Program.cs
│   ├── appsettings.json
│   └── ...
├── UserPanel.Auth/
│   ├── Controllers/
│   │   └── AccountController.cs
│   ├── DTOs/
│   │   ├── LoginDto.cs
│   │   └── RegisterDto.cs
│   ├── Models/
│   │   └── ApplicationUser.cs
│   ├── Services/
│   │   ├── ITokenService.cs
│   │   └── TokenService.cs
│   ├── Program.cs
│   ├── appsettings.json
│   └── ...
├── UserPanel.Data/
│   ├── UserPanelDbContext.cs
│   └── ...
├── UserPanel.Shared/
│   ├── Interfaces/
│   │   ├── ITicketRepository.cs
│   │   └── IFollowUpChatRepository.cs
│   ├── Models/
│   │   ├── Ticket.cs
│   │   └── FollowUpChat.cs
│   └── ...
├── UserPanel.Seeder/
│   └── DatabaseSeeder.cs
├── UserPanel.sln
└── ...


## Features

*   **User Authentication:** Secure user registration and login using ASP.NET Core Identity and JWT Bearer authentication.
*   **Ticket Management:** Users can create and respond to tickets. Admins can also respond to tickets and close them.
*   **Follow-Up Chats:** Real-time or asynchronous communication within the context of a ticket.
*   **Role-Based Authorization:** Implements role-based access control (RBAC) with "Admin" and "User" roles.
*   **Database Seeding:** Provides a mechanism to seed the database with initial data.
*   **CORS Support:** Configured for cross-origin requests from `http://localhost:3000`.

## Technologies Used

*   ASP.NET Core 9.0
*   Entity Framework Core 9.0
*   ASP.NET Core Identity
*   JWT Bearer Authentication
*   AutoMapper
*   SQL Server
*   Swagger/OpenAPI

## Getting Started

### Prerequisites

*   .NET SDK 9.0 or later
*   SQL Server instance
*   Node.js and npm (if using the frontend application on port 3000)

### Installation

1.  Clone the repository:

    ```bash
    git clone git@github.com:sicario1380/UserPanelAPIASP.git
    ```

2.  Navigate to the project directory:

    ```bash
    cd UserPanelAPIASP
    ```

3.  Set up the database connection strings in the `appsettings.json` files of both `UserPanel.API` and `UserPanel.Auth` projects. Ensure that the "IdentityConnection" string points to the same database.

4.  Apply database migrations:

    ```bash
    # In the UserPanel.API directory
    dotnet ef database update -p ../UserPanel.Data -s UserPanel.API
    # In the UserPanel.Auth directory
    dotnet ef database update -p ../UserPanel.Auth -s UserPanel.Auth
    ```

5.  Run the API projects:

    ```bash
    # In the UserPanel.API directory
    dotnet run
    # In the UserPanel.Auth directory
    dotnet run
    ```

## API Endpoints

Refer to the Swagger documentation at `https://localhost:5001/swagger` (or the corresponding port for your API project) for a complete list of

Introduction

Welcome to the UserPanel project! This project is a ticketing system designed to handle user queries and support requests. It includes a robust authentication system, allowing users to register, log in, and manage their tickets, while admins can respond to tickets and manage the overall ticketing system.
Features

    User Authentication: Users can register and log in to manage their tickets.

    Role-based Access Control: Different roles (Admin, User) with specific permissions.

    Ticket Management: Users can create, view, update, and delete tickets.

    Admin Controls: Admins can respond to tickets, terminate, and manage overall ticket flow.

    Follow-Up Chats: Users and admins can engage in follow-up chats on tickets.

    Security: JWT-based authentication and HTTPS for secure communication.

Technologies Used

    .NET 6

    ASP.NET Core

    Entity Framework Core

    AutoMapper

    JWT Authentication

    SQL Server

    Swagger for API Documentation

Setup and Installation
Prerequisites

    .NET 6 SDK

    SQL Server

Clone the Repository
sh  
git remote add origin git@github.com:sicario1380/UserPanelAPIASP.git

Configure Databases

Update your connection strings in appsettings.json for both UserPanel.Auth and UserPanel.API:
json
"ConnectionStrings": {
  "IdentityConnection": "Your_Identity_DB_Connection_String",
  "DefaultConnection": "Your_Default_DB_Connection_String"
}

dotnet ef database update --project UserPanel.Auth
dotnet ef database update --project UserPanel.API

dotnet restore

dotnet build

dotnet run --project UserPanel.Auth

dotnet run --project UserPanel.API

Usage
Register a New User
curl --insecure -X POST https://localhost:5001/api/account/register -H "Content-Type: application/json" -d '{
  "username": "user@example.com",
  "email": "user@example.com",
  "password": "UserPassword123!"
}'

Create a Ticket

Replace <YOUR_JWT_TOKEN> with the token received from the login.
curl --insecure -X POST https://localhost:5001/api/tickets -H "Content-Type: application/json" -H "Authorization: Bearer <YOUR_JWT_TOKEN>" -d '{
  "subject": "Sample Ticket",
  "type": "General",
  "description": "This is a test ticket."
}'
API Endpoints
UserPanel.Auth

    POST /api/account/register - Register a new user.

    POST /api/account/login - Authenticate user and obtain JWT token.

UserPanel.API

    GET /api/tickets - Get all tickets.

    GET /api/tickets/{id} - Get ticket by ID.

    POST /api/tickets - Create a new ticket.

    PUT /api/tickets/{id} - Update a ticket.

    DELETE /api/tickets/{id} - Delete a ticket.

    POST /api/followupchats/admin - Admin sends a message.

    POST /api/followupchats/user - User sends a message.



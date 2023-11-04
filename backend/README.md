![Backend CI](https://github.com/SQUASHD/GoNAD-StudentBlog/actions/workflows/ci-backend.yml/badge.svg)
![Backend CD](https://github.com/SQUASHD/GoNAD-StudentBlog/actions/workflows/cd-backend.yml/badge.svg)

# Student Blog API

This ASP.NET Core backend started initially as coursework for my vocational degree in Backend Programming, but has continued to grow as I developed it beyond [the initial requirements.](./REQUIREMENTS.md)

The API is made using `ASP.NET Core` and `Entity Framework Core` with a `MySQL` database.

## Table of Contents
- [Quick Start](#quick-start)
- [Using JetBrains Rider](#using-jetbrains-rider)
- [Using Docker](#using-docker)
- [Using Docker Compose](#using-docker-compose)
- [Setting Up Environment Variables](#setting-up-environment-variables)
- [Features](#features)
- [Advanced Implementations](#advanced-implementations)

## Quick Start

Follow these steps to set up the project:

1. Clone the repository.

2. Navigate to the backend directory:

```bash
cd GoNAD-StudentBlog/backend
```

3. Copy `.env.example` to `.env` and configure your environment variables. See [Setting Up Environment Variables](#setting-up-environment-variables) for more information.

```bash
cp .env.example .env
```

4. Ensure you have the .NET Core CLI tools installed to work with Entity Framework migrations. If not, install them:

```bash
dotnet tool install --global dotnet-ef
```

5. Run migrations to set up your database schema:

```bash
dotnet ef database update
```

_Note_: If you are using a Vitess-based MySQL database like PlanetScale, you may need to generate SQL scripts for the migrations and apply them manually.

```bash
dotnet ef migrations script
```

### Using JetBrains Rider

1. Open the solution file in JetBrains Rider.

2. Ensure the correct startup project is selected.

3. Click 'Run' or use the shortcut Shift + F10 to start the API.

Make sure to have a MySQL database running and the connection string correctly configured in `appsettings.json` or your user secrets.

### Using Docker

1. Build the Docker image for the backend service:

```bash
docker build -t studentblog-backend .
```

2. Run the Docker container, making sure to replace `<port>` with your preferred port number:

```bash
docker run -p <port>:80 studentblog-backend
```

_NOTE_: You need to have a MySQL database accessible for the API to connect to. You might also need to adjust your Docker run command to include environment variables, such as your `DATABASE_URL` and `JWT_KEY`, unless they are baked into your image or you're using Docker secrets.

### Using Docker Compose

1. To run the backend with Docker Compose, use:

```bash
docker-compose up
```

## Setting Up Environment Variables

To properly configure your application, you'll need to set various environment variables. You can define these variables within a `.env` file or within your `appsettings.json` if you're running the application locally.

1. Create a `.env` file from the example provided:

```bash
cp .env.example .env
```

2. Open the `.env` file and input your actual configuration values for the `DATABASE_URL` and `JWT_KEY`.

Example:

```
DATABASE_URL=mysql://username:password@host:port/database
JWT_KEY=YourJWTSecretKey
```

Alternatively, if you are only running the API locally, you may add the database connection and JWT configuration to your `appsettings.json` or your user secrets. The `appsettings.json` is pre-configured with standard JWT issuers.

Example `appsettings.json` content:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "YourDatabaseConnectionString"
  },
  "Jwt": {
    "AccessIssuer": "StudentBlogAPI-Access",
    "RefreshIssuer": "StudentBlogAPI-Refresh",
    "Key": "YourJWTSecretKey"
  }
}
```

## Features

- [x] N-tier architecture
  - Controller, Service and Repository layers, with mapping between model and DTOs
- [x] CRUD operations for users, posts and comments, and revoked tokens
- [x] Validation
- [x] Secure authentication and authorization using JWTs.
  - Utilizing both Access and Refresh tokens for enhanced security.

## Advanced Implementations

- _Custom Validation Filters_: Streamlining input validation by intercepting requests and handling inconsistencies before they reach the controllers.
- _Extended ControllerBase_: Enhanced base controller to encapsulate common functionalities,d
- _Middleware Integration_:
  - Error handling middleware to catch exceptions globally, preventing unhandled errors from slipping through.
  - JWT Authentication middleware
  - Basic rate limiting middleware
- _Comprehensive Error Model_: Tailored error responses that provide clarity and context for client-side consumption.
- _Layered Architecture_:
  - Controllers, services, and repositories — Clearly separated concerns for simplified maintenance and potential scalability.
- _Data Mapping_:
  - Utilizing object mappers to seamlessly convert entities to and from DTOs (Data Transfer Objects).
  - Distinct request, response, and internal DTOs, ensuring data consistency and security.
- _Entity Framework Core_: Entity Framework Core for its robust ORM capabilities and seamless integration with ASP.NET Core.

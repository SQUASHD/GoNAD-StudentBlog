![Backend CI](https://github.com/SQUASHD/GoNAD-StudentBlog/actions/workflows/ci-backend.yml/badge.svg)
![Backend CD](https://github.com/SQUASHD/GoNAD-StudentBlog/actions/workflows/cd-backend.yml/badge.svg)

# Student Blog API

This ASP.NET Core backend started initially as coursework for my vocational degree in Backend Programming, but has
continued to grow as I developed it beyond [the initial requirements.](./REQUIREMENTS.md)

The API is made using `ASP.NET Core` and `Entity Framework Core` with a `MySQL` database.

## Quick Start

Follow these steps to set up the project:

1. Clone the repository

2. Navigate to the frontend directory:

```bash
cd GoNAD-StudentBlog/backend
```

3. Copy `.env.example` to `.env` and configure your environment variables.

```bash
cp .env.example .env
# Edit .env with your specific variables
```

### Using JetBrains Rider

1. Open the solution file in JetBrains Rider.

2. Ensure the correct startup project is selected.

3. Click 'Run' or use the shortcut Shift + F10 to start the API.

Make sure to have a MySQL database running and the connection string correctly configured in appsettings.json or your user secrets.

### Using Docker

1. Build the Docker image for the backend service:

```bash
docker build -t studentblog-backend .
```

2. Run the Docker container, making sure to replace `<port>` with your preferred port number:

```bash
docker run -p <port>:80 studentblog-backend
```

*NOTE*: You need to have a MySQL database accessible for the API to connect to. You might also need to adjust your Docker run command to include environment variables, such as your DATABASE_URL and JWT_KEY, unless they are baked into your image or you're using Docker secrets.

### Using Docker Compose

1. To run the backend with Docker Compose, use:

```bash
docker-compose up
```

## Environment Variables

Open the .env file and replace the placeholder values with your actual configuration values for `DATABASE_URL` and `JWT_KEY`.

If you are only running the API locally, you may choose to add a database connection and a JWT to your `appsettings.json`
or your `user-secrets`. I've added a standard Access and Refresh JWT Issuer to `appsettings.json`.

The API is set up to work with a MySQL database, but you could easily change that if you want.

Example

```
DATABASE_URL
JWT_KEY=
```

```json
# appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": ""
  },
  "Jwt": {
    "AccessIssuer": "StudentBlogAPI-Access",
    "RefreshIssuer": "StudentBlogAPI-Refresh",
    "Key": ""
  }
}

```



## Features

- [x] N-tier architecture
  - Controller, Service and Repository layers, with mapping between model and DTOs
- [x] CRUD operations for users, posts and comments
- [x] Validation
- [x] Secure authentication and authorization using JWTs.
  - Utilizing both Access and Refresh tokens for enhanced security.

## Advanced Implementations

- _Custom Validation Filters_: Streamlining input validation by intercepting requests and handling inconsistencies
  before they reach the controllers.
- _Extended ControllerBase_: Enhanced base controller to encapsulate common functionalities, promoting DRY (Don't Repeat
  Yourself) principles.
- _Middleware Integration_:
  - Error handling middleware to catch exceptions globally, preventing unhandled errors from slipping through.
  - JWT Authentication middleware
  - Basic rate limiting middleware
- _Comprehensive Error Model_: Tailored error responses that provide clarity and context for client-side consumption.
- _Layered Architecture_:
  - Controllers, services, and repositories — Clearly separated concerns for simplified maintenance and potential
    scalability.
- _Data Mapping_:
  - Utilizing object mappers to seamlessly convert entities to and from DTOs (Data Transfer Objects).
  - Distinct request, response, and internal DTOs, ensuring data consistency and security.
- _Entity Framework Core_: Entity Framework Core for its robust ORM capabilities and seamless integration with ASP.NET Core.

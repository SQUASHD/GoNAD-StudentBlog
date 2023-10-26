# Student Blog API

This ASP.NET Core backend started initially as coursework my vocational degree in Backend Programming, but has continued to grow as I developed it beyond [the initial requirements.](./REQUIREMENTS.md)

The API is made using ASP.NET Core and Entity Framework Core with a MySQL database.

## Features

- [X] N-tier architecture with all the bells and whistles
- [X] CRUD operations for users, posts and comments
- [X] Validation
- [X] Secure authentication and authorization using JWTs. 
    - Utilizing both Access and Refresh tokens for enhanced security. 
    - TODO: RBAC?
- [X] Detailed logging

## Advanced Implementations
- *Custom Validation Filters*: Streamlining input validation by intercepting requests and handling inconsistencies before they reach the controllers.
- *Extended ControllerBase*: Enhanced base controller to encapsulate common functionalities, promoting DRY (Don't Repeat Yourself) principles.
- *Middleware Integration*:
    - Custom authorization middleware to manage access controls efficiently.
    - Error handling middleware to catch exceptions globally, preventing unhandled errors from slipping through.
- *Comprehensive Error Model*: Tailored error responses that provide clarity and context for client-side consumption.
- *Layered Architecture*:
    - Controllers, services, and repositories — Clearly separated concerns for simplified maintenance and potential scalability.
- *Data Mapping*:
    - Utilizing object mappers to seamlessly convert entities to and from DTOs (Data Transfer Objects).
    - Distinct request, response, and internal DTOs, ensuring data consistency and security.
- Entity Framework Core: because why not?
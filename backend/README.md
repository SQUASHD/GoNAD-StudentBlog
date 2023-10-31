# Student Blog API

This ASP.NET Core backend started initially as coursework for my vocational degree in Backend Programming, but has
continued to grow as I developed it beyond [the initial requirements.](./REQUIREMENTS.md)

The API is made using ASP.NET Core and Entity Framework Core with a MySQL database.

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

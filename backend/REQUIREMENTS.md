# Updated Project Description: Student Blog

## Security

### Authentication

- Must be a registered user to use the REST API and must send a username and password with each request to the REST API.
- Can register a new user without having to authenticate with a username and password.
- Unauthorized users should receive HTTP status 401 Unauthorized.

### Authorization

- Users can only update, post data, and delete their own posts and comments and will receive HTTP status code 401 Unauthorized if they attempt to modify data that does not belong to them.

## Components

### Presentation (API)

#### Endpoints (Controller)

**Users**

_Open for all_

- `POST /api/v1/users/register`: Register a new user.

_Requires Authentication_

- `GET /api/v1/users?page=1&size=10`: Retrieve information about all users (with pagination).
- `GET /api/v1/users/{id}`: Retrieve information about a specific user.

_Optional_

- `GET /api/v1/users/{id}/posts`: Retrieve all posts for this user.

_Requires Authentication / Authorization_

- `DELETE /api/v1/users/{id}` (check authorization)
- `UPDATE /api/v1/users/{id}` (check authorization)

**Posts**

_Requires Authentication_

- `GET /api/v1/posts?page=1&size=10`: Retrieve all posts (with pagination).
- `GET /api/v1/posts/{postId}`: Retrieve a specific post.
- `POST /api/v1/posts`: Add a new post.

_Optional_

- `GET /api/v1/posts/{postId}/comments`: Retrieve all comments for a specific post.

_Requires Authentication / Authorization_

- `PUT /api/v1/posts/{postId}`: Update a specific post, only own posts.
- `DELETE /api/v1/posts/{postId}`: Delete a specific post, only own posts.

**Comments**

_Requires Authentication_

- `GET /api/v1/comments?page=1&size=10`: Retrieve all comments (with pagination).
- `POST /api/v1/comments/{postId}`: Add a comment to a specific post.

_Requires Authentication / Authorization_

- `PUT /api/v1/comments/{commentId}`: Update a specific comment. Only own comments.
- `DELETE /api/v1/comments/{commentId}`: Delete a specific comment. Only own comments.

### Middleware

- Basic Authentication: Verifies user credentials.
- Logging: Logs important events, errors, and system information.

### Service Layer

- Handles business logic.
- Performs authorization based on business rules (e.g., a user can only edit/delete their own posts).
- Calls the Repository Layer for database access.

### Repository Layer

- Direct interaction with the MySQL database - Entity Framework.
- CRUD operations for 'User', 'Posts', and 'Comments'.

## Functionality

### User

- Registration with username and password (hashing with salt using BCrypt or similar).

### Posts

- Users can write new posts.
- Users can edit and delete their own posts.
- All registered users can view all posts.

### Comments

- Authenticated users can comment on posts.
- Comment authors can edit and delete their own comments.
- All can view comments associated with a post.

## Technical Specifications

### Authentication

- Implemented as middleware using Basic Authentication.

### Database

- MySQL with tables 'User', 'Posts', and 'Comments'.
- Use of Entity Framework.

### Logging

- Use of a logging solution like Serilog or built-in logging in ASP.NET Core to log system events.

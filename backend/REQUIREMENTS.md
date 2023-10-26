# Project Description: Student Blog

These are the original specifications for the Student Blog project, translated from Norwegian to English with the help of ChatGPT.

## Purpose:
The goal is to create a simple blogging platform where users can register, log in, write posts, and leave comments on posts. This will be a Controller-based API.

## Components:
1. **Presentation (API):**
    - **Endpoints (Controller):**
        - **User:**
            - `POST /api/v1/users/register`: Register a new user.
            - `GET /api/v1/users/`: Retrieve information about all users.
            - `GET /api/v1/users/{id}`: Retrieve information about a specific user.
            - `DELETE /api/v1/users/{id}`: Delete a specific user.
            - `UPDATE /api/v1/users/{id}`: Update a specific user's information.
        - **Admin:** (only for admin users)
            - `DELETE/UPDATE /api/v1/....`: Admin operations.
        - **Posts:**
            - `POST /api/v1/posts`: Add a new post (authentication required).
            - `GET /api/v1/posts`: Retrieve all posts.
            - `GET /api/v1/posts/{postId}`: Retrieve a specific post.
            - `PUT /api/v1/posts/{postId}`: Update a specific post, only own posts (authentication required).
            - `DELETE /api/v1/posts/{postId}`: Delete a specific post, only own posts (authentication required).
        - **Comments:**
            - `POST /api/posts/{postId}/comments`: Add a comment to a specific post.
            - `GET /api/posts/{postId}/comments`: Retrieve all comments for a specific post.
            - `PUT /api/comments/{commentId}`: Update a specific comment. Only own comments (authentication required).
            - `DELETE /api/comments/{commentId}`: Delete a specific comment. Only own comments (authentication required).
    - **Middleware:**
        - Basic Authentication: Verifies user credentials.
        - Logging: Logs important events, errors, and system information.
        - Data validation.

2. **Service Layer:**
    - Handles business logic.
    - Performs authorization based on business rules (e.g., a user can only edit/delete their own posts).
    - Calls the Repository Layer for database access.

3. **Repository Layer:**
    - Direct interaction with the MySQL database - Entity Framework.
    - CRUD operations for 'User', 'Posts', and 'Comments'.

## Functionality:
1. **User:**
    - Registration with username and password (hashing with salt using BCrypt or similar).
2. **Posts:**
    - Users can write new posts.
    - Users can edit and delete their own posts.
    - Everyone can view posts.
3. **Comments:**
    - Authenticated users can comment on posts.
    - Comment authors can edit and delete their own comments.
    - Everyone can view comments associated with a post.

## Technical Specifications:
1. **Authentication:** Implemented as middleware using Basic Authentication.
2. **Database:**
    - MySQL with the tables 'User', 'Posts', and 'Comments'.
    - Use of Entity Framework.
3. **Logging:** Use of a logging solution like Serilog or built-in logging in ASP.NET Core to log system events.

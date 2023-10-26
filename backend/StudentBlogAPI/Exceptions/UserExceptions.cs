namespace StudentBlogAPI.Exceptions;

public class PasswordMismatchException : Exception
{
    private const string DefaultMessage = "Passwords do not match.";

    public PasswordMismatchException()
        : base(DefaultMessage)
    {
    }

    public PasswordMismatchException(string message)
        : base(string.IsNullOrEmpty(message) ? DefaultMessage : message)
    {
    }

    public PasswordMismatchException(string message, Exception inner)
        : base(string.IsNullOrEmpty(message) ? DefaultMessage : message, inner)
    {
    }
}

public class UserNotAuthorizedException : Exception
{
    private const string DefaultMessage = "You are not authorized to perform this action.";

    public UserNotAuthorizedException()
        : base(DefaultMessage)
    {
    }

    public UserNotAuthorizedException(string message)
        : base(string.IsNullOrEmpty(message) ? DefaultMessage : message)
    {
    }

    public UserNotAuthorizedException(string message, Exception inner)
        : base(string.IsNullOrEmpty(message) ? DefaultMessage : message, inner)
    {
    }
}

public class UserAlreadyExistsException : Exception
{
    private const string DefaultMessage = "User with that username or email already exists.";

    public UserAlreadyExistsException()
        : base(DefaultMessage)
    {
    }

    public UserAlreadyExistsException(string message)
        : base(string.IsNullOrEmpty(message) ? DefaultMessage : message)
    {
    }

    public UserAlreadyExistsException(string message, Exception inner)
        : base(string.IsNullOrEmpty(message) ? DefaultMessage : message, inner)
    {
    }
}

public class BadLoginRequestException : Exception
{
    private const string DefaultMessage = "Invalid username or password.";

    public BadLoginRequestException()
        : base(DefaultMessage)
    {
    }

    public BadLoginRequestException(string message)
        : base(string.IsNullOrEmpty(message) ? DefaultMessage : message)
    {
    }

    public BadLoginRequestException(string message, Exception inner)
        : base(string.IsNullOrEmpty(message) ? DefaultMessage : message, inner)
    {
    }
}

public class UserForbiddenException : Exception
{
    private const string DefaultMessage = "You do not have permission to perform this action.";

    public UserForbiddenException()
        : base(DefaultMessage)
    {
    }

    public UserForbiddenException(string message)
        : base(string.IsNullOrEmpty(message) ? DefaultMessage : message)
    {
    }

    public UserForbiddenException(string message, Exception inner)
        : base(string.IsNullOrEmpty(message) ? DefaultMessage : message, inner)
    {
    }
}
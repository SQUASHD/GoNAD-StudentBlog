using Microsoft.AspNetCore.Authorization;
using StudentBlogAPI.Data;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Middleware;

public class AdminRoleRequirement : IAuthorizationRequirement
{
}

public class AdminRoleHandler : AuthorizationHandler<AdminRoleRequirement>
{
    private readonly StudentBlogDbContext _dbContext;
    private readonly IJwtService _jwtService;

    public AdminRoleHandler(StudentBlogDbContext context, IJwtService jwtService)
    {
        _dbContext = context;
        _jwtService = jwtService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        AdminRoleRequirement requirement)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService);;

        var user = await _dbContext.Users.FindAsync(currentUserId);

        if (user is { IsAdmin: true })
            context.Succeed(requirement);
    }

    private object GetCurrentUserId(IJwtService jwtService)
    {
        throw new NotImplementedException();
    }
}
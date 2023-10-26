using Microsoft.AspNetCore.Mvc;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Extensions;

public static class ControllerExtensions
{
    public static int GetCurrentUserId(this ControllerBase controller, IJwtService jwtService)
    {
        var httpContext = controller.HttpContext;
        var token = jwtService.GetTokenFromRequest(httpContext);
        return jwtService.GetUserIdFromToken(token);
    }
}
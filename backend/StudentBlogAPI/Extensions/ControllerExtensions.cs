using Microsoft.AspNetCore.Mvc;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Extensions;

public static class ControllerExtensions
{
    public static int GetCurrentUserId(this ControllerBase controller, IJwtService jwtService,
        ITokenValidator tokenValidator)
    {
        var token = jwtService.GetTokenFromRequest(controller.HttpContext);
        return tokenValidator.GetUserIdFromToken(token);
    }
}
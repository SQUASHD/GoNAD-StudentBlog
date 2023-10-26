using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StudentBlogAPI.Validators;

public class ValidationErrorHandlingFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        var errorResponse = new ValidationErrorResponse();

        foreach (var state in context.ModelState)
        foreach (var error in state.Value.Errors)
        {
            var validationError = new ValidationError
            {
                Field = state.Key,
                Message = error.ErrorMessage
            };

            errorResponse.Errors.Add(validationError);
        }

        context.Result = new BadRequestObjectResult(errorResponse);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // nothing to do post-execution in this case
    }
}
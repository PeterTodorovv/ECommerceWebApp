using FootballApp.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace FootballApp.API
{
    public class ApiResponseWrapperFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var response = new ApiResponse<object>
            {
                Status = ExtractStatusCode(context),
                Result = context.Result is ObjectResult result ? result.Value : null
            };

            if (context.Exception is Exception exception)
            {
                HandleExceptions(context, response, exception);
            }

            context.Result = new ObjectResult(response);
            context.HttpContext.Response.StatusCode = (int)response.Status;
        }

        private static void HandleExceptions(ActionExecutedContext context, ApiResponse<object> response, Exception exception)
        {
            response.Errors = new List<ErrorModel>();

            response.Errors.Add(new ErrorModel(exception));

            response.Status = exception is BaseError baseError ? (int)baseError.StatusCode : (int)HttpStatusCode.InternalServerError;

            context.ExceptionHandled = true;
        }

        public int ExtractStatusCode(ActionExecutedContext context)
        {
            return context.Result switch
            {
                IStatusCodeActionResult result when result.StatusCode != (int)HttpStatusCode.NoContent => (int)result.StatusCode!,
                _ => (int)HttpStatusCode.OK
            };
        }

        private static string GetErrorMessage(ModelStateEntry value)
        {
            return value.Errors.FirstOrDefault()?.ErrorMessage ?? "";
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var response = new ApiResponse<object>
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Errors = context.ModelState.ToList().Where(x => x.Value is { ValidationState: ModelValidationState.Invalid })
                    .Select(x => new ErrorModel
                    {
                        Message = GetErrorMessage(x.Value)
                    }).ToList(),
                    Result = null!
                };

                context.Result = new ObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)response.Status;
            }
        }
    }
}

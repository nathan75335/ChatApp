using ChatApp.BusinessLogic.Exceptions;
using System.Text;

namespace ChatApp.API.Middlewares;

public class CustomExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.ContentType = "application/json";

        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync<Error>(new()
            {
                Message = ex.Message,
            });
        }
        catch(ExistException ex)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsJsonAsync<Error>(new()
            {
                Message = ex.Message,
            });
        }
        catch(Exception ex)
        {
            context.Response.StatusCode= StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync<Error>(new()
            {
                Message = ex.Message
            });
        }
    }

    public class Error
    {
        public string Message { get; set; }
    }
}

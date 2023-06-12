using ToDoList.Authentication;

namespace ToDoList;

public class ExceptionHandler
{
    private readonly RequestDelegate m_next;
    private readonly ILogger<ExceptionHandler> m_logger;

    public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
    {
        m_next = next;
        m_logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await m_next(context);
        }
        catch (UserNotFoundException ex)
        {
            m_logger.LogError(ex, "The user was not found in the controller.");

            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new {
                Message = "No user found in the application."
            });
        }
        catch (Exception ex)
        {
            m_logger.LogError(ex, "An error happened while processing the request.");

            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Details = ex.ToString()
            });
        }
    }
}
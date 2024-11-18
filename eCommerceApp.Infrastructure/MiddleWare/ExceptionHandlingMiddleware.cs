using eCommerceApp.Application.Services.Interfaces.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerceApp.Infrastructure.MiddleWare
{
    public class ExceptionHandlingMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (DbUpdateException ex)
            {
                var logger = httpContext.RequestServices.GetRequiredService<IAppLogger <ExceptionHandlingMiddleware> >();

                httpContext.Response.ContentType = "application/json";
                //var innerException = ex.InnerException as SqlException;
                //if (innerException != null)

                if (ex.InnerException is SqlException innerException)
                {
                    logger.LogError(innerException, "Sql Exception");

                    switch (innerException.Number)
                    {
                        case 2627: // unique constraint violation
                            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                            await httpContext.Response.WriteAsync("Unique constraint violation");
                            break;

                        case 515: // cannot insert null
                            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await httpContext.Response.WriteAsync("Cannot insert null");
                            break;

                        case 547: // Foreign Key constraint violation
                            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                            await httpContext.Response.WriteAsync("Foreign Key constraint violation");
                            break;

                        default:
                            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            await httpContext.Response.WriteAsync("Error occurred while saving the changes");
                            break;

                    }

                }
                else
                {
                    logger.LogError(ex, "Related EFCore Exception");
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await httpContext.Response.WriteAsync("Error occurred while saving the changes");
                }
            }
            catch (Exception ex) 
            {
                var logger = httpContext.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddleware>>();
                logger.LogError(ex, "Unknown Exception");

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsync("An error occurred: "+ ex.Message);
            }
        }
    }
}

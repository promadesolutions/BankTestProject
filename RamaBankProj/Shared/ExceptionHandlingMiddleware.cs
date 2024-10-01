// using System.Net;

// namespace RamaBankProj.Shared;

// public class ExceptionHandlingMiddleware
// {
//     private readonly RequestDelegate _next;
//     private readonly ILogger<ExceptionHandlingMiddleware> _logger;

//     public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
//     {
//         _next = next;
//         _logger = logger;
//     }

//     public async Task InvokeAsync(HttpContext context)
//     {
//         try
//         {
//             await _next(context);
//         }
//         catch (Exception ex)
//         {
//             _logger.LogError(ex, "An unhandled exception occurred.");
//             await HandleExceptionAsync(context, ex);
//         }
//     }

//     private static Task HandleExceptionAsync(HttpContext context, Exception exception)
//     {
//         context.Response.ContentType = "application/json";
//         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

//         var response = new { message = "An error occurred while processing your request." };
//         return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
//     }
// }
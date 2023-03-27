using System.Net;
using Lean.Contracts.MessageModels;
using Lean.Domain.Exceptions;
using Newtonsoft.Json;

namespace Lean.Web.MiddleWares
{
    public class ExceptionHandlingMiddleWare
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionHandlingMiddleWare(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception e) 
            {
                await HandleExceptionAsync(httpContext, e);
                Console.WriteLine(e.ToString());

            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            (MessageModel messageModel, int statusCode) = ex switch
            {
                BadRequestException => (new ErrorMessage(ex.Message), (int) HttpStatusCode.BadRequest),
                NotFoundException => (new ErrorMessage(ex.Message), (int) HttpStatusCode.NotFound),
                _ => (new ErrorMessage(RM.Exceptions.InternalServerError), (int) HttpStatusCode.InternalServerError),
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            if (statusCode == (int) HttpStatusCode.InternalServerError)
                Console.WriteLine(ex.Message);

            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(messageModel));
        }
    }
}
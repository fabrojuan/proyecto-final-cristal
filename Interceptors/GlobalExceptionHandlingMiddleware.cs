using System.Net;
using System.Text.Json;
using iText.Commons.Actions.Confirmations;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MVPSA_V2022.Exceptions;

namespace MVPSA_V2022.Interceptors
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next) {
            _next = next; 
        }

        public async Task Invoke(HttpContext context) {
            try {
                await _next(context);
            } catch(Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception) {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            ResponseModel exModel = new ResponseModel();

            Console.WriteLine(exception);
            switch(exception) {                
                case BusinessException ex:
                    exModel.responseCode = (int) HttpStatusCode.BadRequest;
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    exModel.responseMessage = ex.Message;
                    break;
                case LoginException ex:
                    exModel.responseCode = (int) HttpStatusCode.Unauthorized;
                    response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    exModel.responseMessage = ex.Message;
                    break;
                case ResourceNotFoundException ex:
                    exModel.responseCode = (int) HttpStatusCode.NotFound;
                    response.StatusCode = (int) HttpStatusCode.NotFound;
                    exModel.responseMessage = ex.Message;
                    break;
                default:
                    exModel.responseCode = (int) HttpStatusCode.InternalServerError;
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    exModel.responseMessage = "Ocurrió un error, por favor contacte al administrador del sistema";
                    break;
            }
            
            var exResult = JsonSerializer.Serialize(exModel);
            await context.Response.WriteAsync(exResult);

        }


    }
}
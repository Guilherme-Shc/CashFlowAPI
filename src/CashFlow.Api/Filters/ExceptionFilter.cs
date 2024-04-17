using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

//Só lembrando para quando criar outro filtro, é necessário adiciona-lo dentro do Program.cs
namespace CashFlow.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //o if entra como CashFlowException pois se ele trigrar com ele significa que ele trigará com qualquer tipo de exception
            //(Validation, NotFound, etc) assim será necessário só um if e não vários ifs elses
            if(context.Exception is CashFlowException)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnknownError(context);
            }
        }
        private void HandleProjectException(ExceptionContext context)
        {
            if(context.Exception is ErrorOnValidationException)
            {

                //Assim tbm funciona var ex = context.Exception as ErrorOnValidationException;
                var ex = (ErrorOnValidationException)context.Exception;

                var errorResponse = new ResponseErrorJson(ex.Errors);

                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                //se atentando ao tipo de Object que é devolvido por cada tipo especifico de error, nesse caso BadRequestObjectResult
                context.Result = new BadRequestObjectResult(errorResponse);

            }
            //else usado para caso esse if entre como um error seja cashflow no entanto não entre no if acima
            else
            {
                var errorResponse = new ResponseErrorJson(context.Exception.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                //se atentando ao tipo de Object que é devolvido por cada tipo especifico de error, nesse caso BadRequestObjectResult
                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }

        private void ThrowUnknownError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);

        }
    }
}

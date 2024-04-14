using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register([FromBody] RequestRegisterExpenseJson request)
        {
            try
            {
                var useCase = new RegisterExpenseUseCase();

                var response = useCase.Execute(request);

                return Created(string.Empty, response);
            }
            catch (ArgumentException ex)
            {
                //esse var erroResponse são usados para retornar a mensagem de error como JSON e não como String
                var errorResponse = new ResponseErrorJson
                {
                    ErrorMessage = ex.Message
                };

                return BadRequest(errorResponse);
            }
            catch
            {
                //esse var erroResponse são usados para retornar a mensagem de error como JSON e não como String
                var errorResponse = new ResponseErrorJson
                {
                    ErrorMessage = "unknown error"
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}

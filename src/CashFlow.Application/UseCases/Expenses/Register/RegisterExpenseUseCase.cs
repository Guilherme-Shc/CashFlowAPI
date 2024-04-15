using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase
    {
        public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            Validate(request);

            return new ResponseRegisteredExpenseJson();
        }

        private void Validate(RequestRegisterExpenseJson request)
        {
            var validator = new RegisterExpenseValidator();

            var result = validator.Validate(request);

            //se IsValid no result for true significa que a request está valida (sem erros) se não ela entra no if e da a(s) msg(s) de erro
            if (result.IsValid == false)
            {
                //transformando o valor de erros de Result em uma lista de Strings com as mensagens pois é só o que será usado
                var errorMessages = result.Errors.Select(param => param.ErrorMessage).ToList(); //To list para transformar em Lista .-.

                throw new ArgumentException();
            }
        }
    }
}

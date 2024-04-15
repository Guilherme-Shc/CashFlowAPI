using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
    {
        public RegisterExpenseValidator() 
        {
            //os EXPENSE aqui em baixo são parâmetro do REQUESTREGISTEREXPENSEJSON acima
            RuleFor(expense => expense.Title).NotEmpty().WithMessage("The title is required.");
            RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage("The amount must be greater than zero.");
            RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Expenses cannot be for the future.");
            RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage("Payment type is not valid.");
        }
        
    }
}
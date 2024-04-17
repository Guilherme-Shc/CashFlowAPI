using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register
{
    public class ResgisterExpenseValidatorTests
    {
        [Fact]
        public void Success()
        {
            //Arrange
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build(); //não é necessário chama-la como new pois ela é declarada como static em seu arquivo
            //Act

            var result = validator.Validate(request);

            //Assert(FluentValidation)
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Date_Future()
        {
            //Arrange
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build(); 
            request.Date = DateTime.UtcNow.AddDays(1);

            //Act
            var result = validator.Validate(request);

            //Assert(FluentValidation)
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.DATE_ERROR));
        }

        [Fact]
        public void Error_Payments_Type_Invalid()
        {
            //Arrange
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build(); //não é necessário chama-la como new pois ela é declarada como static em seu arquivo
            request.PaymentType = (PaymentType)700;

            //Act
            var result = validator.Validate(request);

            //Assert(FluentValidation)
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_ERROR));
        }

        //Theory é usado ao invés de fact caso seja necessário a função executar com mais de um parametro para conferir o erro,
        //nesse caso numeros menores ou iguais a 0
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-7)]
        public void Error_Amount_Invalid(decimal amount) //o valor de InlineData é passado para o amount
        {
            //Arrange
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build(); 
            request.Amount = amount;

            //Act
            var result = validator.Validate(request);

            //Assert(FluentValidation)
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_REQUIRED));
        }

        [Theory]
        [InlineData("         ")]
        [InlineData("")]
        [InlineData(null)]

        public void Error_Title_Empty(string title)
        {
            //Arrange
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Title = title;

            //Act
            var result = validator.Validate(request);

            //Assert(FluentValidation)
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
        }
    }
}

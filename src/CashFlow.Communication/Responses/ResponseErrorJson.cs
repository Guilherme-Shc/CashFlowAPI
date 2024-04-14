namespace CashFlow.Communication.Responses
{
    public class ResponseErrorJson
    {
        public required string ErrorMessage { get; set; } = string.Empty;

        //em códigos mais antigos o REQUIRED não existia então era necessário ser criado um construtor:

        /*
        public string ErrorMessage { get; set; } = string.Empty;
            
        public ResponseErrorJson(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        */
    }
}

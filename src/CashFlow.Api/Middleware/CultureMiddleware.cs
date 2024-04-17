using System.Globalization;

namespace CashFlow.Api.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        public CultureMiddleware(RequestDelegate next)
        {
            _next=next;
        }
        public async Task Invoke(HttpContext context)
        {
            //criando uma lista com todos os idiomas suportados pelo .NET
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            //extraindo o idioma requisitado
            var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            //implantando o idioma default
            var baseCultureInfo = new CultureInfo("en");

            //alterando o idioma default caso seja encontrado um arquivo não nulo que se equivala a linguagem requisitada
            if (
                string.IsNullOrWhiteSpace(requestedCulture) == false 
                && 
                supportedLanguages.Exists(fadeLanguages => fadeLanguages.Name.Equals(requestedCulture))
                )
            {
                baseCultureInfo = new CultureInfo(requestedCulture);
            }

            //aplicando o idioma
            CultureInfo.CurrentCulture = baseCultureInfo;
            CultureInfo.CurrentUICulture = baseCultureInfo;

            //permitindo o fluxo continuar
            await _next(context);
        }
    }
}

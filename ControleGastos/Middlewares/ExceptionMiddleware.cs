using ControleGastos.Response;

namespace ControleGastos.Middlewares
{
    /// <summary>
    /// Middleware responsável por capturar exceções não tratadas durante o processamento das requisições HTTP e 
    /// retornar uma resposta JSON estruturada com informações sobre o erro ocorrido, utilizando a classe ApiResponse para formatar a resposta de erro.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Método responsável por invocar o próximo middleware na pipeline de processamento de requisições HTTP e 
        /// capturar quaisquer exceções não tratadas que possam ocorrer durante esse processo.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = ApiResponse<string>.Fail(ex.Message);
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}

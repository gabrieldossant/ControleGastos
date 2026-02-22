namespace ControleGastos.Response
{
    public class ApiResponse<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public T Dados { get; set; }
        public static ApiResponse<T> Success(T dados, string Mensagem = null)
        {
            return new ApiResponse<T>()
            {
                Sucesso = true,
                Mensagem = Mensagem,
                Dados = dados,
            };
        }

        public static ApiResponse<T> Fail(string Mensagem)
        {
            return new ApiResponse<T>
            {
                Sucesso = false,
                Mensagem = Mensagem
            };
        }
    }
}

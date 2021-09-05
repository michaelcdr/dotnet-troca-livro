using System;

namespace TrocaLivro.Api.Filtros
{
    public class ErrorResponse
    {
        public int Codigo { get; set; }
        public string Mensagem { get; set; }
        public ErrorResponse InnerError { get; set; }

        public static ErrorResponse From(Exception e)
        {
            if (e == null) return null;

            return new ErrorResponse
            {
                Codigo = e.HResult,
                Mensagem = e.Message,
                InnerError = ErrorResponse.From(e.InnerException)
            };
        }
    }
}

namespace TrocaLivro.Aplicacao.Helpers
{
    public class AppHelper
    {
        public static string ObterDiretorioAvatar(string avatar) 
        {
            if (string.IsNullOrEmpty(avatar)) return string.Empty;

            return $"https://localhost:5001/api/v1/usuario/avatar/{avatar}";
        }
    }
}

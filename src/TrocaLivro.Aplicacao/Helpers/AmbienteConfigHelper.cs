namespace TrocaLivro.Aplicacao.Helpers
{
    public class AmbienteConfigHelper  
    {
        public string Host { get; set; }
        public static string ObterDiretorioAvatar(string avatar)
        { 
            if (string.IsNullOrEmpty(avatar)) return string.Empty;

            return $"{AppServicesHelper.Config.Host}usuario/avatar/{avatar}";
        }
        
        public static string ObterApiPath() => $"{AppServicesHelper.Config.Host}";
    }
}
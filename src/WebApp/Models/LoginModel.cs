namespace WebApp.Models
{
    public class LoginModel
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }

    public class RegistrarModel
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string ConfirmarSenha { get; set; }
    }

    public class ContaModel
    {
        public RegistrarModel Registrar { get; set; }
        public LoginModel Login { get; set; }
    }
}

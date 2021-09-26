namespace TrocaLivro.Infra.Services
{ 
    public class TokenResultado 
    {
        public TokenResultado(string token,string role)
        {
            Token = token;
            Role = role;
        }

        public string Token { get; private  set; }
        public string Role { get; private set; }
    }    
}

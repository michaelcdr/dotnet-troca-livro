namespace TrocaLivro.Infra.Services
{ 
    public class TokenResultado 
    {
        public TokenResultado(string token)
        {
            Token = token;
        }

        public string Token { get; private  set; }
    }    
}

namespace TrocaLivro.Infra.Services
{ 
    public class TokenResultado 
    {
        public TokenResultado(string token,string role, string usuarioId)
        {
            Token = token;
            Role = role;
            UsuarioId = usuarioId;  
        } 
        public string Token { get; private  set; }
        public string Role { get; private set; }
        public string UsuarioId {  get; private set; }
    }    
}

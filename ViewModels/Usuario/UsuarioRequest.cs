namespace ViewModel.Usuario
{
    public class UsuarioRequest : PersonaRequest
    {
        [Validator(true)]
        public string Password { get; set; } = string.Empty;
    }
}

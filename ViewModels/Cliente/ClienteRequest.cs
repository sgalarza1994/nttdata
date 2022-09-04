
namespace ViewModel.Cliente
{
    public class ClienteRequest : PersonaRequest
    {
        public string Password { get; set; } = string.Empty;
        public int ClienteId { get; set; }
    }
}

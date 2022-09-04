

namespace ViewModel.Cliente
{
    public class ClienteResponse : PersonaRequest
    {
        public int ClienteId { get; set; }
        public int Edad { get; set; }
        public string Estado { get; set; } =string.Empty;
    }
}

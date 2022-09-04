

namespace ViewModel
{
    public class JwtToken
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;  
        public string Audience { get; set; } = string.Empty;    
    }
}

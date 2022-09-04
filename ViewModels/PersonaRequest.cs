namespace ViewModel
{
    public class PersonaRequest
    {
        [Validator(true)]
        public string Nombre { get; set; } = string.Empty;
        [Validator(true)]
        public string Genero { get; set; } =string.Empty;
        [Validator(true)]
        public string Identificacion { get; set; } = string.Empty;
        [Validator(true)]
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        [Validator(true)]
        public string UserName { get; set; } = string.Empty ;
        [Validator(true)]
        public string Email { get; set; } =string.Empty ;

        public string FechaNacimiento { get; set; } = string.Empty;
    }
}

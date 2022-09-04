namespace ViewModel.Movimiento
{
    public class MovimentoFiltroFechaRequest
    {   public string Desde { get; set; } = string.Empty; 
        public string Hasta { get; set; } = string.Empty;   

        public string Identificacion { get; set; } = string.Empty;
    }
}

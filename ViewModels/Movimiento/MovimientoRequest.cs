namespace ViewModel.Movimiento
{
    public class MovimientoRequest
    {
        public int CuentaId { get; set; }
        public decimal Valor { get; set; }

        public string Observacion { get; set; } = string.Empty; 
        public string ClaveCajero { get; set; } =string.Empty;
    }
}

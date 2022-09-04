namespace NttDataApi.ViewModels.Cuenta
{
    public class CuentaResponse
    {
        public int CuentaId { get; set; }
        public string NumeroCuenta { get; set; }
        public string Cliente { get; set; }

        public string FechaApertura { get; set; }

        public decimal SaldoApertura { get; set; }
    }
}

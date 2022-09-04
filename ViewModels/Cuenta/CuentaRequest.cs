
namespace ViewModel.Cuenta
{
    public class CuentaRequest
    {
        public int ClienteId { get; set; }
        [Validator(true)]
        public string TipoCuenta { get; set; } =string.Empty;
        public decimal SaldoInicial { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Movimiento
{
    public class MovimientoResponse
    {
        public int MovimientoId { get; set; }
       public string Fecha { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty; 
        public string NumCuenta { get; set; } = string.Empty;   
        public string TipoCuenta { get; set; } = string.Empty;  
        public decimal SaldoInicial { get; set; }   
        public string Movimiento { get; set; } = string.Empty;   
        public decimal SaldoDisponible { get; set; }  
        
        public string Observacion { get; set; } = string.Empty; 
    }
}

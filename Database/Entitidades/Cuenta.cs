using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NttDataApi.Database.Entitidades
{
    [Index(nameof(NumeroCuenta), IsUnique = true)]
    public class Cuenta
    {
        [Key]
        public int CuentaId { get; set; }
        public string NumeroCuenta { get; set; } =string.Empty;
        
        public string TipoCuenta { get; set; } = string.Empty;
    
        public decimal SaldoInicial { get; set; }   

        public string Estado { get; set; } =string.Empty;

        [ForeignKey("ClienteId")]
        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }

        public DateTime Creacion { get; set; }
    }
}

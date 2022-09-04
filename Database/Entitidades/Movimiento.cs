

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NttDataApi.Database.Entitidades
{
    public class Movimiento
    {
        [Key]
        public int MovimientoId { get; set; }
        public DateTime Fecha { get; set; }

        public string TipoMovimiento { get; set; } = "AHORRO";

        public decimal Valor { get; set; }

        public decimal Saldo { get; set; }

        [ForeignKey("CuentaId")]
        public int CuentaId { get; set; }
        public Cuenta Cuenta { get; set; }

        public string Observacion { get; set; } = string.Empty;
    }
}

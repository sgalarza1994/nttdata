
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NttDataApi.Database.Entitidades
{
    public class Cliente : Persona
    {
        [Key]
        public int ClienteId { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string Password { get; set; }   = string.Empty;

        public byte[] PasswordHash { get; set; }

        [Column(TypeName ="varchar(2)")]
        public string Estado { get; set; } = string.Empty;


        public List<Cuenta> Cuentas { get; set; }
    }
}

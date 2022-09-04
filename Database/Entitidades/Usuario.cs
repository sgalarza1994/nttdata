

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NttDataApi.Database.Entitidades
{
    public class Usuario : Persona
    {
        [Key]
        public int UsuarioId { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string Password { get; set; } = string.Empty;

        
        public byte[] PasswordHash { get; set; }
    }
}

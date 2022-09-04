
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NttDataApi.Database.Entitidades
{
    public class Persona
    {
        [Required]
        [Column(TypeName ="varchar(200)")]
        public string Nombre { get; set; }  = string.Empty;
        [Required]
        [Column(TypeName = "varchar(1)")]

        public string Genero { get; set; } = string.Empty ;
        public int Edad { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Identificacion { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Direccion { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string UserName { get; set; } = string.Empty;

        [Column(TypeName = "varchar(30)")]
        public string FechaNacimiento { get; set; } = string.Empty;

    }
}

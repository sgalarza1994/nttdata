
using Microsoft.EntityFrameworkCore;
using NttDataApi.Database.Entitidades;

namespace NttDataApi.Database
{
    public class CooperativaDatabase :DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }


        public CooperativaDatabase(DbContextOptions<CooperativaDatabase> options)
            : base(options)
        {
            
        }
    }
}

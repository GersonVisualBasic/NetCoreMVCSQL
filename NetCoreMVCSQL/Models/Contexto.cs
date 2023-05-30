using Microsoft.EntityFrameworkCore;

namespace NetCoreMVCSQL.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        public DbSet<Logistica> Logistica { get; set; }

    }
}

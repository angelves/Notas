using Microsoft.EntityFrameworkCore;
namespace Notas.Database
{
    public class NotasContext : DbContext
    {
        public NotasContext(DbContextOptions<NotasContext> options) : base(options)
        { }


        public DbSet<Models.POCOs.Notas> Notas { get; set; }
    }
}

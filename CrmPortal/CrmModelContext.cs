using Microsoft.EntityFrameworkCore;

namespace CrmPortal.Models
{
    public class CrmPortalContext
        : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public CrmPortalContext(DbContextOptions<CrmPortalContext> options)
            : base(options)
        {
        }
    }
}
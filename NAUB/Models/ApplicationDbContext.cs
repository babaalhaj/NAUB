using System.Data.Entity;
using System.Security.AccessControl;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NAUB.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<BorrowType> BorrowTypes { get; set; }
        


    }
}
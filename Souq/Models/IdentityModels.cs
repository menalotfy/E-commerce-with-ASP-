using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Souq.Models.MyClasses;

namespace Souq.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public bool Status { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(@"Data Source=DESKTOP-2KNRDIS\SQL2016;Initial Catalog=Souq;Integrated Security=True", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual DbSet<Payment> Payments { set; get; }
        public virtual DbSet<Product> Product { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<OrderDetails> OrderDetails { set; get; }
        public virtual DbSet<Category> Categories { set; get; }

      //  public System.Data.Entity.DbSet<Souq.Models.ApplicationUser> ApplicationUsers { get; set; }

        //  public System.Data.Entity.DbSet<Souq.Models.ApplicationUser> ApplicationUsers { get; set; }

        //  public System.Data.Entity.DbSet<Souq.Models.ApplicationUser> ApplicationUsers { get; set; }

        // public System.Data.Entity.DbSet<Souq.Models.ApplicationUser> ApplicationUsers { get; set; }

        //   public System.Data.Entity.DbSet<Souq.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}
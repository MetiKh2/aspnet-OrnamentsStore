

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ornaments.DataAccess.Entities.Order;
using Ornaments.DataAccess.Entities.Ornaments;
using Ornaments.DataAccess.Entities.User;

namespace Ornaments.DataAccess.Context
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
         
        public DbSet<Entities.Ornaments.Ornament> Ornaments{ get; set; }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Comment> Comments{ get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<OrderDetail> OrderDetails{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Ornament>().HasQueryFilter(p => !p.IsRemoved);
            builder.Entity<Category>().HasQueryFilter(p => !p.IsRemoved);
            builder.Entity<Comment>().HasQueryFilter(p => !p.IsRemoved);
            builder.Entity<Order>().HasQueryFilter(p => !p.IsRemoved);
            builder.Entity<OrderDetail>().HasQueryFilter(p => !p.IsRemoved);

            base.OnModelCreating(builder);
        }
    }
}

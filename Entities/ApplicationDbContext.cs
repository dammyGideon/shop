using Microsoft.EntityFrameworkCore;
using orderService.domain.Entities;
using orderService.domain.Seeder;

namespace orderService.domain
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderBook>()
            .HasKey(ob => new { ob.OrderId, ob.BookId });

            modelBuilder.Entity<OrderBook>().HasOne(ob => ob.Order)
             .WithMany(o => o.OrderBooks)
                .HasForeignKey(ob => ob.OrderId);

            modelBuilder.Entity<OrderBook>()
                .HasOne(ob => ob.Book)
                .WithMany()
                .HasForeignKey(ob => ob.BookId);
            modelBuilder.ApplyConfiguration(new BookTableSeeder()); 
           
        }



        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderBook> OrderBooks { get; set; }
    }
}
namespace ProductDuplicatePoC.Data.Sql
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using ProductDuplicatePoC.Models.Sql;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<DuplicateGroup> Groups { get; set; }
        
        public DbSet<DuplicateGroupItem> GroupItems { get; set; }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<DigitalAsset> DigitalAssets { get; set; }

        public DbSet<Merchant> Merchants { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()  // This will include parameter values in logs
                .LogTo(Console.WriteLine, LogLevel.Information);  // Logs to the console
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DuplicateGroup>().ToTable("DuplicateGroups");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Merchant>().ToTable("Merchant");
            modelBuilder.Entity<DigitalAsset>().ToTable("DigitalAssets");
            modelBuilder.Entity<DuplicateGroupItem>().ToTable("DuplicateGroupItems");


            modelBuilder.Entity<DuplicateGroup>()
                .HasMany(g => g.GroupItems)
                .WithOne(gi => gi.Group)
                .HasForeignKey(gi => gi.DuplicateGroupId);

            modelBuilder.Entity<DuplicateGroupItem>()
                .HasOne(gi => gi.Product)
                .WithMany()
                .HasForeignKey(gi => gi.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne<Merchant>()
                .WithMany()
                .HasForeignKey(p => p.MerchantCode)
                .HasPrincipalKey(m => m.Code)
                .IsRequired(false);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.DigitalAssets)
                .WithOne(x => x.Product)
                .HasForeignKey(d => d.ProductId)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}

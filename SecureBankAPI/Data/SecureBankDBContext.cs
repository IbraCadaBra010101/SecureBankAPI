namespace SecureBankAPI.Data
{
    using Microsoft.EntityFrameworkCore;
    using SecureBankAPI.Models;

    /// <summary>
    /// Represents the session with the database, providing access to the data and handling transactions.
    /// </summary>
    public class SecureBankDBContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecureBankDBContext"/> class.
        /// </summary>
        /// <param name="options">The options used to configure the context.</param>
        public SecureBankDBContext(DbContextOptions<SecureBankDBContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Client}"/> representing the clients in the database.
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Investment}"/> representing the investments in the database.
        /// </summary>
        public DbSet<Investment> Investments { get; set; }

        /// <summary>
        /// Configures the entity mappings and relationships for the context.
        /// </summary>
        /// <param name="modelBuilder">The <see cref="ModelBuilder"/> used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(c => c.ClientId);

                entity?.HasMany(c => c.Investments)
                      .WithOne(i => i.Client)
                      .HasForeignKey(i => i.ClientId);
            });

            modelBuilder.Entity<Investment>(entity =>
            {
                entity.Property(e => e.Amount)
                  .HasColumnType("decimal(18,2)");

                entity.Property(e => e.CurrentValue)
                      .HasColumnType("decimal(18,2)");

                entity.HasKey(i => i.InvestmentId);

                entity.HasOne(i => i.Client)
                      .WithMany(c => c.Investments)
                      .HasForeignKey(i => i.ClientId);
                      });
        }
    }
}

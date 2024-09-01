namespace SecureBankAPI.Data
{
    using System;
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
                entity.HasMany(c => c.Investments)
                      .WithOne(i => i.Client)
                      .HasForeignKey(i => i.ClientId);

                var clients = new Client[10];
                for (int i = 0; i < 10; i++)
                {
                    clients[i] = new Client
                    {
                        ClientId = Guid.NewGuid(),
                        FirstName = $"ClientFirstName{i + 1}",
                        LastName = $"ClientLastName{i + 1}",
                        Email = $"client{i + 1}@example.com",
                        PhoneNumber = $"555-010{i + 1}",
                        DateOfBirth = new DateTime(1980 + i, 1, 1),
                        DateRegistered = DateTime.Now,
                    };
                }

                modelBuilder.Entity<Client>().HasData(clients);

                var investments = new Investment[30];
                var random = new Random();
                int investmentIndex = 0;
                foreach (var client in clients)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        investments[investmentIndex++] = new Investment
                        {
                            InvestmentId = Guid.NewGuid(),
                            ClientId = client.ClientId,
                            InvestmentCategory = random.Next(1, 5),
                            Amount = random.Next(1000, 50000),
                            DateInvested = DateTime.Now.AddMonths(-random.Next(1, 24)),
                            CurrentValue = random.Next(1000, 50000),
                            RiskLevel = random.Next(1, 4),
                            InvestmentStatus = 0,
                        };
                    }
                }

                modelBuilder.Entity<Investment>().HasData(investments);
            });
        }
    }
}
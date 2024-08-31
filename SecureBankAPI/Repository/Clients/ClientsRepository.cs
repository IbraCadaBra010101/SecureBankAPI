// <copyright file="ClientsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SecureBankAPI.Repository.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SecureBankAPI.Data;
    using SecureBankAPI.Models;

    /// <summary>
    /// Provides the implementation of operations for interacting with the client data using EF Core.
    /// </summary>
    public class ClientsRepository : IClientsRepository
    {
        private readonly SecureBankDBContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientsRepository"/> class.
        /// </summary>
        /// <param name="context">The EF Core database context.</param>
        public ClientsRepository(SecureBankDBContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task AddClientAsync(Client client)
        {
            ArgumentNullException.ThrowIfNull(client);

            await this.context.Clients.AddAsync(client);
            await this.context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<Client?> GetClientByIdAsync(Guid clientId)
        {
            return await this.context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await this.context.Clients
                .AsNoTracking()
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateClientAsync(Client client)
        {
            ArgumentNullException.ThrowIfNull(nameof(client));

            this.context.Clients.Update(client);
            await this.context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteClientAsync(Guid clientId)
        {
            var client = await this.context.Clients.FindAsync(clientId) ?? throw new KeyNotFoundException($"Client with ID {clientId} not found.");
            this.context.Clients.Remove(client);
            await this.context.SaveChangesAsync();
        }
    }
}

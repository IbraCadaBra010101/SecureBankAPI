// <copyright file="IClientsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SecureBankAPI.Repository.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SecureBankAPI.Models;

    /// <summary>
    /// Defines the operations for interacting with the client data.
    /// </summary>
    public interface IClientsRepository
    {
        /// <summary>
        /// Adds a new client to the repository.
        /// </summary>
        /// <param name="client">The client entity to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddClientAsync(Client client);

        /// <summary>
        /// Gets a client by its unique identifier.
        /// </summary>
        /// <param name="clientId">The unique identifier of the client.</param>
        /// <returns>The client entity, or null if not found.</returns>
        Task<Client?> GetClientByIdAsync(Guid clientId);

        /// <summary>
        /// Gets all clients from the repository.
        /// </summary>
        /// <returns>A list of client entities.</returns>
        Task<IEnumerable<Client>> GetAllClientsAsync();

        /// <summary>
        /// Updates an existing client in the repository.
        /// </summary>
        /// <param name="client">The client entity with updated values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateClientAsync(Client client);

        /// <summary>
        /// Deletes a client from the repository by its unique identifier.
        /// </summary>
        /// <param name="clientId">The unique identifier of the client to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteClientAsync(Guid clientId);

        /// <summary>
        /// Gets a paginated list of clients.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <returns>A paginated list of clients.</returns>
        Task<IEnumerable<Client>> GetClientsPaginatedAsync(int pageNumber, int pageSize);
    }
}
// <copyright file="IInvestmentsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SecureBankAPI.Repository.Investments
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SecureBankAPI.Models;

    /// <summary>
    /// Defines the operations for interacting with investment data.
    /// </summary>
    public interface IInvestmentsRepository
    {
        /// <summary>
        /// Adds a new investment to the repository.
        /// </summary>
        /// <param name="investment">The investment entity to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddInvestmentAsync(Investment investment);

        /// <summary>
        /// Gets an investment by its unique identifier.
        /// </summary>
        /// <param name="investmentId">The unique identifier of the investment.</param>
        /// <returns>The investment entity, or null if not found.</returns>
        Task<Investment?> GetInvestmentByIdAsync(Guid investmentId);

        /// <summary>
        /// Gets all investments for a specific client.
        /// </summary>
        /// <param name="clientId">The unique identifier of the client.</param>
        /// <returns>A list of investment entities for the specified client.</returns>
        Task<IEnumerable<Investment>> GetInvestmentsByClientIdAsync(Guid clientId);

        /// <summary>
        /// Updates an existing investment in the repository.
        /// </summary>
        /// <param name="investment">The investment entity with updated values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateInvestmentAsync(Investment investment);

        /// <summary>
        /// Deletes an investment from the repository by its unique identifier.
        /// </summary>
        /// <param name="investmentId">The unique identifier of the investment to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteInvestmentAsync(Guid investmentId);

        /// <summary>
        /// Gets all investments from the repository.
        /// </summary>
        /// <returns>A list of all investment entities.</returns>
        Task<IEnumerable<Investment>> GetAllInvestmentsAsync();
    }
}
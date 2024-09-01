// <copyright file="InvestmentsRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SecureBankAPI.Repository.Investments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SecureBankAPI.Data;
    using SecureBankAPI.Models;

    /// <summary>
    /// Provides the implementation of operations for interacting with investment data using EF Core.
    /// </summary>
    public class InvestmentsRepository : IInvestmentsRepository
    {
        private readonly SecureBankDBContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvestmentsRepository"/> class.
        /// </summary>
        /// <param name="context">The EF Core database context.</param>
        public InvestmentsRepository(SecureBankDBContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task AddInvestmentAsync(Investment investment)
        {
            ArgumentNullException.ThrowIfNull(investment);

            await this.context.Investments.AddAsync(investment);
            await this.context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<Investment?> GetInvestmentByIdAsync(Guid investmentId)
        {
            return await this.context.Investments
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.InvestmentId == investmentId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Investment>> GetInvestmentsByClientIdAsync(Guid clientId)
        {
            return await this.context.Investments
                .AsNoTracking()
                .Where(i => i.ClientId == clientId)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateInvestmentAsync(Investment investment)
        {
            ArgumentNullException.ThrowIfNull(investment);

            this.context.Investments.Update(investment);
            await this.context.SaveChangesAsync();
        }
    }
}
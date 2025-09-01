// <copyright file="ICompaniesRepository.cs" company="Ibrahim Mahdi">
// Copyright (c) Ibrahim Mahdi. All rights reserved.
// </copyright>

namespace FastighetsAPI.Repository.Companies
{
    using FastighetsAPI.Models.DataModels;

    public interface ICompaniesRepository
    {

        Task<IEnumerable<Company>> GetAllAsync();

        Task<Company?> GetByIdAsync(Guid id);

        Task<int> SaveChangesAsync();
    }
}
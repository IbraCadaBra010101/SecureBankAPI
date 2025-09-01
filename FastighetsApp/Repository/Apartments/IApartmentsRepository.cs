// <copyright file="IApartmentsRepository.cs" company="Ibrahim Mahdi">
// Copyright (c) Ibrahim Mahdi. All rights reserved.
// </copyright>

namespace FastighetsAPI.Repository.Apartments
{
    using FastighetsAPI.Models.DataModels;

    public interface IApartmentsRepository
    {

        Task<IEnumerable<Apartment>> GetAllApartmentsAsync();

        Task<Apartment?> GetApartmentByIdAsync(Guid id);

        Task<Apartment?> GetApartmentByIdForUpdateAsync(Guid id);

        Task<IEnumerable<Apartment>> GetByCompanyIdAsync(Guid companyId);

        Task<int> UpdateApartmentAsync(Apartment apartment);

        Task<int> SaveChangesAsync();
    }
}
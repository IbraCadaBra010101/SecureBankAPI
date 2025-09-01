// <copyright file="IRealEstateService.cs" company="Ibrahim Mahdi">
// Copyright (c) Ibrahim Mahdi. All rights reserved.
// </copyright>

namespace FastighetsAPI.Services.ApartmentService
{
    using FastighetsAPI.Models.DataModels;

    public interface IApartmentService
    {

        Task<IEnumerable<Company>> GetCompaniesAsync();

        Task<Apartment?> GetApartmentByIdAsync(Guid apartmentId);

        Task<IEnumerable<Apartment>> GetApartmentsByCompanyAsync(Guid companyId);

        Task<IEnumerable<Apartment>> GetApartmentsWithExpiringContractsAsync(Guid companyId, TimeSpan timeSpan);
    }
}
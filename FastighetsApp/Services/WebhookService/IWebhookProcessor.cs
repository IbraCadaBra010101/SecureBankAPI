// <copyright file="IWebhookProcessor.cs" company="Ibrahim Mahdi">
// Copyright (c) Ibrahim Mahdi. All rights reserved.
// </copyright>

namespace FastighetsAPI.Services.WebhookService
{
    using FastighetsAPI.Models.DataModels;
    using FastighetsAPI.Models.DTO;
    using FastighetsAPI.Models.WebhookModels;
    using Microsoft.AspNetCore.Http;

    public interface IWebhookProcessor
    {

        Task<WebhookUpdateResult> UpdateApartmentAttributesAsync(ApartmentAttributeUpdateDto updateDto);
    }
}

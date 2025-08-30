// <copyright file="ApartmentAttributeUpdateDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FastighetsAPI.Models.DTO 
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Webhook payload for updating apartment attributes.
    /// </summary>
    public class ApartmentAttributeUpdateDto
    {
        /// <summary>
        /// Gets or sets the target apartment identifier.
        /// </summary>
        [Required]
        public Guid ApartmentId { get; set; }

        /// <summary>
        /// Gets or sets the source identifier from the webhook payload.
        /// </summary>
        public string? SourceId { get; set; }

        /// <summary>
        /// Gets or sets the occupancy flag to update.
        /// </summary>
        public bool? IsOccupied { get; set; }

        /// <summary>
        /// Gets or sets the rent per month to update.
        /// </summary>
        public decimal? RentPerMonth { get; set; }
    }
}
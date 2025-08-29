// <copyright file="ApartmentAttributeUpdateDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Models;

/// <summary>
/// Webhook payload for updating apartment attributes.
/// </summary>
public class ApartmentAttributeUpdateDto
{
    /// <summary>
    /// Gets or sets the target apartment identifier.
    /// </summary>
    public Guid ApartmentId { get; set; }

    /// <summary>
    /// Gets or sets the occupancy flag to update.
    /// </summary>
    public bool? IsOccupied { get; set; }

    /// <summary>
    /// Gets or sets the rent per month to update.
    /// </summary>
    public decimal? RentPerMonth { get; set; }

    /// <summary>
    /// Gets or sets the webhook source identifier for tracking.
    /// </summary>
    public string? SourceId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the update was processed by the source system.
    /// </summary>
    public DateTime? ProcessedAt { get; set; }
}

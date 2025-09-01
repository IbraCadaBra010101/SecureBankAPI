// <copyright file="ApartmentAttributeUpdateDto.cs" company="Ibrahim Mahdi">
// Copyright (c) Ibrahim Mahdi. All rights reserved.
// </copyright>

namespace FastighetsAPI.Models.DTO 
{
    using System.ComponentModel.DataAnnotations;

    public class ApartmentAttributeUpdateDto
    {
    
        [Required]
        public Guid ApartmentId { get; set; }

        public bool? IsOccupied { get; set; }

        public decimal? RentPerMonth { get; set; }
    }
}
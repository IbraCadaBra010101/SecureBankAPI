// <copyright file="ClientViewmodel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SecureBankAPI.Services.Clients.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents the view model for adding a new client along with multiple investments.
    /// </summary>
    public class ClientViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the client.
        /// </summary>
        /// <remarks>
        /// This ID is automatically generated when the object is instantiated, but it can be overridden.
        /// </remarks>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the client.
        /// </summary>
        /// <remarks>
        /// The first name is required and must be at most 100 characters long.
        /// </remarks>
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the client.
        /// </summary>
        /// <remarks>
        /// The last name is required and must be at most 100 characters long.
        /// </remarks>
        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the client.
        /// </summary>
        /// <remarks>
        /// The email address is required and must be a valid email format.
        /// </remarks>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number of the client.
        /// </summary>
        /// <remarks>
        /// The phone number is required and must be in a valid phone number format.
        /// </remarks>
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date of birth of the client.
        /// </summary>
        /// <remarks>
        /// The date of birth is required and must be a valid date.
        /// </remarks>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the date when the client was registered in the system.
        /// </summary>
        /// <remarks>
        /// The date registered is required and defaults to the current date and time.
        /// </remarks>
        [Required]
        public DateTime DateRegistered { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the list of investments associated with the client.
        /// </summary>
        /// <remarks>
        /// The investments list is required and must contain at least one investment.
        /// </remarks>
        [Required]
        public List<InvestmentViewModel> Investments { get; set; } = new List<InvestmentViewModel>();
    }
}
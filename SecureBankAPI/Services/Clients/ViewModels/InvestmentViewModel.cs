// <copyright file="InvestmentViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#nullable disable

namespace SecureBankAPI.Services.Clients.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents the view model for an investment associated with a client.
    /// </summary>
    public class InvestmentViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the investment.
        /// </summary>
        public Guid InvestmentId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the type of investment.
        /// </summary>
        [Required]
        [Range(0, 4, ErrorMessage = "InvestmentCategory must be between 0 and 4.")]
        public int InvestmentCategory { get; set; }

        /// <summary>
        /// Gets or sets the amount invested.
        /// </summary>
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive number.")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date when the investment was made.
        /// </summary>
        public DateTime DateInvested { get; set; }

        /// <summary>
        /// Gets or sets the current value of the investment.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "CurrentValue must be a positive number.")]
        public decimal CurrentValue { get; set; }

        /// <summary>
        /// Gets or sets the risk level of the investment.
        /// </summary>
        [Range(0, 4, ErrorMessage = "RiskLevel must be between 0 and 4.")]
        public int RiskLevel { get; set; }

        /// <summary>
        /// Gets or sets the status of the investment.
        /// </summary>
        [Range(0, 1, ErrorMessage = "Status must be between 0 and 1 where 0 means inactive and 1 - means active.")]
        public int Status { get; set; }
    }
}

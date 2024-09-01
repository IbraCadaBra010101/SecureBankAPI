// <copyright file="Investment.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#nullable disable
namespace SecureBankAPI.Models
{
    /// <summary>
    /// Represents an investment associated with a client's SIPP (Self-Invested Personal Pension).
    /// </summary>
    public class Investment
    {
        /// <summary>
        /// Gets or sets the unique identifier for the investment.
        /// </summary>
        /// <remarks>
        /// This GUID is the primary key for the investment entity in the database.
        /// </remarks>
        public Guid InvestmentId { get; set; }

        /// <summary>
        /// Gets or sets the type of investment.
        /// </summary>
        /// <remarks>
        /// The type could be one of several categories, such as equities, bonds, funds, real estate, or cash deposits.
        /// </remarks>
        public int InvestmentCategory { get; set; }

        /// <summary>
        /// Gets or sets the amount invested.
        /// </summary>
        /// <remarks>
        /// The amount represents the value of the original investment in GBP.
        /// </remarks>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date when the investment was made.
        /// </summary>
        public DateTime DateInvested { get; set; }

        /// <summary>
        /// Gets or sets the current value of the investment.
        /// </summary>
        /// <remarks>
        /// The current value is updated based on market conditions or periodic valuations and represents the current wealth of the client.
        /// </remarks>
        public decimal CurrentValue { get; set; }

        /// <summary>
        /// Gets or sets the risk level of the investment.
        /// </summary>
        /// <remarks>
        /// The risk level indicates how risky the investment is, with common classifications being Low, Medium, or High.
        /// </remarks>
        public int RiskLevel { get; set; }

        /// <summary>
        /// Gets or sets the status of the investment.
        /// </summary>
        /// <remarks>
        /// The status could be Active, Sold, or Closed, depending on the lifecycle of the investment.
        /// </remarks>
        public int InvestmentStatus { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the client who owns the investment.
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client associated with this investment.
        /// </summary>
        /// <remarks>
        /// This is a navigation property that establishes the relationship between an investment and its owning client.
        /// </remarks>
        public Client Client { get; set; }
    }
}

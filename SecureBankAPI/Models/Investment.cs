// <copyright file="Investment.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SecureBankAPI.Models
{
    /// <summary>
    /// Represents the type of investment in a SIPP (Self-Invested Personal Pension).
    /// </summary>
    public enum InvestmentType
    {
        /// <summary>
        /// Investments in stocks and shares.
        /// </summary>
        Equities,

        /// <summary>
        /// Investments in debt securities issued by governments or corporations.
        /// </summary>
        Bonds,

        /// <summary>
        /// Investments in mutual funds or exchange-traded funds (ETFs).
        /// </summary>
        Funds,

        /// <summary>
        /// Investments in real estate properties.
        /// </summary>
        RealEstate,

        /// <summary>
        /// Investments in cash deposits or savings accounts.
        /// </summary>
        CashDeposits,

        /// <summary>
        /// Investments that do not fit into the other specified categories.
        /// </summary>
        Other,
    }

    /// <summary>
    /// Represents the risk level associated with an investment.
    /// </summary>
    public enum RiskLevel
    {
        /// <summary>
        /// Investments with low risk and stable returns.
        /// </summary>
        Low,

        /// <summary>
        /// Investments with moderate risk and potentially higher returns.
        /// </summary>
        Medium,

        /// <summary>
        /// Investments with high risk and potential for significant returns or losses.
        /// </summary>
        High,
    }

    /// <summary>
    /// Represents the status of an investment.
    /// </summary>
    public enum InvestmentStatus
    {
        /// <summary>
        /// The investment is currently active and operational.
        /// </summary>
        Active,

        /// <summary>
        /// The investment has been sold.
        /// </summary>
        Sold,

        /// <summary>
        /// The investment has been closed or terminated.
        /// </summary>
        Closed,
    }

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
        public InvestmentType Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the specific investment.
        /// </summary>
        /// <remarks>
        /// This could be the name of a stock, fund, bond, or any other specific investment product.
        /// </remarks>
        public string? InvestmentName { get; set; }

        /// <summary>
        /// Gets or sets the amount invested.
        /// </summary>
        /// <remarks>
        /// The amount represents the value of the investment in GBP.
        /// </remarks>
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the date when the investment was made.
        /// </summary>
        public DateTime DateInvested { get; set; }

        /// <summary>
        /// Gets or sets the current value of the investment.
        /// </summary>
        /// <remarks>
        /// The current value is updated based on market conditions or periodic valuations.
        /// </remarks>
        public decimal? CurrentValue { get; set; }

        /// <summary>
        /// Gets or sets the risk level of the investment.
        /// </summary>
        /// <remarks>
        /// The risk level indicates how risky the investment is, with common classifications being Low, Medium, or High.
        /// </remarks>
        public RiskLevel RiskLevel { get; set; }

        /// <summary>
        /// Gets or sets the status of the investment.
        /// </summary>
        /// <remarks>
        /// The status could be Active, Sold, or Closed, depending on the lifecycle of the investment.
        /// </remarks>
        public InvestmentStatus? Status { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the client who owns the investment.
        /// </summary>
        public int? ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client associated with this investment.
        /// </summary>
        /// <remarks>
        /// This is a navigation property that establishes the relationship between an investment and its owning client.
        /// </remarks>
        public Client? Client { get; set; }
    }
}

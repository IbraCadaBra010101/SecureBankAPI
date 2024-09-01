// <copyright file="MessageConstants.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SecureBankAPI.Models
{
    using SecureBankAPI.Services.Clients.ViewModels;

    /// <summary>
    /// Contains constants for various message strings used throughout the application.
    /// </summary>
    public static class MessageConstants
    {
        /// <summary>
        /// Message indicating that the <see cref="ClientViewModel"/> cannot be null.
        /// </summary>
        public const string ClientViewModelNullError = "ClientViewModel cannot be null.";

        /// <summary>
        /// Message indicating that the client and investments were added successfully.
        /// </summary>
        public const string ClientAddedSuccess = "Client and investments added successfully.";

        /// <summary>
        /// Message indicating that an error occurred while processing the request.
        /// </summary>
        public const string InternalServerError = "An error occurred while processing your request.";

        /// <summary>
        /// Message indicating that no clients or investments were found.
        /// </summary>
        public const string NoClientsInvestmentsFound = "No Client Investments were found.";

        /// <summary>
        /// Message indicating that the transfer amount must be greater than zero.
        /// </summary>
        public const string TransferAmountInvalid = "The transfer amount must be greater than zero.";

        /// <summary>
        /// Message indicating a successful fund transfer.
        /// </summary>
        public const string TransferFundsSuccess = "Funds transferred successfully.";

        /// <summary>
        /// Message indicating an invalid argument error during funds transfer.
        /// </summary>
        public const string TransferFundsInvalidArgumentError = "Invalid argument error during funds transfer.";

        /// <summary>
        /// Message indicating that an investment was not found during funds transfer.
        /// </summary>
        public const string TransferFundsNotFoundError = "Investment not found error during funds transfer.";

        /// <summary>
        /// Message indicating an operation error during funds transfer.
        /// </summary>
        public const string TransferFundsOperationError = "Operation error during funds transfer.";

        /// <summary>
        /// Message indicating an unexpected error occurred during funds transfer.
        /// </summary>
        public const string TransferFundsUnexpectedError = "An unexpected error occurred during funds transfer.";
    }
}
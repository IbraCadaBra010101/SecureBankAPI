// <copyright file="MessageConstants.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SecureBankAPI.Models
{
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
        /// Message indicating that an error occurred while processing the request.
        /// </summary>
        public const string NoClientsInvestmentsFound = "No Client Investments where found.";
    }
}
 
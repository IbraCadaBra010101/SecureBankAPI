// <copyright file="Routes.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SecureBankAPI.Models
{
    /// <summary>
    /// Represents the controller routes.
    /// </summary>
    public static class Routes
    {
        /// <summary>
        /// Gets or sets the route to read all clients.
        /// </summary>
        /// <remarks>
        /// The route will return a list of all the clients that are secure bank customers.
        /// </remarks>
        internal const string Clients = "Clients";

        /// <summary>
        /// Gets or sets the route to add a new client.
        /// </summary>
        /// <remarks>
        /// The route is used to add a new client to the secure bank system.
        /// </remarks>
        internal const string AddClient = "add-client";

        /// <summary>
        /// Gets or sets the route to add a new investment for a client.
        /// </summary>
        /// <remarks>
        /// The route is used to add a new investment for a specific client.
        /// </remarks>
        internal const string AddInvestment = "add-investment";

        /// <summary>
        /// Gets or sets the route to retrieve all investments for a specific client.
        /// </summary>
        /// <remarks>
        /// The route will return a list of all investments for a specified client.
        /// </remarks>
        internal const string ClientInvestments = "client/{clientId}/investments";

        /// <summary>
        /// Gets or sets the route to update an investment for a client.
        /// </summary>
        /// <remarks>
        /// The route is used to update an existing investment for a specific client.
        /// </remarks>
        internal const string UpdateInvestment = "update-investment/{investmentId}";

        /// <summary>
        /// Gets or sets the route to update an existing client.
        /// </summary>
        /// <remarks>
        /// The route is used to update an existing client in the secure bank system.
        /// </remarks>
        internal const string UpdateClient = "update-client/{clientId}";
    }
}

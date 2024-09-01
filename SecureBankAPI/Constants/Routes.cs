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
        internal const string ClientsInvestments = "ClientsInvestments";

        /// <summary>
        /// Gets or sets the route to read all clients and their investments or a specific client by ID.
        /// </summary>
        /// <remarks>
        /// The route will return all clients with their investments if no ID is provided, or a specific client with their investments if an ID is provided.
        /// </remarks>
        internal const string ClientInvestment = "investments/{id?}";

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

        /// <summary>
        /// Gets or sets the route to read all clients with pagination.
        /// </summary>
        /// <remarks>
        /// The route returns a paginated list of clients with their investments.
        /// The `pageNumber` and `pageSize` parameters should be provided as query parameters in the URL.
        /// Example: `ClientsInvestments/Paginated?pageNumber=1&pageSize=10`.
        /// </remarks>
        internal const string ClientsInvestmentsPaginated = "ClientsInvestments/Paginated";

    }
}

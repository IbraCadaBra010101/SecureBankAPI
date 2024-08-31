namespace SecureBankAPI.Services.Clients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SecureBankAPI.Services.Clients.ViewModels;

    /// <summary>
    /// Defines the contract for client-related operations in the investment bank system.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Adds a new client along with multiple investments to the system.
        /// </summary>
        /// <param name="clientViewModel">The view model containing the details of the client and their investments.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// This method handles the transaction to ensure that both client and investment records are added
        /// atomically. If any part of the operation fails, the transaction will be rolled back.
        /// </remarks>
        Task AddClientWithInvestmentsAsync(ClientViewModel clientViewModel);

        /// <summary>
        /// Retrieves all clients along with their associated investments.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of
        /// <see cref="ClientWithInvestmentsViewModel"/> objects, each representing a client and their investments.
        /// </returns>
        /// <remarks>
        /// This method fetches all clients from the repository and retrieves their investments based on client IDs.
        /// </remarks>
        Task<IEnumerable<ClientWithInvestmentsViewModel>> GetAllClientsWithInvestmentsAsync();
    }
}
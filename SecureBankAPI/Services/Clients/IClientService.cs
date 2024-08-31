namespace SecureBankAPI.Services.Clients
{
    using System.Threading.Tasks;
    using SecureBankAPI.Services.Clients.ViewModels;

    /// <summary>
    /// Defines the contract for client-related operations in the investment bank system.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Adds a new client along with multiple investments.
        /// </summary>
        /// <param name="clientViewModel">The view model containing client details and investments.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddClientWithInvestmentsAsync(ClientViewModel clientViewModel);
    }
}

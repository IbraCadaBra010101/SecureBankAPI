namespace SecureBankAPI.Services.Clients
{
    using System;
    using System.Threading.Tasks;
    using SecureBankAPI.Data;
    using SecureBankAPI.Models;
    using SecureBankAPI.Repository.Clients;
    using SecureBankAPI.Repository.Investments;
    using SecureBankAPI.Services.Clients.ViewModels;

    /// <summary>
    /// Provides operations related to clients and investments.
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IClientsRepository clientsRepository;
        private readonly IInvestmentsRepository investmentsRepository;
        private readonly SecureBankDBContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="clientsRepository">The repository for client data operations.</param>
        /// <param name="investmentsRepository">The repository for investment data operations.</param>
        /// <param name="context">The database context used for transaction management.</param>
        public ClientService(IClientsRepository clientsRepository, IInvestmentsRepository investmentsRepository, SecureBankDBContext context)
        {
            this.clientsRepository = clientsRepository;
            this.investmentsRepository = investmentsRepository;
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task AddClientWithInvestmentsAsync(ClientViewModel clientViewModel)
        {
            using var transaction = await this.context.Database.BeginTransactionAsync();
            try
            {
                var client = new Client
                {
                    ClientId = clientViewModel.ClientId,
                    FirstName = clientViewModel.FirstName,
                    LastName = clientViewModel.LastName,
                    Email = clientViewModel.Email,
                    PhoneNumber = clientViewModel.PhoneNumber,
                    DateOfBirth = clientViewModel.DateOfBirth,
                    DateRegistered = clientViewModel.DateRegistered,
                };

                await this.clientsRepository.AddClientAsync(client);

                foreach (var investmentViewModel in clientViewModel.Investments)
                {
                    var investment = new Investment
                    {
                        InvestmentId = investmentViewModel.InvestmentId,
                        ClientId = client.ClientId,
                        InvestmentCategory = investmentViewModel.InvestmentCategory,
                        Amount = investmentViewModel.Amount,
                        CurrentValue = investmentViewModel.CurrentValue,
                        DateInvested = investmentViewModel.DateInvested,
                        RiskLevel = investmentViewModel.RiskLevel,
                        InvestmentStatus = investmentViewModel.Status,
                    };

                    await this.investmentsRepository.AddInvestmentAsync(investment);
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ClientWithInvestmentsViewModel>> GetAllClientsWithInvestmentsAsync()
        {
            try
            {
                var clients = await this.clientsRepository.GetAllClientsAsync();

                var clientWithInvestmentsList = new List<ClientWithInvestmentsViewModel>();

                foreach (var client in clients)
                {
                    var investments = await this.investmentsRepository.GetInvestmentsByClientIdAsync(client.ClientId);

                    var clientWithInvestments = new ClientWithInvestmentsViewModel
                    {
                        Client = client,
                        Investments = investments.ToList(),
                    };

                    clientWithInvestmentsList.Add(clientWithInvestments);
                }

                return clientWithInvestmentsList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}

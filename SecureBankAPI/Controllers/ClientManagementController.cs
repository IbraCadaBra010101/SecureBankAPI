// <copyright file="ClientsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SecureBankAPI.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using SecureBankAPI.Models;
    using SecureBankAPI.Services.Clients;
    using SecureBankAPI.Services.Clients.ViewModels;

    /// <summary>
    /// Controller for managing clients and their investments.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ClientManagementController : ControllerBase
    {
        private readonly ILogger<ClientManagementController> logger;
        private readonly IClientService clientService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientManagementController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="clientService">The controller for client and investment operations.</param>
        public ClientManagementController(ILogger<ClientManagementController> logger, IClientService clientService)
        {
            this.logger = logger;
            this.clientService = clientService;
        }

        /// <summary>
        /// Adds a new client along with investments.
        /// </summary>
        /// <param name="clientViewModel">The client view model containing client and investment information.</param>
        /// <returns>Returns a status of the operation.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Authentication.ClientsManagePolicy)]
        [HttpPost]
        [Route(Routes.AddClient)]
        public async Task<IActionResult> AddClientAsync([FromBody] ClientViewModel clientViewModel)
        {
            if (clientViewModel == null)
            {
                this.logger.LogError(MessageConstants.ClientViewModelNullError);

                return this.BadRequest(MessageConstants.ClientViewModelNullError);
            }

            try
            {
                await this.clientService.AddClientWithInvestmentsAsync(clientViewModel);
                return this.Ok(MessageConstants.ClientAddedSuccess);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                return this.StatusCode(500, MessageConstants.InternalServerError);
            }
        }

        /// <summary>
        /// Gets a list of all clients and their investments.
        /// </summary>
        /// <returns>A list of clients.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Authentication.ClientsManagePolicy)]
        [HttpGet]
        [Route(Routes.ClientsInvestments)]
        public async Task<IActionResult> GetClientInvestmentsAsync()
        {
            try
            {
                var result = await this.clientService.GetAllClientsWithInvestmentsAsync().ConfigureAwait(false);

                if (result == null || !result.Any())
                {
                    return this.NotFound(MessageConstants.NoClientsInvestmentsFound);
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of all clients and their investments, or a specific client if an ID is provided.
        /// </summary>
        /// <param name="id">The optional client ID. If provided, only the client with this ID and their investments will be returned.</param>
        /// <returns>A list of clients or a specific client with their investments.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Authentication.ClientsManagePolicy)]
        [HttpGet]
        [Route(Routes.ClientInvestment)]
        public async Task<IActionResult> GetClientInvestmentsAsyncById(Guid? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var clientWithInvestments = await this.clientService.GetClientWithInvestmentsByIdAsync(id.Value);

                    if (clientWithInvestments == null)
                    {
                        return this.NotFound("Client not found.");
                    }

                    return this.Ok(clientWithInvestments);
                }
                else
                {
                    var clientsWithInvestments = await this.clientService.GetAllClientsWithInvestmentsAsync();
                    return this.Ok(clientsWithInvestments);
                }
            }
            catch (Exception ex)
            {
               this.logger.LogError(ex, ex.Message);

               return this.StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Gets a paginated list of all clients and their investments.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <returns>A paginated list of clients.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Authentication.ClientsManagePolicy)]
        [HttpGet]
        [Route(Routes.ClientsInvestmentsPaginated)]
        public async Task<IActionResult> GetClientInvestmentsAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = await this.clientService.GetClientsWithInvestmentsAsync(pageNumber, pageSize).ConfigureAwait(false);

                if (result == null || !result.Any())
                {
                    return this.NotFound(MessageConstants.NoClientsInvestmentsFound);
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Transfers funds from one investment to another.
        /// </summary>
        /// <param name="sourceInvestmentId">The unique identifier of the source investment.</param>
        /// <param name="destinationInvestmentId">The unique identifier of the destination investment.</param>
        /// <param name="amount">The amount to transfer.</param>
        /// <returns>A status of the operation.</returns>
        /// <remarks>
        /// This method performs a transaction to ensure that the transfer is atomic. It will return a 400 Bad Request
        /// if the request parameters are invalid or if any error occurs during the operation.
        /// </remarks>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Authentication.InvestmentsManagePolicy)]
        [HttpPost]
        [Route(Routes.TransferFunds)]
        public async Task<IActionResult> TransferFundsAsync(
            [FromQuery] Guid sourceInvestmentId,
            [FromQuery] Guid destinationInvestmentId,
            [FromQuery] decimal amount)
        {
            if (amount <= 0)
            {
                return this.BadRequest(MessageConstants.TransferAmountInvalid);
            }

            try
            {
                await this.clientService.TransferInvestmentFundsAsync(sourceInvestmentId, destinationInvestmentId, amount);

                return this.Ok(MessageConstants.TransferFundsSuccess);
            }
            catch (ArgumentException ex)
            {
                this.logger.LogWarning(ex, MessageConstants.TransferFundsInvalidArgumentError);

                return this.BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                this.logger.LogWarning(ex, MessageConstants.TransferFundsNotFoundError);

                return this.NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                this.logger.LogWarning(ex, MessageConstants.TransferFundsOperationError);

                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, MessageConstants.TransferFundsUnexpectedError);

                return this.StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.TransferFundsUnexpectedError);
            }
        }
    }
}

// <copyright file="ClientsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SecureBankAPI.Controllers
{
    using System;
    using System.Collections.Generic;
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
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> logger;
        private readonly IClientService clientService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientsController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="clientService">The service for client operations.</param>
        public ClientsController(ILogger<ClientsController> logger, IClientService clientService)
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Authentication.ClientsReadAllPolicy)]
        [HttpGet]
        [Route(Routes.Clients)]
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
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Edits an existing client.
        /// </summary>
        /// <param name="clientId">The client's ID.</param>
        /// <returns>Enables editing an existing client.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Authentication.ClientsManagePolicy)]
        [HttpPut]
        [Route(Routes.UpdateClient)]
        public async Task<IActionResult> UpdateClientAsync(int clientId)
        {
            await Task.CompletedTask;
            return this.NoContent();
        }
    }
}

// <copyright file="ClientsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SecureBankAPI.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SecureBankAPI.Models;

    /// <summary>
    /// Controller for managing clients and their investments.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        /// <summary>
        /// Logger instance.
        /// </summary>
        private readonly ILogger<ClientsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientsController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        public ClientsController(ILogger<ClientsController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets a list of all clients.
        /// </summary>
        /// <returns>A list of clients.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Authentication.ClientsReadAllPolicy)]
        [HttpGet]
        [Route(Routes.Clients)]
        public async Task<IEnumerable<WeatherForecast>> GetClientsAsync()
        {
            await Task.CompletedTask;

            return Enumerable.Empty<WeatherForecast>();
        }

        /// <summary>
        /// Adds a new client.
        /// </summary>
        /// <returns>Enables adding a new client.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Authentication.ClientsManagePolicy)]
        [HttpPost]
        [Route(Routes.AddClient)]
        public async Task AddClientAsync()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Edits an existing client.
        /// </summary>
        /// <param name="clientId">The client's ID.</param>
        /// <returns>Enables editing an existing client.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Authentication.ClientsManagePolicy)]
        [HttpPut]
        [Route(Routes.UpdateClient)]
        public async Task UpdateClientAsync(int clientId)
        {
            await Task.CompletedTask;
        }
    }
}
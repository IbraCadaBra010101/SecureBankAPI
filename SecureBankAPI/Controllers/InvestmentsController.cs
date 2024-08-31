// <copyright file="InvestmentsController.cs" company="PlaceholderCompany">
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
    public class InvestmentsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
        };

        /// <summary>
        /// Logger instance.
        /// </summary>
        private readonly ILogger<InvestmentsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvestmentsController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        public InvestmentsController(ILogger<InvestmentsController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Adds a new investment for a specific client.
        /// </summary>
        /// <returns>Enables Adding a new investment.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Authentication.InvestmentsManagePolicy)]
        [HttpPost]
        [Route(Routes.AddInvestment)]
        public async Task AddInvestmentAsync()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Gets all investments for a specific client.
        /// </summary>
        /// <param name="clientId">The client's ID.</param>
        /// <returns>Returns a list of investments for the specified client.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Authentication.InvestmentsReadPolicy)]
        [HttpGet]
        [Route(Routes.ClientInvestments)]
        public async Task<IEnumerable<Investment>> GetClientInvestmentsAsync(int clientId)
        {
            return await Task.FromResult(Enumerable.Empty<Investment>());
        }

        /// <summary>
        /// Edits an existing investment for a specific client.
        /// </summary>
        /// <param name="investmentId">The investment's ID.</param>
        /// <returns>Enables editing an existing investment.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Authentication.InvestmentsManagePolicy)]
        [HttpPut]
        [Route("update-investment/{investmentId}")]
        public async Task UpdateInvestmentAsync(int investmentId)
        {
            await Task.CompletedTask;
        }
    }
}
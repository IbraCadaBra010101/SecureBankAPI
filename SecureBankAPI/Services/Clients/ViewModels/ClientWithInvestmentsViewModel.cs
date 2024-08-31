namespace SecureBankAPI.Services.Clients.ViewModels
{
    using System.Collections.Generic;
    using SecureBankAPI.Models;
#nullable disable
    /// <summary>
    /// Represents a client along with their associated investments.
    /// </summary>
    public class ClientWithInvestmentsViewModel
    {
        /// <summary>
        /// Gets or sets the client details.
        /// </summary>
        /// <value>
        /// The <see cref="Client"/> object that contains the client's personal and contact information.
        /// </value>
        public Client Client { get; set; }

        /// <summary>
        /// Gets or sets the list of investments associated with the client.
        /// </summary>
        /// <value>
        /// A <see cref="List{Investment}"/> that contains the investments made by the client. Each investment
        /// entry represents an individual investment with its details.
        /// </value>
        public List<Investment> Investments { get; set; }
    }
}
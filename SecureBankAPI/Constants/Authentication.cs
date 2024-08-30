namespace SecureBankAPI.Models
{
    /// <summary>
    /// Represents a client in the investment bank system.
    /// </summary>
    public static class Authentication
    {
        /// <summary>
        /// Gets or sets the Azure AD App service principal section.
        /// </summary>
        /// <remarks>
        /// The Azure AD section is used to set up secure authentication using the app service principal application registration.
        /// </remarks>
        internal const string AzureADSection = "AzureAd";

        /// <summary>
        /// Gets or sets the the Scope to read all clients.
        /// </summary>
        /// <remarks>
        /// The Scope name constant for reading all clients.
        /// </remarks>
        internal const string ClientsReadAllRole = "Clients.Read.All";

        /// <summary>
        /// Gets or sets the the role policy to read all clients.
        /// </summary>
        /// <remarks>
        /// The policy role internally can read all clients.
        /// </remarks>
        internal const string ClientsReadAllPolicy = "ClientsReadAll";
    }
}

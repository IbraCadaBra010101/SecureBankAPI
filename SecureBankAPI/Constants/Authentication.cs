namespace SecureBankAPI.Models
{
    /// <summary>
    /// Represents authentication and authorization constants used in the investment bank system.
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
        /// Gets or sets the scope to read all clients.
        /// </summary>
        /// <remarks>
        /// The scope name constant for reading all clients.
        /// </remarks>
        internal const string ClientsReadAllRole = "Clients.Read.All";

        /// <summary>
        /// Gets or sets the role policy to read all clients.
        /// </summary>
        /// <remarks>
        /// The policy role internally can read all clients.
        /// </remarks>
        internal const string ClientsReadAllPolicy = "ClientsReadAll";

        /// <summary>
        /// Gets or sets the scope to manage clients.
        /// </summary>
        /// <remarks>
        /// The scope name constant for managing clients.
        /// </remarks>
        internal const string ClientsManageRole = "Clients.Manage";

        /// <summary>
        /// Gets or sets the role policy to manage clients.
        /// </summary>
        /// <remarks>
        /// The policy role internally can manage clients.
        /// </remarks>
        internal const string ClientsManagePolicy = "ClientsManage";

        /// <summary>
        /// Gets or sets the scope to manage investments.
        /// </summary>
        /// <remarks>
        /// The scope name constant for managing investments.
        /// </remarks>
        internal const string InvestmentsManageRole = "Investments.Manage";

        /// <summary>
        /// Gets or sets the role policy to manage investments.
        /// </summary>
        /// <remarks>
        /// The policy role internally can manage investments.
        /// </remarks>
        internal const string InvestmentsManagePolicy = "InvestmentsManage";

        /// <summary>
        /// Gets or sets the scope to read all investments.
        /// </summary>
        /// <remarks>
        /// The scope name constant for reading all investments.
        /// </remarks>
        internal const string InvestmentsReadRole = "Investments.Read";

        /// <summary>
        /// Gets or sets the role policy to read all investments.
        /// </summary>
        /// <remarks>
        /// The policy role internally can read all investments.
        /// </remarks>
        internal const string InvestmentsReadPolicy = "InvestmentsRead";
    }
}
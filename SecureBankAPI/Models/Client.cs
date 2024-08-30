namespace SecureBankAPI.Models
{
    /// <summary>
    /// Represents a client in the investment bank system.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets or sets the unique identifier for the client.
        /// </summary>
        /// <remarks>
        /// This GUID ID is the primary key for the client entity in the database.
        /// </remarks>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the client.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the client.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the client.
        /// </summary>
        /// <remarks>
        /// The email address is used for communication purposes and must be unique.
        /// </remarks>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the client.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the client.
        /// </summary>
        /// <remarks>
        /// The date of birth is used for verification and compliance purposes.
        /// </remarks>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the residential address of the client.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets the national identification number of the client.
        /// </summary>
        /// <remarks>
        /// This number is required for compliance with financial regulations.
        /// </remarks>
        public string? NationalId { get; set; }

        /// <summary>
        /// Gets or sets the date when the client was registered in the system.
        /// </summary>
        public DateTime DateRegistered { get; set; }

        /// <summary>
        /// Gets or sets the collection of investments associated with the client.
        /// </summary>
        /// <remarks>
        /// This is a navigation property that establishes a one-to-many relationship
        /// between the client and their investments.
        /// </remarks>
        public ICollection<Investment>? Investments { get; set; }
    }
}

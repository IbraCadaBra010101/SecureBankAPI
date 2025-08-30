namespace FastighetsAPI.Models.Validation
{
    /// <summary>
    /// Represents a validation error with specific details.
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Gets or sets the failed or property that failed validation.
        /// </summary>
        public string Field { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the errors that describing the validation failure.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the value that failed validation.
        /// </summary>
        public object? InvalidValue { get; set; }

        /// <summary>
        /// Gets or sets the validation rule that was violated.
        /// </summary>
        public string? ValidationRule { get; set; }

        /// <summary>
        /// Gets or sets additional context about the validation failure.
        /// </summary>
        public string? Context { get; set; }
    }

}

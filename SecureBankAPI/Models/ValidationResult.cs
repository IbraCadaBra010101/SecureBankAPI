namespace RealEstateAPI.Models
{
    /// <summary>
    /// Result of webhook payload validation.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether gets or sets whether the validation passed.
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the list of validation errors.
        /// </summary>
        public List<ValidationError> Errors { get; set; } = new();
    }
}

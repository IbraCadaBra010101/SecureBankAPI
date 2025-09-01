namespace FastighetsAPI.Models.Validation
{

    public class ValidationError
    {
    
        public string Field { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public object? InvalidValue { get; set; }

        public string? ValidationRule { get; set; }

        public string? Context { get; set; }
    }

}

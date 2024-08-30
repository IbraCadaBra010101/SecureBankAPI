namespace SecureBankAPI
{
    /// <summary>
    /// Represents the weather forecast for a specific date.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Gets or sets the date of the weather forecast.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets the temperature in Celsius.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Gets the temperature in Fahrenheit.
        /// </summary>
        /// <remarks>
        /// The temperature in Fahrenheit is calculated using the formula: 32 + (int)(TemperatureC / 0.5556).
        /// </remarks>
        public int TemperatureF => 32 + (int)(this.TemperatureC / 0.5556);

        /// <summary>
        /// Gets or sets the summary or description of the weather.
        /// </summary>
        /// <remarks>
        /// The summary typically describes the general weather conditions, such as "Sunny" or "Cloudy".
        /// </remarks>
        public string? Summary { get; set; }
    }
}
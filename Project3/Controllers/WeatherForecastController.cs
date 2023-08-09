using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;


namespace Project3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly string _connectionString;

        public WeatherForecastController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlDBConnection");
        }
       [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            try
            {
                List<WeatherForecast> forecasts = new List<WeatherForecast>();

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("SELECT TemperatureC, Summary FROM WeatherForecast", connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            WeatherForecast forecast = new WeatherForecast
                            {
                                TemperatureC = reader.GetInt32(0),
                                Summary = reader.GetString(1)
                            };

                            forecasts.Add(forecast);
                        }
                    }
                }

                return Ok(forecasts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //    private static readonly string[] Summaries = new[]
        //    {
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //    private readonly ILogger<WeatherForecastController> _logger;

        //    public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //    {
        //        _logger = logger;
        //    }

        //    [HttpGet]
        //    public IEnumerable<WeatherForecast> Get()
        //    {
        //        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //        {
        //            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //            TemperatureC = Random.Shared.Next(-20, 55),
        //            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //        })
        //        .ToArray();
        //    }
    }
}
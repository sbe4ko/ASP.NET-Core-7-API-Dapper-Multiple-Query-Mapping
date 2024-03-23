using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;  
using System.Data.Common;
using System.Data.SqlClient;

namespace MltipletableWithDapperApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet]
        [Route("StateAndCity")]
        public async Task<IActionResult> StateAndCity()
        {
            CountryDetail _CountryDetail = new CountryDetail();
            try
            {

                    var procedure = "getStateAndCity";
                var conn = CreateConnection();
                    var objDetails = await SqlMapper.QueryMultipleAsync(CreateConnection(), procedure, null, commandType: CommandType.StoredProcedure);
                    _CountryDetail.States = objDetails.Read<States>().ToList();
                    _CountryDetail.City = objDetails.Read<City>().ToList();
                    return Ok(_CountryDetail);
            }

            catch
            {
                throw;
            }
        }
        private IDbConnection CreateConnection()
              => new SqlConnection("server=your server;Initial Catalog=Your DB;User ID=your user;Password= your Password");
    }
}
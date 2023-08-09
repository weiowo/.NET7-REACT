using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Project3.DataAccessLayer;
using Project3.Model;
using System.Configuration;

namespace Project3.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("[controller]/[Action]")]
    public class RestaurantController : ControllerBase
    {
        private readonly string _connectionString;
        public readonly IRestaurant _restaurant;
        public RestaurantController(IRestaurant restaurant, IConfiguration configuration)
        {
            _restaurant = restaurant;
            _connectionString = configuration.GetConnectionString("MySqlDBConnection");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantRequest request)
        {
            CreateRestaurantResponse response = new CreateRestaurantResponse();
            try
            {
                response = await _restaurant.CreateRestaurant(request);

            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantData>> GetRestaurants()
        {
            try
            {
                List<RestaurantData> restaurants = new List<RestaurantData>();

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM restaurants", connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RestaurantData restaurant = new RestaurantData
                            {
                                restaurantUid = reader.GetString(0),
                                restaurantName = reader.GetString(1),
                                restaurantType = reader.GetString(2),
                                restaurantStatus = reader.GetString(3),
                            };

                            restaurants.Add(restaurant);
                        }
                    }
                }
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

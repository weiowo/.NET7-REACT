using MySql.Data.MySqlClient;
using Project3.Model;

namespace Project3.DataAccessLayer
{
    public class Restaurant : IRestaurant
    {
        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;

        public Restaurant(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlConnection = new MySqlConnection(_configuration["ConnectionStrings:MySqlDBConnection"]);
        }

        public async Task<CreateRestaurantResponse> CreateRestaurant(CreateRestaurantRequest request)
        {
            CreateRestaurantResponse response = new CreateRestaurantResponse();
            response.isSuccess = true;
            response.message = "Successful";
            string restaurantUid = Guid.NewGuid().ToString(); // Generate restaurantUid here

            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"INSERT INTO restaurant_system.restaurants(restaurantUid, restaurantName, restaurantType, restaurantStatus, createdOn) 
                      VALUES(@restaurantUid, @restaurantName, @restaurantType, @restaurantStatus, NOW())";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@restaurantUid", restaurantUid); // Use the generated restaurantUid
                    sqlCommand.Parameters.AddWithValue("@restaurantName", request.restaurantName);
                    sqlCommand.Parameters.AddWithValue("@restaurantType", request.restaurantType);
                    sqlCommand.Parameters.AddWithValue("@restaurantStatus", request.restaurantStatus);

                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.isSuccess = false;
                        response.message = "Something went wrong";
                        return response;
                    }
                }

                if (request.foods != null && request.foods.Any())
                {
                    foreach (var food in request.foods)
                    {
                        string foodUid = Guid.NewGuid().ToString();

                        string insertFoodQuery = @"INSERT INTO restaurant_system.foods(foodUid, foodName) 
                                  VALUES(@foodUid, @foodName)";

                        using (MySqlCommand insertFoodCommand = new MySqlCommand(insertFoodQuery, _mySqlConnection))
                        {
                            insertFoodCommand.CommandType = System.Data.CommandType.Text;
                            insertFoodCommand.CommandTimeout = 180;
                            insertFoodCommand.Parameters.AddWithValue("@foodUid", foodUid);
                            insertFoodCommand.Parameters.AddWithValue("@foodName", food);

                            int foodStatus = await insertFoodCommand.ExecuteNonQueryAsync();
                            if (foodStatus <= 0)
                            {
                                response.isSuccess = false;
                                response.message = "Something went wrong";
                                return response;
                            }
                        }

                        string insertRelationQuery = @"INSERT INTO restaurant_system.restaurantfoodrelation(restaurantUid, foodUid) 
                                   VALUES(@restaurantUid, @foodUid)";

                        using (MySqlCommand insertRelationCommand = new MySqlCommand(insertRelationQuery, _mySqlConnection))
                        {
                            insertRelationCommand.CommandType = System.Data.CommandType.Text;
                            insertRelationCommand.CommandTimeout = 180;
                            insertRelationCommand.Parameters.AddWithValue("@restaurantUid", restaurantUid); // Use the same restaurantUid
                            insertRelationCommand.Parameters.AddWithValue("@foodUid", foodUid);

                            int relationStatus = await insertRelationCommand.ExecuteNonQueryAsync();
                            if (relationStatus <= 0)
                            {
                                response.isSuccess = false;
                                response.message = "Something went wrong";
                                return response;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }
            return response;
        }


    }
}

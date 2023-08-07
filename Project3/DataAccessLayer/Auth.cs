using MySql.Data.MySqlClient;
using Project3.Model;

namespace Project3.DataAccessLayer
{
    public class Auth : IAuth
    {
        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;

        public Auth(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlConnection = new MySqlConnection(_configuration["ConnectionStrings:MySqlDBConnection"]);
        }
        public Task<SignInResponse> SignIn(SignInRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            response.isSuccess = true;
            response.message = "Successful";
            try
            {

                if (!request.password.Equals(request.confirmPassword))
                {
                    response.isSuccess = false;
                    response.message = "Password And Confirm Password Not Match";
                    return response;
                }
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"insert into restaurant_system.users(userUid, userName, password, role, isActive, createdOn) 
                                  Values(@userUid, @userName, @password, @role, 1, NOW())";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@userUid",  Guid.NewGuid().ToString());
                    sqlCommand.Parameters.AddWithValue("@userName", request.userName);
                    sqlCommand.Parameters.AddWithValue("@password", request.password);
                    sqlCommand.Parameters.AddWithValue("@role", request.role);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.isSuccess = false;
                        response.message = "Something went wrong";
                        return response;
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

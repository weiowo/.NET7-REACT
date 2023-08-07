using System.ComponentModel.DataAnnotations;

namespace Project3.Model
{
    public class SignInRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }

    public class SignInResponse
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
    }

}
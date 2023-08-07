using System.ComponentModel.DataAnnotations;

namespace Project3.Model
{
    public class SignUpRequest
    {
        //UserName, PassWord, Role
        public string userName { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string role { get; set; }
    }

    public class SignUpResponse
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
    }
}
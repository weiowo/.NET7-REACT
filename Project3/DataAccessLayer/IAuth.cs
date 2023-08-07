using Project3.Model;

namespace Project3.DataAccessLayer
{
    public interface IAuth
    {
        public Task<SignUpResponse> SignUp(SignUpRequest request);
        public Task<SignInResponse> SignIn(SignInRequest request);
    }
}

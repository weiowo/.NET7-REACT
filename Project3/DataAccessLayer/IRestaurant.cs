using Project3.Model;

namespace Project3.DataAccessLayer
{
    public interface IRestaurant
    {
        public Task<CreateRestaurantResponse> CreateRestaurant(CreateRestaurantRequest request);
    }
}

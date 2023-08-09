using System.ComponentModel.DataAnnotations;

namespace Project3.Model
{
    public class CreateRestaurantRequest
    {
        public string restaurantName { get; set; }
        public string restaurantType { get; set; }
        public string restaurantStatus { get; set; }
        public string[] foods { get; set; }
    }

    public class CreateRestaurantResponse
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
    }

    public class RestaurantData
    {
        public string restaurantUid { get; set; }
        public string restaurantName { get; set; }
        public string restaurantType { get; set; }
        public string restaurantStatus { get; set; }
    }
}
namespace JustEatAPI.DTOs  
{
    public class RestaurantDto
    {
        public required string Name { get; set; }
        public required string Cuisines { get; set; }
        public double Rating { get; set; }
        public required string Address { get; set; }
    }
}


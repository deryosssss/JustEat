namespace JustEatAPI.DTOs  
{
    public class RestaurantDto
    {
        public required string Name { get; set; }
        public required  List<string>  Cuisines { get; set; }
        public double Rating { get; set; }
        public required string Address { get; set; }
    }
}
// Introduced DTOs to standardize and format responses before sending them to the frontend. This will make the system more maintainable and less dependent on changes in external APIs.

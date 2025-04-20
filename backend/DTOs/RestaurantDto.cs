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
// The Dto suffix stands for Data Transfer Object – it’s a lightweight class used to send data between layers (like from your API to your frontend).

/*
EXAMPLE OUTPUT-
{
  "name": "Pizza Palace",
  "cuisines": ["Pizza", "Italian"],
  "rating": 4.2,
  "address": "123 High St, London, EC1A 1BB"
}
*/
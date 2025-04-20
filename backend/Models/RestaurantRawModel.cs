public class RestaurantRawModel
{
    public required string name { get; set; }
    public required List<Cuisine> cuisines { get; set; }
    public required Rating rating { get; set; }
    public required Address address { get; set; }  // ✅ this line changes
}
public class Cuisine
{
    public string name { get; set; }
}
public class Address
{
    public string firstLine { get; set; }
    public string city { get; set; }
    public string postalCode { get; set; }
}
public class Rating
{
    public double starRating { get; set; }
}


using AutoMapper;
// ^ This brings in the AutoMapper library, which is used for mapping one object type to another
public class RatingConverter : ITypeConverter<object, double>
// Declares a class RatingConverter. It implements ITypeConverter<object, double>, which means: it converts from any object (the source) to a double 
{
    public double Convert(object source, double destination, ResolutionContext context)
    {
        if (source == null) return 0;

        return double.TryParse(source.ToString(), out var rating) ? rating : 0;

        // Tries to convert the source to a string, then parse it to a double. If parsing succeeds → returns the rating. If parsing fails → returns 0.
    }
}

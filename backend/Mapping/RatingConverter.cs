using AutoMapper;

public class RatingConverter : ITypeConverter<object, double>
{
    public double Convert(object source, double destination, ResolutionContext context)
    {
        if (source == null) return 0;

        return double.TryParse(source.ToString(), out var rating) ? rating : 0;
    }
}

namespace DishGuide;

public class Dish
{
    public string Name { get; set; }
    public string Cuisine { get; set; }
    public decimal Price { get; set; }

    public Dish(string name, string cuisine, decimal price)
    {
        Name = name;
        Cuisine = cuisine;
        Price = price;
    }
}

public class Review
{
    public string DishName { get; set; }
    public int Rating { get; set; }

    public Review(string dishName, int rating)
    {
        DishName = dishName;
        Rating = rating;
    }
}

public class RecommendationService
{
    public List<Dish> FilterByCuisine(List<Dish> dishes, string cuisine)
    {
        if (dishes == null)
            throw new ArgumentNullException(nameof(dishes));

        if (string.IsNullOrWhiteSpace(cuisine))
            throw new ArgumentException("Cuisine cannot be empty");

        return dishes
            .Where(d => d.Cuisine.Equals(cuisine, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Dish> FilterByMaxPrice(List<Dish> dishes, decimal maxPrice)
    {
        if (dishes == null)
            throw new ArgumentNullException(nameof(dishes));

        if (maxPrice < 0)
            throw new ArgumentException("Max price cannot be negative");

        return dishes
            .Where(d => d.Price <= maxPrice)
            .ToList();
    }

    public double CalculateAverageRating(List<Review> reviews, string dishName)
    {
        if (reviews == null)
            throw new ArgumentNullException(nameof(reviews));

        if (string.IsNullOrWhiteSpace(dishName))
            throw new ArgumentException("Dish name cannot be empty");

        var dishReviews = reviews
            .Where(r => r.DishName.Equals(dishName, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (dishReviews.Count == 0)
            return 0;

        return Math.Round(dishReviews.Average(r => r.Rating), 2);
    }

    public List<Dish> GetRecommendedDishes(
        List<Dish> dishes,
        List<Review> reviews,
        double minRating)
    {
        if (dishes == null)
            throw new ArgumentNullException(nameof(dishes));

        if (reviews == null)
            throw new ArgumentNullException(nameof(reviews));

        if (minRating < 0 || minRating > 5)
            throw new ArgumentException("Minimum rating must be between 0 and 5");

        return dishes
            .Where(d => CalculateAverageRating(reviews, d.Name) >= minRating)
            .ToList();
    }
}
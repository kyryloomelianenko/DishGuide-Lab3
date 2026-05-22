using DishGuide;

namespace DishGuide.Tests;

public class RecommendationServiceTests
{
    [Fact]
    public void FilterByCuisine_ValidCuisine_ReturnsMatchingDishes()
    {
        var service = new RecommendationService();

        var dishes = new List<Dish>
        {
            new Dish("Pizza", "Italian", 250),
            new Dish("Sushi", "Japanese", 300),
            new Dish("Pasta", "Italian", 200)
        };

        var result = service.FilterByCuisine(dishes, "Italian");

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void FilterByCuisine_EmptyCuisine_ThrowsException()
    {
        var service = new RecommendationService();

        var dishes = new List<Dish>();

        Assert.Throws<ArgumentException>(() =>
            service.FilterByCuisine(dishes, ""));
    }

    [Fact]
    public void FilterByMaxPrice_ValidPrice_ReturnsFilteredDishes()
    {
        var service = new RecommendationService();

        var dishes = new List<Dish>
        {
            new Dish("Burger", "American", 150),
            new Dish("Steak", "American", 500)
        };

        var result = service.FilterByMaxPrice(dishes, 200);

        Assert.Single(result);
    }

    [Fact]
    public void FilterByMaxPrice_ZeroPrice_ReturnsEmpty()
    {
        var service = new RecommendationService();

        var dishes = new List<Dish>
        {
            new Dish("Burger", "American", 150)
        };

        var result = service.FilterByMaxPrice(dishes, 0);

        Assert.Empty(result);
    }

    [Fact]
    public void FilterByMaxPrice_NegativePrice_ThrowsException()
    {
        var service = new RecommendationService();

        var dishes = new List<Dish>();

        Assert.Throws<ArgumentException>(() =>
            service.FilterByMaxPrice(dishes, -1));
    }

    [Fact]
    public void CalculateAverageRating_ValidReviews_ReturnsAverage()
    {
        var service = new RecommendationService();

        var reviews = new List<Review>
        {
            new Review("Pizza", 5),
            new Review("Pizza", 3)
        };

        var result = service.CalculateAverageRating(reviews, "Pizza");

        Assert.Equal(4, result);
    }

    [Fact]
    public void CalculateAverageRating_NoReviews_ReturnsZero()
    {
        var service = new RecommendationService();

        var reviews = new List<Review>();

        var result = service.CalculateAverageRating(reviews, "Pizza");

        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateAverageRating_EmptyDishName_ThrowsException()
    {
        var service = new RecommendationService();

        var reviews = new List<Review>();

        Assert.Throws<ArgumentException>(() =>
            service.CalculateAverageRating(reviews, ""));
    }

    [Fact]
    public void GetRecommendedDishes_ValidRating_ReturnsRecommended()
    {
        var service = new RecommendationService();

        var dishes = new List<Dish>
        {
            new Dish("Pizza", "Italian", 200),
            new Dish("Burger", "American", 150)
        };

        var reviews = new List<Review>
        {
            new Review("Pizza", 5),
            new Review("Pizza", 4),
            new Review("Burger", 2)
        };

        var result = service.GetRecommendedDishes(dishes, reviews, 4);

        Assert.Single(result);
    }

    [Fact]
    public void GetRecommendedDishes_InvalidRating_ThrowsException()
    {
        var service = new RecommendationService();

        var dishes = new List<Dish>();
        var reviews = new List<Review>();

        Assert.Throws<ArgumentException>(() =>
            service.GetRecommendedDishes(dishes, reviews, 6));
    }

    [Fact]
    public void GetRecommendedDishes_BoundaryRatingFive_WorksCorrectly()
    {
        var service = new RecommendationService();

        var dishes = new List<Dish>
        {
            new Dish("Pizza", "Italian", 200)
        };

        var reviews = new List<Review>
        {
            new Review("Pizza", 5)
        };

        var result = service.GetRecommendedDishes(dishes, reviews, 5);

        Assert.Single(result);
    }
}
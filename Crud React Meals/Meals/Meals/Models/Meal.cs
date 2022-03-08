namespace Meals.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public Provider Provider { get; set; }
    }
}
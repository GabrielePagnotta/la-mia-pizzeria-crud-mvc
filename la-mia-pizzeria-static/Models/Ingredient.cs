namespace la_mia_pizzeria_static.Models
{
    public class Ingredient
    {
        public int Id { get; set; } 
        public string Ingredienti { get; set; }
        

        public List<Pizza>? Pizzas { get; set; }

    }
}

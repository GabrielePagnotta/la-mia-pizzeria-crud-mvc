using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; }
        public List<Category>?Categories { get; set; }  
        
        public List<SelectListItem> ? Ingredientis { get; set; }
        public List<string> SelectedIngredients { get; set; }
    }
}

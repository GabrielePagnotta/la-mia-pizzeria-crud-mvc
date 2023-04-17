using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int ?Id { get; set; }
        [Required(ErrorMessage ="Il campo è obbligatorio")]
        [StringLength(25, ErrorMessage ="il campo inserire no deve superare i 25 caratteri")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(100, ErrorMessage = "il campo inserire no deve superare i 25 caratteri")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string  Image { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Range(1,50 ,ErrorMessage = "il numero deve essere maggiore di 0")]
        public string Price { get; set;}
        
        public int? CategoryId { get; set; }
        public Category ? Category { get; set; }

        public List<Ingredient>? Ingredients { get; set; }

        public Pizza() { 

            //per evitare NullReferenceException
            Ingredients = new List<Ingredient>();
        }

        public Pizza(int id, string name, string description, string image, string price,List<Ingredient> ingredients)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
            Price = price;

            if(ingredients == null)
            {
                throw new ArgumentNullException();
            }

            Ingredients = ingredients;
        }

        public Pizza(int id, string name, string description, string image, string price)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
            Price = price;

            Ingredients = new List<Ingredient>();
        }
    }
}

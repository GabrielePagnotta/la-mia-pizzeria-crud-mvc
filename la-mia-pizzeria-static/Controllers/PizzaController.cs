using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Controllers
{
    
    public class PizzaController : Controller
    {
        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            

            using var ctx = new DbPizzaContext();
            var pizza= ctx.Pizzas.ToArray();
            return View(pizza);
        }
        [Authorize (Roles ="User")]
        public IActionResult Details(int id)
        {
            using (var ctx = new DbPizzaContext()) 
            {
                Pizza singleuser = ctx.Pizzas.SingleOrDefault(h => h.Id == id);

                if (singleuser == null) 
                {
                    return NotFound($"l'id numero {id} non è stato trovato");
                }

                return View(singleuser);
                    
            };
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        

        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                using(DbPizzaContext context = new DbPizzaContext())
                {
                    List<Category> categori = context.Categories.ToList();
                    List<Ingredient> ingredients = context.Ingredients.ToList();
                    List<SelectListItem> listingr = new List<SelectListItem>();
                    foreach (Ingredient elem in ingredients)
                    {
                        listingr.Add(new SelectListItem()
                        { Text = elem.Ingredienti, Value = elem.Id.ToString() });
                    }
                    data.Categories = categori;
                    data.Ingredientis = listingr;
                }
                return View("Create",data);
            }
            using (DbPizzaContext context = new DbPizzaContext())
            {
                Pizza newpizza = new Pizza();
                //non mi devo preoccupare di
                //newpizza.Ingredients = new List<Ingredient>();
                //perchè ho deciso che lo fa il costruttore
                newpizza.Name = data.Pizza.Name;
                newpizza.Description = data.Pizza.Description;
                newpizza.Price = data.Pizza.Price;
                newpizza.Image = data.Pizza.Image;
                newpizza.CategoryId= data.Pizza.CategoryId;

                if(data.SelectedIngredients != null)
                {
                    foreach(string selectedIngredientId in data.SelectedIngredients)
                    {
                        int selectedId = int.Parse(selectedIngredientId);
                        Ingredient ingredient = context.Ingredients.Where(p=> p.Id == selectedId).FirstOrDefault();
                        newpizza.Ingredients.Add(ingredient);
                    }
                }
                context.Pizzas.Add(newpizza);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IActionResult Create(Ingredient ingredient) 
        {
            using (DbPizzaContext context = new DbPizzaContext())
            {
                List<Category> Categories = context.Categories.ToList();
                List <Ingredient> ingredients = context.Ingredients.ToList();

                
                PizzaFormModel Model = new PizzaFormModel();
                Model.Pizza = new Pizza();
                Model.Categories = Categories;
                
                
                List<SelectListItem> listingr=new List<SelectListItem>();
                foreach (Ingredient elem in ingredients)
                {
                    listingr.Add(new SelectListItem()
                    { Text = elem.Ingredienti, Value = elem.Id.ToString() });
                }
                Model.Ingredientis = listingr;
                return View("Create",Model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            using (DbPizzaContext ctx = new DbPizzaContext()) 
            {
                Pizza editpizza = ctx.Pizzas.Where(Pizza=> Pizza.Id == id).FirstOrDefault();

                if(editpizza == null)
                {
                    return NotFound();
                }

                var formModel = new PizzaFormModel
                {
                    Pizza = editpizza,
                    Categories = ctx.Categories.ToList()
                };

                return View(formModel);
                
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id,PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                using(DbPizzaContext ctx =new DbPizzaContext())
                {

                    List<Category> categories = ctx.Categories.ToList();
                    data.Categories = categories;
                return View("Update", data);
                }
            }

            using (DbPizzaContext ctx = new DbPizzaContext()) 
            {
                Pizza pizzaeditinfo = ctx.Pizzas.Where(Pizza => Pizza.Id == id).FirstOrDefault();

                if(pizzaeditinfo != null) 
                {
                    pizzaeditinfo.Name = data.Pizza.Name;
                    pizzaeditinfo.Description = data.Pizza.Description;
                    pizzaeditinfo.Price = data.Pizza.Price;
                    pizzaeditinfo.Image = data.Pizza.Image;
                    pizzaeditinfo.CategoryId = data.Pizza.CategoryId;
                    
                    ctx.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int Id) 
        {
            using (DbPizzaContext context =new DbPizzaContext())
            {
                Pizza deletepizza = context.Pizzas.Where(pizza => pizza.Id==Id).FirstOrDefault();

                if(deletepizza != null)
                {
                    context.Pizzas.Remove(deletepizza);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}

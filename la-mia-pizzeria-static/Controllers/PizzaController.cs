﻿using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            

            using var ctx = new DbPizzaContext();
            var pizza= ctx.Pizzas.ToArray();
            return View(pizza);
        }

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
                    data.Categories = categori; 
                }
                return View("Create",data);
            }
            using (DbPizzaContext context = new DbPizzaContext())
            {
                Pizza newpizza = new Pizza();
                newpizza.Name = data.Pizza.Name;
                newpizza.Description = data.Pizza.Description;
                newpizza.Price = data.Pizza.Price;
                newpizza.Image = data.Pizza.Image;
                newpizza.CategoryId= data.Pizza.CategoryId;
                context.Add(newpizza);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Create() 
        {
            using (DbPizzaContext context = new DbPizzaContext())
            {
                List<Category> Categories = context.Categories.ToList();

                PizzaFormModel Model = new PizzaFormModel();
                Model.Pizza = new Pizza();
                Model.Categories = Categories;
                return View("Create",Model);
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using (DbPizzaContext ctx = new DbPizzaContext()) 
            {
                Pizza editpizza = ctx.Pizzas.Where(Pizza=> Pizza.Id == id).FirstOrDefault();

                if(editpizza == null)
                {
                    return NotFound();
                }
                else
                {
                    List<Category> Categories = ctx.Categories.ToList();
                    PizzaFormModel Model = new PizzaFormModel();
                    Model.Pizza = new Pizza();
                    Model.Categories = Categories;
                    return View(Model);
                }
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
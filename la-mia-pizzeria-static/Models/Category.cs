﻿namespace la_mia_pizzeria_static.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pizza> pizzas { get; set; }
    }
}

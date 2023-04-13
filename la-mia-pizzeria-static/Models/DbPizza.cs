using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models
{
    public class DbPizzaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pizzabase;Integrated Security=True;encrypt=false");
        }

        public void seed()
        {
            if (!Pizzas.Any())
            {
                var seed = new Pizza[]
                {
                        new Pizza {
                            Name = "Margherita",
                            Description = "abcd",
                            Image  = "https://picsum.photos/id/292/100/100",
                            Price ="7"
                        },

                        new Pizza {
                            Name = "Marinara",
                            Description = "abcd",
                            Image  = "https://picsum.photos/id/292/100/100",
                            Price ="5"
                        }
                };
                Pizzas.AddRange(seed);
                SaveChanges();

                if (!Categories.Any())
                {
                    var seedtwo = new Category[]
                     {
                        new Category {
                            Name = "Pizze classiche"

                        },

                        new Category
                        {
                            Name = "Pizze Bianche"

                        },
                        new Category
                        {
                            Name = "Pizze  Vegetariane"

                        },
                        new Category
                        {
                            Name = "Pizze di mare"

                        }
                    };
                    Categories.AddRange(seedtwo);
                    SaveChanges();

                }
            }

        }
    }
}


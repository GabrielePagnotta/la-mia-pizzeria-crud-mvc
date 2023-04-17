using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models
{
    public class DbPizzaContext : IdentityDbContext<IdentityUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pizzabase;Integrated Security=True;encrypt=false");
        }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient>? Ingredients { get; set; }


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

                    if (!Ingredients.Any())
                    {
                        var seedthree = new Ingredient[]
                        {

                           new Ingredient()
                           {
                               Ingredienti="Pomodoro Datterino"
                            },
                            new Ingredient()
                            {
                            Ingredienti = "Mozzarella"
                            },

                            new Ingredient
                             {
                            Ingredienti = "Mozzarella di Bufala"
                            },

                            new Ingredient
                            {
                            Ingredienti = "Pomodoro"
                            },

                            new Ingredient
                            {
                            Ingredienti = "Patatine fritte"
                            },

                            new Ingredient
                            {
                            Ingredienti = "Capperi"
                            },

                             new Ingredient
                            {
                            Ingredienti = "Tonno"
                            },

                             new Ingredient
                            {
                            Ingredienti = "Salmone"
                            },

                             new Ingredient
                            {
                            Ingredienti = "Rucola"
                            },

                             new Ingredient
                            {
                            Ingredienti = "Prosciutto"
                            },

                             new Ingredient
                            {
                            Ingredienti = "Salame"
                            },


                        };
                        Ingredients.AddRange(seedthree);



                    }

                }
            }

            //creazione dei ruoli utente
                if (!Roles.Any())
                {
                    var seedRoles = new IdentityRole[]
                    {
                                new("Admin"),
                                new("User")
                    };
                    Roles.AddRange(seedRoles);

                }

            //condizione per dare l'admin
            if (Users.Any(u => u.Email == "admin@dev.com" || u.Email == "gabriele.pagnotta1998@gmail.com")
               && !UserRoles.Any())
            {
                var admin = Users.First(u => u.Email == "admin@dev.com");
                var user = Users.First(u => u.Email == "gabriele.pagnotta1998@gmail.com");

                var adminRole = Roles.First(r => r.Name == "Admin");
                var userRole = Roles.First(r => r.Name == "User");

                var seed = new IdentityUserRole<string>[]
                {
                    new()
                    {
                        UserId = admin.Id,
                        RoleId = adminRole.Id
                    },
                    new()
                    {
                        UserId = user.Id,
                        RoleId = userRole.Id
                    }
                };

                UserRoles.AddRange(seed);
            }

                SaveChanges();
        }
    }
}


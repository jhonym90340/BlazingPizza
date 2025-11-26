using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
// Asegúrate de tener 'using BlazingPizza;' si tus modelos (Topping, PizzaSpecial) están allí.

namespace BlazingPizza.Server;

public static class SeedData
{
    // MÉTODO NUEVO Y COMPLETO (Async con Roles + Pizzas)
    public static async Task InitializeAsync(
        PizzaStoreContext db,
        UserManager<PizzaStoreUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        // ===============================================
        // 1. LÓGICA DE ROLES Y USUARIOS (Nueva)
        // ===============================================
        const string adminRole = "Administrators";

        // Crear el Rol si no existe
        if (await roleManager.FindByNameAsync(adminRole) is null)
        {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        // *** IMPORTANTE: CAMBIA ESTE CORREO por tu usuario administrador ***
        var adminEmail = "jhonymira90341@gmail.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        // Asignar el rol si el usuario existe y no lo tiene
        if (adminUser is not null && !await userManager.IsInRoleAsync(adminUser, adminRole))
        {
            await userManager.AddToRoleAsync(adminUser, adminRole);
        }

        // ===============================================
        // 2. LÓGICA DE TOPPINGS Y ESPECIALES (Movida de Initialize)
        // ===============================================

        var toppings = new Topping[]
        {
            // TUS DATOS DE TOPPINGS VAN AQUÍ
            new Topping() { Name = "Extra cheese", Price = 2.50m },
            new Topping() { Name = "American bacon", Price = 2.99m },
            new Topping() { Name = "British bacon", Price = 2.99m },
            new Topping() { Name = "Canadian bacon", Price = 2.99m },
            new Topping() { Name = "Tea and crumpets", Price = 5.00m },
            new Topping() { Name = "Fresh-baked scones", Price = 4.50m },
            new Topping() { Name = "Bell peppers", Price = 1.00m },
            new Topping() { Name = "Onions", Price = 1.00m },
            new Topping() { Name = "Mushrooms", Price = 1.00m },
            new Topping() { Name = "Pepperoni", Price = 1.00m },
            new Topping() { Name = "Duck sausage", Price = 3.20m },
            new Topping() { Name = "Venison meatballs", Price = 2.50m },
            new Topping() { Name = "Served on a silver platter", Price = 250.99m },
            new Topping() { Name = "Lobster on top", Price = 64.50m },
            new Topping() { Name = "Sturgeon caviar", Price = 101.75m },
            new Topping() { Name = "Artichoke hearts", Price = 3.40m },
            new Topping() { Name = "Fresh tomatoes", Price = 1.50m },
            new Topping() { Name = "Basil", Price = 1.50m },
            new Topping() { Name = "Steak (medium-rare)", Price = 8.50m },
            new Topping() { Name = "Blazing hot peppers", Price = 4.20m },
            new Topping() { Name = "Buffalo chicken", Price = 5.00m },
            new Topping() { Name = "Blue cheese", Price = 2.50m },
        };

        var specials = new PizzaSpecial[]
        {
            // TUS DATOS DE SPECIALS VAN AQUÍ
            new PizzaSpecial() { Name = "Basic Cheese Pizza", Description = "It's cheesy and delicious...", BasePrice = 9.99m, ImageUrl = "img/pizzas/cheese.jpg", Id = 1 },
            new PizzaSpecial() { Name = "The Baconatorizor", Description = "It has EVERY kind of bacon", BasePrice = 11.99m, ImageUrl = "img/pizzas/bacon.jpg", Id = 2 },




new PizzaSpecial()
{
        Id = 3,
        Name = "Classic pepperoni",
        Description = "It's the pizza you grew up with, but Blazing hot!",
        BasePrice = 10.50m,
        ImageUrl = "img/pizzas/pepperoni.jpg",
},
new PizzaSpecial()
{
        Id = 4,
        Name = "Buffalo chicken",
        Description = "Spicy chicken, hot sauce and bleu cheese, guaranteed to warm you up",
        BasePrice = 12.75m,
        ImageUrl = "img/pizzas/meaty.jpg",
},
new PizzaSpecial()
{
        Id = 5,
        Name = "Mushroom Lovers",
        Description = "It has mushrooms. Isn't that obvious?",
        BasePrice = 11.00m,
        ImageUrl = "img/pizzas/mushroom.jpg",
},
new PizzaSpecial()
{
        Id = 6,
        Name = "The Brit",
        Description = "When in London...",
        BasePrice = 10.25m,
        ImageUrl = "img/pizzas/brit.jpg",
},
new PizzaSpecial()
{
        Id = 7,
        Name = "Veggie Delight",
        Description = "It's like salad, but on a pizza",
        BasePrice = 11.50m,
        ImageUrl = "img/pizzas/salad.jpg",
},
new PizzaSpecial()
{
        Id = 8,
        Name = "Margherita",
        Description = "Traditional Italian pizza with tomatoes and basil",
        BasePrice = 9.99m,
        ImageUrl = "img/pizzas/margherita.jpg",
},
        };

        if (!db.Specials.Any())
        {
            db.Toppings.AddRange(toppings);
            db.Specials.AddRange(specials);
        }

        // Usar la versión asíncrona
        await db.SaveChangesAsync();
    }

    // MÉTODO SÍNCRONO ORIGINAL: Lo DEJAMOS VACÍO
    // (Ahora solo actuará como un marcador de posición, ya que el código en Program.cs 
    // lo reemplazaremos para llamar a InitializeAsync)
    public static void Initialize(PizzaStoreContext db) { }
}
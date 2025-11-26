using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingPizza; // Si los modelos están en Shared

namespace BlazingPizza.Server;

[Route("admin")]
[ApiController]
[Authorize(Roles = "Administrators")] // ¡Importante! Solo para administradores.
public class AdminController : Controller
{
    private readonly PizzaStoreContext _db;

    public AdminController(PizzaStoreContext db)
    {
        _db = db;
    }

    [HttpGet("specials")]
    public async Task<ActionResult<List<PizzaSpecial>>> GetSpecials()
    {
        var specials = await _db.Specials.OrderByDescending(s => s.BasePrice).ToListAsync();
        return specials;
    }

    // ... Aquí se pueden agregar más métodos (POST, PUT, DELETE) para gestionar pizzas, toppings, etc.
}

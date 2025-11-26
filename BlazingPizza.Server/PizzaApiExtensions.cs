using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Server;

public static class PizzaApiExtensions
{

    public static WebApplication MapPizzaApi(this WebApplication app)
    {

        // Subscribe to notifications
        app.MapPut("/notifications/subscribe", [Authorize] async (
            HttpContext context,
            PizzaStoreContext db,
            NotificationSubscription subscription) => {

                // We're storing at most one subscription per user, so delete old ones.
                // Alternatively, you could let the user register multiple subscriptions from different browsers/devices.
                var userId = GetUserId(context);
                if (userId is null)
                {
                    return Results.Unauthorized();
                }
                var oldSubscriptions = db.NotificationSubscriptions.Where(e => e.UserId == userId);
                db.NotificationSubscriptions.RemoveRange(oldSubscriptions);

                // Store new subscription
                subscription.UserId = userId;
                db.NotificationSubscriptions.Attach(subscription);

                await db.SaveChangesAsync();
                return Results.Ok(subscription);

            });

        // Specials
        app.MapGet("/specials", async (PizzaStoreContext db) => {

            var specials = await db.Specials.ToListAsync();
            return Results.Ok(specials);

        });

        // Toppings
        app.MapGet("/toppings", async (PizzaStoreContext db) => {
            var toppings = await db.Toppings.OrderBy(t => t.Name).ToListAsync();
            return Results.Ok(toppings);
        });


        // Endpoint de Administración para especiales. Requiere el rol "Administrators".
        app.MapGet("/specials/admin", [Authorize(Roles = "Administrators")] async (PizzaStoreContext db) =>
        {
            var specials = await db.Specials.ToListAsync();
            return Results.Ok(specials);
        });


        // Endpoint de CREACIÓN (POST) - Solo Administradores
        app.MapPost("/specials", [Authorize(Roles = "Administrators")] async (PizzaStoreContext db, PizzaSpecial special) =>
        {
            db.Specials.Add(special);
            await db.SaveChangesAsync();
            return Results.Ok(special.Id);
        });

        // Endpoint de ACTUALIZACIÓN (PUT) - Solo Administradores
        app.MapPut("/specials", [Authorize(Roles = "Administrators")] async (PizzaStoreContext db, PizzaSpecial special) =>
        {
            // Adjuntar la entidad y marcarla como modificada para que EF la actualice
            db.Attach(special).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Results.Ok();
        });

        return app;

    }

    public static string? GetUserId(HttpContext context) => context.User.FindFirstValue(ClaimTypes.NameIdentifier);

}
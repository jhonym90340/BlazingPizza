using BlazingPizza.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.AddContext<BlazingPizza.OrderContext>();
    });
builder.Services.AddRazorPages();

builder.Services.AddDbContext<PizzaStoreContext>(options =>
        options.UseSqlite("Data Source=pizza.db")
            .UseModel(BlazingPizza.Server.Models.PizzaStoreContextModel.Instance));

builder.Services.AddDefaultIdentity<PizzaStoreUser>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<PizzaStoreContext>();

// CÓDIGO CORREGIDO para Claims y IdentityServer:
builder.Services.AddIdentityServer()
    .AddApiAuthorization<PizzaStoreUser, PizzaStoreContext>(options => {
        // 1. Configura el ProfileService para incluir Claims
        options.IdentityResources["openid"].UserClaims.Add("role");
        options.ApiResources.Single().UserClaims.Add("role");
    });

// 2. Mapea el Claim de Rol
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");

builder.Services.AddAuthentication()
        .AddIdentityServerJwt();

var app = builder.Build();

// INICIALIZACIÓN DE LA BASE DE DATOS (COMENTADO COMPLETAMENTE para usar SQLITE y evitar el Error 500.30)
/*
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var db = serviceProvider.GetRequiredService<PizzaStoreContext>();

    var userManager = serviceProvider.GetRequiredService<UserManager<PizzaStoreUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Comentado para evitar el EnsureCreated y la inicialización.
    // if (db.Database.EnsureCreated())
    // {
    //     await SeedData.InitializeAsync(db, userManager, roleManager);
    // } 
    // await SeedData.InitializeAsync(db, userManager, roleManager); // Esta línea también debe estar comentada
}
*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapPizzaApi();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
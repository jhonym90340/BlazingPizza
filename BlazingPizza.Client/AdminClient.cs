using System.Net.Http.Json;
using BlazingPizza; // Para PizzaSpecial

namespace BlazingPizza.Client;

public class AdminClient
{
    private readonly HttpClient httpClient;

    public AdminClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    /// <summary>
    /// Obtiene todas las pizzas especiales. Requiere rol de Administrador.
    /// </summary>
    public async Task<List<PizzaSpecial>?> GetSpecials()
    {
        // La ruta es 'admin/specials'
        return await httpClient.GetFromJsonAsync<List<PizzaSpecial>>("admin/specials");
    }

    public async Task<List<PizzaSpecial>> GetSpecialsAsync() =>
        await httpClient.GetFromJsonAsync("specials/admin", OrderContext.Default.ListPizzaSpecial) ?? new();


    // BlazingPizza.Client/AdminClient.cs

    // ... (después de GetSpecialsAsync)

    /// <summary>
    /// Crea un nuevo especial de pizza (POST).
    /// </summary>
    public async Task<int> CreateSpecial(PizzaSpecial special)
    {
        var response = await httpClient.PostAsJsonAsync("/specials", special);
        response.EnsureSuccessStatusCode();

        // El servidor devuelve el ID del nuevo especial
        return await response.Content.ReadFromJsonAsync<int>();
    }

    /// <summary>
    /// Actualiza un especial de pizza existente (PUT).
    /// </summary>
    public async Task UpdateSpecial(PizzaSpecial special)
    {
        var response = await httpClient.PutAsJsonAsync("/specials", special);
        response.EnsureSuccessStatusCode();
    }



}
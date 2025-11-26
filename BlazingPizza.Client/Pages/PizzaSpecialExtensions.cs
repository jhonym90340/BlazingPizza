using BlazingPizza;
using System.Text.Json;

namespace BlazingPizza.Client.Extensions
{
    public static class PizzaSpecialExtensions
    {
        // Método de extensión que añade Clone() a la clase PizzaSpecial
        public static PizzaSpecial Clone(this PizzaSpecial special)
        {
            // Serializa el objeto a JSON
            var json = JsonSerializer.Serialize(special);

            // Deserializa el JSON a un nuevo objeto PizzaSpecial
            return JsonSerializer.Deserialize<PizzaSpecial>(json) ?? throw new InvalidOperationException("Failed to clone PizzaSpecial.");
        }
    }
}

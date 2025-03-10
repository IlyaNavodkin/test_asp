using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        Thread.Sleep(5000);

        string apiUrl = "http://localhost:5013/WeatherForecast";

        // GET-запрос
        var forecasts = await client.GetFromJsonAsync<WeatherForecast[]>(apiUrl);
        Console.WriteLine("Полученные прогнозы погоды:");
        foreach (var forecast in forecasts)
        {
            Console.WriteLine($"{forecast.Date}: {forecast.TemperatureC}°C, {forecast.Summary}");
        }

        // POST-запросSystem.Net.Http.HttpRequestException: 'An error occurred while sending the request.'

        var newForecast = new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            TemperatureC = 25,
            Summary = "Sunny"
        };

        var response = await client.PostAsJsonAsync(apiUrl, newForecast);
        var createdForecast = await response.Content.ReadFromJsonAsync<WeatherForecast>();

        Console.WriteLine("\nСоздан новый прогноз:");
        Console.WriteLine($"{createdForecast.Date}: {createdForecast.TemperatureC}°C, {createdForecast.Summary}");
    }
}

public class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }
}

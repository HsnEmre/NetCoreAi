using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var apikey = "your api key";

        Console.WriteLine("this write a questions");


        var promt = Console.ReadLine();

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apikey}");

        var requestbody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new {role="system",content="You are a helpful asistant"},
                new {role="user",content=promt},
            },
            max_tokens = 500
        };

        var json = JsonSerializer.Serialize(requestbody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");


        try
        {
            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<JsonElement>(responseString);
                var answer = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();


                Console.WriteLine("Ai answer:");
                Console.WriteLine(answer);
            }
            else
            {
                Console.WriteLine($"an error occured {response.StatusCode}");
                Console.WriteLine(responseString);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"an error occured {ex.Message}");
        }
    }
    
}
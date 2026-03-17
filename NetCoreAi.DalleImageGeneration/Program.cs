using Newtonsoft.Json.Linq;
using System.Text;

class Program
{
    public static async Task Main(string[] args)
    {
        string apiKey = "your api key";
        Console.Write("Prompt gir: ");
        string prompt = Console.ReadLine();

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

        var jsonBody = """
        {
          "model": "gpt-image-1",
          "prompt": "Kırmızı arka planlı cyberpunk şehir",
          "size": "1024x1024",
          "quality": "medium",
          "output_format": "png"
        }
        """.Replace("draw a people in the rainy day and background red cyberpunk city", prompt);

        using var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://api.openai.com/v1/images/generations", content);
        var responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Hata:");
            Console.WriteLine(responseString);
            return;
        }

        var json = JObject.Parse(responseString);

        // gpt-image-1 için çoğunlukla burada olur
        var b64 = json["data"]?[0]?["b64_json"]?.ToString();

        if (string.IsNullOrEmpty(b64))
        {
            Console.WriteLine("b64_json bulunamadı. Dönen cevap:");
            Console.WriteLine(responseString);
            return;
        }

        byte[] imageBytes = Convert.FromBase64String(b64);
        string filePath = Path.Combine(Environment.CurrentDirectory, "output.png");
        await File.WriteAllBytesAsync(filePath, imageBytes);

        Console.WriteLine($"picture saved: {filePath}");
    }
}
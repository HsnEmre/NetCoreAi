using System.Net.Http.Headers;

class Program
{
    static async Task Main(string[] args)
    {
        
        string apiKey = "your api key";

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            Console.WriteLine("OPENAI_API_KEY not found");
            return;
        }

        string audioFilePath = @"C:\Users\Raven\Desktop\Project\NetCoreAi\NetCoreAi\NetCoreAi.WhispeSpeechToText\Wi-Fi gun DY Most powerful Antenna up to 2Km (fabrication locale).mp4";

        if (!File.Exists(audioFilePath))
        {
            Console.WriteLine("Dosya not  found.");
            return;
        }

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);

        using var form = new MultipartFormDataContent();
        await using var fileStream = File.OpenRead(audioFilePath);
        using var fileContent = new StreamContent(fileStream);

        fileContent.Headers.ContentType = new MediaTypeHeaderValue("video/mp4");

        form.Add(fileContent, "file", Path.GetFileName(audioFilePath));
        form.Add(new StringContent("whisper-1"), "model");

        Console.WriteLine("Audio file is being processed...");

        try
        {
            var response = await client.PostAsync(
                "https://api.openai.com/v1/audio/transcriptions",
                form);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Transcript:");
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                Console.WriteLine(result);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception:");
            Console.WriteLine(ex.Message);
        }
    }
}
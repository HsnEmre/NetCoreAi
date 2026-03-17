using System.Text.Json;
using Google.Cloud.Vision.V1;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("enter image path: ");
        string imagePath = Console.ReadLine();

        string credentialPath = @"C:\Users\Raven\Desktop\Project\mycloudproject-490511-c19c80c32e10.json";

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

        var json = File.ReadAllText(credentialPath);
        using var doc = JsonDocument.Parse(json);

        Console.WriteLine("project_id: " + doc.RootElement.GetProperty("project_id").GetString());
        Console.WriteLine("client_email: " + doc.RootElement.GetProperty("client_email").GetString());
        Console.WriteLine();

        try
        {
            var client = ImageAnnotatorClient.Create();
            var image = Image.FromFile(imagePath);
            var response = client.DetectText(image);

            Console.WriteLine("text in image:");
            foreach (var a in response)
            {
                if (!string.IsNullOrWhiteSpace(a.Description))
                    Console.WriteLine(a.Description);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("error: " + ex.Message);
        }
    }
}
using Google.Cloud.Vision.V1;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("enter image path");

        Console.WriteLine();

        string imagePath = Console.ReadLine();

        string credentialPath = "this get services json file";

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

        try
        {
            var client = ImageAnnotatorClient.Create();

            var image = Image.FromFile(imagePath);
            var response = client.DetectText(image);
            Console.WriteLine("text in the image");
            Console.WriteLine();
            foreach (var anotation in response)
            {
                if (!string.IsNullOrEmpty(anotation.Description))
                {
                    Console.WriteLine($"{anotation.Description}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"an  error occured {ex.Message}");
        }
    }
}
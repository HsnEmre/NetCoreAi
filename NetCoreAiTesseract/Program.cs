using Tesseract;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("the image path for charachter reading");
        string imagepath = Console.ReadLine();

        string tessDataPath = @"C:\tessdata";

        try
        {
            using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagepath))
                {
                    using (var page = engine.Process(img))
                    {
                        string text = page.GetText();
                        Console.WriteLine("text read from the image ");
                        Console.WriteLine(text);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"bir hata olustu :{ex.Message}");
        }
        Console.ReadLine();
    }
}
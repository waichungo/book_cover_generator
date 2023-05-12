

using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using VersOne.Epub;

Program.Start();

public partial class Program
{
    public static void Start()
    {
        var args = Environment.GetCommandLineArgs();
        if (args.Length > 1)
        {
            var folderToParseBooks = args[0];
            if (Directory.Exists(folderToParseBooks))
            {
                var targetThumbFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "thumbnails");
                if (!Directory.Exists(targetThumbFolder))
                {
                    Directory.CreateDirectory(targetThumbFolder);
                }
                var files = Directory.GetFiles(folderToParseBooks, "*", new EnumerationOptions
                {
                    IgnoreInaccessible = true
                }).Where(e => e.ToLower().EndsWith(".pdf") || e.ToLower().EndsWith(".epub")).ToList();

                foreach (var file in files)
                {
                    try
                    {
                        var isPdf = file.ToLower().EndsWith(".pdf");
                        var targetImage = Path.Combine(targetThumbFolder, Path.GetFileNameWithoutExtension(file) + ".jpg");
                        if (isPdf)
                        {
                            using (PdfDocument document = PdfDocument.Open(file))
                            {
                                var pages = document.GetPages();
                                if (pages.Count() > 5)
                                {
                                    pages = pages.Take(5);
                                }
                                foreach (Page page in pages)
                                {
                                    var images = page.GetImages();
                                    if (images.Count() > 0)
                                    {
                                        var image = images.OrderByDescending(e => e.WidthInSamples).First();
                                        File.WriteAllBytes(targetImage, image.RawBytes.ToArray());
                                    }
                                }
                            }
                        }
                        else
                        {
                            var epub = EpubReader.ReadBook(file);
                            var cover = epub.CoverImage;
                            if (cover != null && cover.Length > 0)
                            {
                                File.WriteAllBytes(targetImage, cover);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else
            {
                Console.WriteLine($"'{folderToParseBooks}' does not exist or it is an invalid folder");
            }
        }
    }
}
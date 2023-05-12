

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
                    var isPdf = file.ToLower().EndsWith(".pdf");
                    if (isPdf)
                    {
                        
                    }
                    else
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
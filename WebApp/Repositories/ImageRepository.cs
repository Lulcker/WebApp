namespace WebApp.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private IWebHostEnvironment _environment;
        private readonly string _directoryName = "images";

        public string DirectoryImage { get; }

        public ImageRepository(IWebHostEnvironment environment)
        {
            _environment = environment;
            DirectoryImage = Path.Combine(_environment.WebRootPath, _directoryName);
            if (!Directory.Exists(DirectoryImage))
                Directory.CreateDirectory(DirectoryImage);
        }

        public async Task<string> SaveImageAsync(string extension, Stream imageStream)
        {
            string fileName = GenerateFileName(extension);
            string filePath = Path.Combine(DirectoryImage, fileName);
            imageStream.Position = 0;
            using (var file = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                await imageStream.CopyToAsync(file);
            }
            return $"~/{_directoryName}/{fileName}";
        }

        public void DeleteImage(string fileName)
        {
            string filePath = Path.Combine(DirectoryImage, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        private string GenerateFileName(string extension)
        {
            return string.Format($"{Guid.NewGuid()}{extension}");
        }


    }
}

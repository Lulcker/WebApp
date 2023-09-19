namespace WebApp.Repositories
{
    public interface IImageRepository
    {
        string DirectoryImage { get; }

        Task<string> SaveImageAsync(string extension, Stream imageStream);

        void DeleteImage(string fileName);
    }
}

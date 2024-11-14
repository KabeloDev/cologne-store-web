namespace CologneStore.ImageService
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void DeleteFile(string fileName)
        {
            var wwwPath = _webHostEnvironment.WebRootPath;
            var fileNameWithPath = Path.Combine(wwwPath, "images\\", fileName);
            if (!File.Exists(fileNameWithPath))
                throw new FileNotFoundException(fileName);
            File.Delete(fileNameWithPath);
        }

        public async Task<string> SaveFile(IFormFile file, string[] allowedExtensions)
        {
            var wwwPath = _webHostEnvironment.WebRootPath;
            var path = Path.Combine(wwwPath, "images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException($"Only {string.Join(",", allowedExtensions)} files are allowed");
            }

            string fileName = $"{Guid.NewGuid()}{extension}";
            string fileNameWithPath = Path.Combine(path, fileName);
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await file.CopyToAsync(stream);
            return fileName;
        }
    }
}

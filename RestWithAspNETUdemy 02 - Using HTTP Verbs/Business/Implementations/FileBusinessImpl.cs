using System.IO;

namespace RestWithAspNETUdemy.Business.Implementations
{
    public class FileBusinessImpl : IFileBusiness
    {
        public byte[] GetPDF()
        {
            string path = Directory.GetCurrentDirectory();
            string fullPath = $"{path}/other/ENERGY_STAR.pdf";

            return File.ReadAllBytes(fullPath);
        }
    }
}

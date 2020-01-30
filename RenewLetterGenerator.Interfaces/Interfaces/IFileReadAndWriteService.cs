using RenewalLetterGenerator.Models;
using System.Text;

namespace RenewalLetterGenerator.Interfaces
{
    /// <summary>
    /// Flie interface to write and read data in files
    /// </summary>
    public interface IFileReadAndWriteService
    {
        StringBuilder ReadFromTemplate();

        bool CreateFile(ViewModel viewModel, string file);
    }
}
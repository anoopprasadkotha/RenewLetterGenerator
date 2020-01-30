using RenewalLetterGenerator.Models;
using System.Text;

namespace RenewalLetterGenerator.Interfaces
{
    public interface IFileReadAndWriteService
    {
        StringBuilder ReadFromTemplate();

        bool CreateFile(ViewModel viewModel, string file);
    }
}
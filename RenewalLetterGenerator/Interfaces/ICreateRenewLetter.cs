using RenewalLetterGenerator.Models;

namespace RenewalLetterGenerator.Interfaces
{
    public interface ICreateRenewLetter
    {
        bool CreateRenewLetter(ViewModel viewModel);
    }
}

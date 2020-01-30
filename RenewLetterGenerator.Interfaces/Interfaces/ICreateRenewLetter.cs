using RenewalLetterGenerator.Models;

namespace RenewalLetterGenerator.Interfaces
{
    /// <summary>
    /// interface to Create a Renewal Letter for each member
    /// </summary>
    public interface ICreateRenewLetter
    {
        bool CreateRenewLetter(ViewModel viewModel);
    }
}

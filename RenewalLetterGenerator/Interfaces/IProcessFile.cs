using RenewalLetterGenerator.Models;
using System.Collections.Generic;
using System.Web;

namespace RenewalLetterGenerator.Interfaces
{
    /// <summary>
    /// Main Interface Which take cares of all the processing of files
    /// </summary>
    public interface IProcessFile
    {
        List<ViewModel> ReadCsvFile(HttpPostedFileBase uploadFile);

        bool GenerateRenewalLetters(List<ViewModel> viewModel);
    }
}

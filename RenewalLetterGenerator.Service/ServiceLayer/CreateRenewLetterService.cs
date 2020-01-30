using RenewalLetterGenerator.Helper;
using RenewalLetterGenerator.Interfaces;
using RenewalLetterGenerator.Models;

namespace RenewalLetterGenerator.Service.ServiceLayer
{
    public class CreateRenewLetterFacadeService : ICreateRenewLetter
    {
        private IFileReadAndWriteService _fileCreationService ;

      
        public CreateRenewLetterFacadeService(IFileReadAndWriteService fileCreationService)
        {
            _fileCreationService = fileCreationService;
        }

        /// <summary>
        /// Calls all subsystem to create the renew letter
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>Bool flag , with file generation</returns>
        public bool CreateRenewLetter(ViewModel viewModel)
        {
            // Read the Letter text rom the Content Folder

            var letterStream = _fileCreationService.ReadFromTemplate();

            //formating the letter for individual Member
            var formatedLetter = GenericHelper.FormattingFile(viewModel, letterStream);

            //Store te file in location
            bool IsSuccess= _fileCreationService.CreateFile(viewModel, formatedLetter.ToString());

            return IsSuccess;
        }
    }
}
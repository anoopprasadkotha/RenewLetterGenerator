using RenewalLetterGenerator.Interfaces;
using RenewalLetterGenerator.Service.ModelBinder;
using RenewalLetterGenerator.Models;
using System.Collections.Generic;
using System.Web;

namespace RenewalLetterGenerator.Service.ServiceLayer
{
    public class ProcessFileFacadeService : IProcessFile
    {

        private readonly ICSVReader _cSVReader;
        private readonly ICreateRenewLetter  _createRenewLetter ;
        private readonly ILogger _logger;

        public ProcessFileFacadeService(ICSVReader cSVReader, ICreateRenewLetter createRenewLetter,ILogger logger)
        {
            _cSVReader = cSVReader;
            _createRenewLetter = createRenewLetter;
            _logger = logger;
        }

        public bool GenerateRenewalLetters(List<ViewModel> viewModel)
        {
            bool isSuccess=false;
            foreach (var Member in viewModel)
            {
                isSuccess = _createRenewLetter.CreateRenewLetter(Member);

                Member.MemDetails.IsGeneratedNow = isSuccess;
            }
            return isSuccess;

        }

        public List<ViewModel> ReadCsvFile(HttpPostedFileBase uploadFile)
        {
            //reading a excel file into data table
            var dataTable = _cSVReader.ExcelToDataTable(uploadFile);

            //validate and convert into list of valid enteries

            var IndividualMember = new ViewModel();

            var listofValidMembers = new List<ViewModel>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                //used to bind the values and if the row is corrupted  
                IndividualMember = ViewModelBinder.Bind(dataTable, i,_logger);

                if ((IndividualMember != null))
                {
                    listofValidMembers.Add(IndividualMember);
                }

            }

            return listofValidMembers;
        }
    }
}
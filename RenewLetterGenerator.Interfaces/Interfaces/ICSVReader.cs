using System.Data;
using System.Web;

namespace RenewalLetterGenerator.Interfaces
{
    /// <summary>
    /// To import data from Excel or .Csv file to a DataTable
    /// </summary>
    public interface ICSVReader
    {
        DataTable ExcelToDataTable(HttpPostedFileBase upload);

    }
}

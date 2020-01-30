using System.Data;
using System.Web;

namespace RenewalLetterGenerator.Interfaces
{
    public interface ICSVReader
    {
        DataTable ExcelToDataTable(HttpPostedFileBase upload);

    }
}

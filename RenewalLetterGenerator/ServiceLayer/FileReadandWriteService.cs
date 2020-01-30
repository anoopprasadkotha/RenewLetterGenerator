using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using RenewalLetterGenerator.Helper;
using RenewalLetterGenerator.Interfaces;
using RenewalLetterGenerator.Models;

namespace RenewalLetterGenerator.ServiceLayer
{
    public class FileReadandWriteService : IFileReadAndWriteService
    {
        public static string destinationFilePath = GenericHelper.GetValueFromWebConfig("Destination_File_Path");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool CreateFile(ViewModel viewModel, string file)
        {
            bool createdNow=false;

            // C: \Users\Anoop Prasad Kotha\source\repos\CodingTest\CodingTest\Models\CustomerDetails.cs
            string path = destinationFilePath + viewModel.MemDetails.Id + viewModel.MemDetails.FirstName+viewModel.MemDetails.SurName + ".txt";

            //Create the file only if it doesnt exists.
            if (!System.IO.File.Exists(path))
            {
                createdNow = true;
                //System.IO.File.Delete(path);
                using (FileStream fs = System.IO.File.Create(path))
                {
                    AddText(fs, file);
                }
            }
            return createdNow;
        }


        public StringBuilder ReadFromTemplate()
        {
            var path = @"~/Content/templateDoc.txt";
            StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(path));
            string streamData = sr.ReadToEnd();
            sr.Close();
            return new StringBuilder(streamData);
        }

        #region Private Methods

        private void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
        #endregion
    }
}
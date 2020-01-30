using System.IO;
using System.Net.Http;
using System.Text;
using RenewalLetterGenerator.Interfaces;
using RenewalLetterGenerator.Models;

namespace RenewalLetterGenerator.Service.ServiceLayer
{
    public class FileReadandWriteService : IFileReadAndWriteService
    {

        private const string destinationFilePath = @"D:\";
        private const string templatePath= @"\Content\templateDoc.txt";

        /// <summary>
        /// Create the renewal letter based on the ViewModel and template file passed
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="file"></param>
        /// <returns>Wether new file was generated or not </returns>
        public bool CreateFile(ViewModel viewModel, string file)
        {
            bool createdNow=false;

            //Creating file  path ,file name by concatinating Id,FirstName and SurName
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

        /// <summary>
        /// Readind the value from Template in the solution
        /// </summary>
        /// <returns>Template as a string</returns>
        public StringBuilder ReadFromTemplate()
        {
            //Getting path to read the template
            var appDomain = System.AppDomain.CurrentDomain;
            var path =appDomain.BaseDirectory+templatePath;

            //Reading the template from the Content folder
            StreamReader sr = new StreamReader(path);
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
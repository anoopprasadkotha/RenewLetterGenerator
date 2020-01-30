using RenewalLetterGenerator.Interfaces;
using System.IO;

namespace RenewalLetterGenerator.Service.ServiceLayer
{
    public class LoggerService:ILogger
    {
        //Declaring the logger file Path as D directory
        private const string filePath = @"D:/InvalidData";

        public static object ConfigurationManager { get; private set; }

        public void Log(string message)
        {
            System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
            using (StreamWriter streamWriter = new StreamWriter(fs))
            {
                streamWriter.BaseStream.Seek(0, System.IO.SeekOrigin.End);
                streamWriter.WriteLine(message);
                streamWriter.WriteLine("-------------------------------------------------------------------------------------------------------------");
                streamWriter.Close();
            }
        }
    }
}

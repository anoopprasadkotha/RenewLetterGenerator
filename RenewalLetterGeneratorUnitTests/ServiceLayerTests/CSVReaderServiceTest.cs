using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RenewalLetterGenerator.Service.ServiceLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RenewalLetterGeneratorUnitTests.ServiceLayerTests
{
    [TestClass]
    public class CSVReaderServiceTest
    {
        private readonly CSVReaderService cSVReaderService;

        public CSVReaderServiceTest()
        {
            cSVReaderService = new CSVReaderService();
        }

        [TestMethod]
        public void ConverExcelToTableMethd_ForProperFormat()
        {
            string filePath = @"E:\sample\Customer.csv";
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Mock<FileStream> stream = new Mock<FileStream>();
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            uploadedFile.Setup(x => x.FileName).Returns("Customer.csv");
            uploadedFile.Setup(x => x.ContentType).Returns("text");
            uploadedFile.Setup(x => x.InputStream).Returns(fileStream);
            uploadedFile.Setup(x => x.ContentLength).Returns(5);
            HttpPostedFileBase httpPostedFileBases = uploadedFile.Object;
            //Act.
            var result = cSVReaderService.ExcelToDataTable(httpPostedFileBases);

            //Assert.
            Assert.IsInstanceOfType(result, typeof(System.Data.DataTable));
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ConverExcelToTableMethd_throwsException_incorrectFormat()
        {
            string filePath = @"E:\sample\Dserver_logotransparent.jpeg";
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Mock<FileStream> stream = new Mock<FileStream>();
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            uploadedFile.Setup(x => x.FileName).Returns("Dserver_logotransparent.jpeg");
            uploadedFile.Setup(x => x.ContentType).Returns("image/png");
            uploadedFile.Setup(x => x.InputStream).Returns(fileStream);
            uploadedFile.Setup(x => x.ContentLength).Returns(5);
            HttpPostedFileBase httpPostedFileBases = uploadedFile.Object;
            //Act.
            var result = cSVReaderService.ExcelToDataTable(httpPostedFileBases);

            //Assert.
            //Throws Excepton.
        }
    }
}

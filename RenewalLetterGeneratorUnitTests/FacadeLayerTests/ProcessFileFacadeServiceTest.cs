using System.Data;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RenewalLetterGenerator.Interfaces;
using RenewalLetterGenerator.Models;
using RenewalLetterGenerator.Service.ServiceLayer;
using System.Collections.Generic;
using System.IO;
using System;

namespace RenewalLetterGeneratorUnitTests.FacadeLayerTests
{
    [TestClass]
    public class ProcessFileFacadeServiceTest
    {
        private readonly Mock<ICSVReader> _cSVReader;
        private readonly Mock<ICreateRenewLetter> _createRenewLetter;
        private readonly Mock<ILogger> _logger;
        private readonly ProcessFileFacadeService processFileFacadeService;

        public ProcessFileFacadeServiceTest()
        {
            _cSVReader = new Mock<ICSVReader>();
            _createRenewLetter = new Mock<ICreateRenewLetter>();
            _logger = new Mock<ILogger>();
            processFileFacadeService = new ProcessFileFacadeService(_cSVReader.Object, _createRenewLetter.Object, _logger.Object);
        }

        [TestMethod]
        public void GenerateRenewalLetter_Returns_True()
        {
            _createRenewLetter.Setup(x => x.CreateRenewLetter(It.IsAny<ViewModel>())).Returns(true);
           var result = processFileFacadeService.GenerateRenewalLetters(new List<ViewModel>()
           {
               new ViewModel
               {
                   MemDetails = new MemberDetailsModel
                   {
                       AnnualPremium = 0.55,
                       FirstName = "Chaitra",
                       Id = 1234
                   }
               }
           });
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GenerateRenewalLetter_Returns_False()
        {
            //Arrange.
            _createRenewLetter.Setup(x => x.CreateRenewLetter(It.IsAny<ViewModel>())).Returns(false);

            //Act.
            var result = processFileFacadeService.GenerateRenewalLetters(new List<ViewModel>()
           {
               new ViewModel
               {
                   MemDetails = new MemberDetailsModel
                   {
                       AnnualPremium = 0.55,
                       FirstName = "Chaitra",
                       Id = 1234
                   }
               }
           });

            //Assert.
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReadCSVFile_Returns_ListOfMembers()
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

            DataTable table = new DataTable("MyTable");
            DataColumn idColumn = new DataColumn("id", typeof(int));
            DataColumn amountColumn = new DataColumn("amount", typeof(string));
            DataColumn dateColumn = new DataColumn("date", typeof(string));
            DataColumn ProductName = new DataColumn("ProductName", typeof(string));
            DataColumn Surname = new DataColumn("Surname", typeof(string));
            DataColumn PayoutAmount = new DataColumn("PayoutAmount", typeof(double));
            DataColumn AnnualPremium = new DataColumn("AnnualPremium", typeof(double));
            table.Columns.Add(idColumn);
            table.Columns.Add(amountColumn);
            table.Columns.Add(dateColumn);
            table.Columns.Add(ProductName);
            table.Columns.Add(Surname);
            table.Columns.Add(PayoutAmount);
            table.Columns.Add(AnnualPremium);

            DataRow newRow = table.NewRow();
            newRow["id"] = 1;
            newRow["amount"] = "10.3m";
            newRow["date"] = "dfsdf";
            newRow["ProductName"] = "dfsdf";
            newRow["Surname"] = "dfsdf";
            newRow["PayoutAmount"] = 0.123;
            newRow["AnnualPremium"] = 0.123;
            table.Rows.Add(newRow);

            newRow = table.NewRow();
            newRow["id"] = 1;
            newRow["amount"] = "10.3m";
            newRow["date"] = "dfsdf";
            newRow["ProductName"] = "dfsdf";
            newRow["Surname"] = "dfsdf";
            newRow["PayoutAmount"] = 0.123;
            newRow["AnnualPremium"] = 0.123;
            table.Rows.Add(newRow);

            newRow = table.NewRow();
            newRow["id"] = 1;
            newRow["amount"] = "10.3m";
            newRow["date"] = "dfsdf";
            newRow["ProductName"] = "dfsdf";
            newRow["Surname"] = "dfsdf";
            newRow["PayoutAmount"] = 0.123;
            newRow["AnnualPremium"] = 0.123;
            table.Rows.Add(newRow);

            _cSVReader.Setup(x => x.ExcelToDataTable(It.IsAny<HttpPostedFileBase>())).Returns(table);
            var result = processFileFacadeService.ReadCsvFile(httpPostedFileBases);

            Assert.IsNotNull(result);
        }
    }
}

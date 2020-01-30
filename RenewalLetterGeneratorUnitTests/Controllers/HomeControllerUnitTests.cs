using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RenewalLetterGenerator.Controllers;
using RenewalLetterGenerator.Interfaces;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Assert = NUnit.Framework.Assert;

namespace RenewalLetterGeneratorUnitTests.Controllers
{
    [TestClass]
    public class HomeControllerUnitTests
    {
        private readonly HomeController _homeController;
        private readonly Mock<IProcessFile> _processFile;
        public HomeControllerUnitTests()
        {
            _processFile = new Mock<IProcessFile>();
            _homeController = new HomeController(_processFile.Object);

        }
        [TestMethod]
        public void Upload_HasNoReturnView()
        {

            //act
            var result = _homeController.Upload() as System.Web.Mvc.ViewResult;

            //assert
            Assert.AreEqual("", result.ViewName);

        }

        [TestMethod]
        public void Upload_Filetype_NotCSv()
        {
            //Arrange
            string filePath = @"D:Dserver_logotransparent.jpeg";
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Mock<FileStream> stream = new Mock<FileStream>();
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            uploadedFile.Setup(x => x.FileName).Returns("Dserver_logotransparent.jpeg");
            uploadedFile.Setup(x => x.ContentType).Returns("image/png");
            uploadedFile.Setup(x => x.InputStream).Returns(fileStream);
            uploadedFile.Setup(x => x.ContentLength).Returns(5);
            HttpPostedFileBase httpPostedFileBases = uploadedFile.Object;


            //Act
            var result = _homeController.Upload(httpPostedFileBases) as ViewResult;
           

            //Assert
            var modelState = _homeController.ModelState;

            Assert.AreEqual(1, modelState.Keys.Count);

            Assert.IsTrue(modelState.Keys.Contains("File"));
            Assert.IsTrue(modelState["File"].Errors.Count == 1);
            Assert.AreEqual("This file format is not supported", modelState["File"].Errors[0].ErrorMessage);

        }
        [TestMethod]
        public void Upload_Filetype_CSvWithNoContent()
        {
            //Arrange
            string filePath = @"D:Customer.csv";
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            uploadedFile.Setup(x => x.FileName).Returns("customer.csv");
            uploadedFile.Setup(x => x.ContentType).Returns("csv");
            uploadedFile.Setup(x => x.InputStream).Returns(fileStream);
            uploadedFile.Setup(x => x.ContentLength).Returns(0);
            // uploadedFile.Setup(x => x.InputStream).Returns(new Stream =" ");
            HttpPostedFileBase httpPostedFileBases = uploadedFile.Object;

            //Act
            var result = _homeController.Upload(httpPostedFileBases) as ViewResult;
            var modelState = _homeController.ModelState;

            //Assert

            Assert.AreEqual(1, modelState.Keys.Count);

            Assert.IsTrue(modelState.Keys.Contains("File"));
            Assert.IsTrue(modelState["File"].Errors.Count == 1);
            Assert.AreEqual("Please Upload Your file", modelState["File"].Errors[0].ErrorMessage);

        }

    }
}

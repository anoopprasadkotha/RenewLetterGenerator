using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RenewalLetterGenerator.Interfaces;
using RenewalLetterGenerator.Models;
using RenewalLetterGenerator.Service.ServiceLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewalLetterGeneratorUnitTests.ServiceLayerTests
{
    [TestClass]
    public class CreateRenewLetterService
    {
        private readonly Mock<IFileReadAndWriteService> _fileCreationService;
        private readonly CreateRenewLetterFacadeService createRenewLetterFacadeService;

        public CreateRenewLetterService()
        {
            _fileCreationService = new Mock<IFileReadAndWriteService>();
            createRenewLetterFacadeService = new CreateRenewLetterFacadeService(_fileCreationService.Object);
        }


        [TestMethod]
        public void CreateRenewLetter_Returns_True_when_file_isGenerated_Now()
        {
            var templatePath = @"\Content\templateDoc.txt";
            var appDomain = System.AppDomain.CurrentDomain;
            var path = appDomain.BaseDirectory + templatePath;
            StreamReader sr = new StreamReader(path);
            string streamData = sr.ReadToEnd();
            sr.Close();

            var viewModel = new ViewModel
            {
                MemDetails = new MemberDetailsModel
                {
                    Id = 1,
                    FirstName = "sdfsd",
                    PayoutAmount = 123,
                    SurName = "adsfsdf",
                    Title = "Miss",
                    ProductName = "sfds",
                    AnnualPremium = 0.12
                },
                RenewalDetails = new RenewalModel
                {
                    CreditCharge = "qfs",
                    AverageMonthlyPremium = "adsfdsd",
                    IntialMonthlyPremium = 123.33,
                    RemainingMonthlyPremium = "12",
                    TotalPremium = "12"
                }
            };
            _fileCreationService.Setup(x => x.ReadFromTemplate()).Returns(new StringBuilder(streamData));
            _fileCreationService.Setup(x => x.CreateFile(It.IsAny<ViewModel>(), It.IsAny<string>())).Returns(true);
            //Act.
            var result = createRenewLetterFacadeService.CreateRenewLetter(viewModel);

            //Assert.
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateRenewLetter_Returns_False_when_file_isAlreadyGenerated()
        {
            var templatePath = @"\Content\templateDoc.txt";
            var appDomain = System.AppDomain.CurrentDomain;
            var path = appDomain.BaseDirectory + templatePath;
            StreamReader sr = new StreamReader(path);
            string streamData = sr.ReadToEnd();
            sr.Close();

            var viewModel = new ViewModel
            {
                MemDetails = new MemberDetailsModel
                {
                    Id = 1,
                    FirstName = "sdfsd",
                    PayoutAmount = 123,
                    SurName = "adsfsdf",
                    Title = "Miss",
                    ProductName = "sfds",
                    AnnualPremium = 0.12
                },
                RenewalDetails = new RenewalModel
                {
                    CreditCharge = "qfs",
                    AverageMonthlyPremium = "adsfdsd",
                    IntialMonthlyPremium = 123.33,
                    RemainingMonthlyPremium = "12",
                    TotalPremium = "12"
                }
            };
            _fileCreationService.Setup(x => x.ReadFromTemplate()).Returns(new StringBuilder(streamData));
            _fileCreationService.Setup(x => x.CreateFile(It.IsAny<ViewModel>(), It.IsAny<string>())).Returns(false);
            //Act.
            var result = createRenewLetterFacadeService.CreateRenewLetter(viewModel);

            //Assert.
            Assert.IsTrue(result);
        }
    }
}

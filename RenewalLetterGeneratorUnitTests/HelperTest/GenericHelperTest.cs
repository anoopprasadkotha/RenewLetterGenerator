using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenewalLetterGenerator.Helper;
using RenewalLetterGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewalLetterGeneratorUnitTests.HelperTest
{
    [TestClass]
    public class GenericHelperTest
    {
        [TestMethod]
        public void CalculatePreminum_Returns_Premiun()
        {
            var result = GenericHelper.CalculatePremiumVariables(12.35);
            Assert.IsInstanceOfType(result, typeof(RenewalModel));
        }

        [TestMethod]
        public void RoundUp_Retunrs_Double()
        {
            var result = GenericHelper.RoundUp(12.3, 2);
            Assert.IsInstanceOfType(result, typeof(double));
        }

        [TestMethod]
        public void FormattingFile_Returns_FormattedFile()
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

            //Act.
            var result = GenericHelper.FormattingFile(viewModel, new StringBuilder(streamData));
            //Assert.
            Assert.IsInstanceOfType(result, typeof(string));

        }
    }
}

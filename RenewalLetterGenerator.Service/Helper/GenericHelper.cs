using RenewalLetterGenerator.Models;
using System;
using System.Configuration;
using System.Text;

namespace RenewalLetterGenerator.Helper
{
    public class GenericHelper
    {


        #region Constants
        private const string Date = "{The current date}";
        private const string ToNameFieldValue = "{customer’s full name including Title}";
        private const string DearNameFieldvalue = "{customer’s Title followed by Surname}";
        private const string ProductName = "{Product Name}";
        private const string AnnualPremium = "{Annual Premium}";
        private const string CreditCharge = "{Credit Charge}";
        private const string Payout = "{Payout Amount}";
        private const string IntialMonthly = "{Initial Monthly Payment Amount}";
        private const string OtherPayment = "{Other Monthly Payments Amount}";
        private const string TotalCost = "{Annual Premium plus Credit Charge}";
        #endregion

        /// <summary>
        /// Calculate the variables for a member for a given annual premium.
        /// </summary>
        /// <param name="annualPremium"></param>
        /// <returns></returns>
        // need to rename with a MeaningFullName
        public static RenewalModel CalculatePremiumVariables(Double annualPremium)
        {

            //credit charge calculation
            var creditCharge = RoundUp(annualPremium * .05, 2);

            var monthlyInst = ((creditCharge + annualPremium) / 12).ToString("0.00");
            var totalAmount = RoundUp((creditCharge + annualPremium), 2);

            var firstMonthPremium = RoundUp(totalAmount - (11 * Convert.ToDouble(monthlyInst)), 2);
            var buffer = firstMonthPremium;
            firstMonthPremium = firstMonthPremium < Convert.ToDouble(monthlyInst) ? firstMonthPremium + .11 : firstMonthPremium;
            monthlyInst = buffer < Convert.ToDouble(monthlyInst) ? (Convert.ToDouble(monthlyInst) - .01).ToString() : monthlyInst;

            return new RenewalModel()
            {
                CreditCharge = RoundUp(annualPremium * .05, 2).ToString(),
                TotalPremium = RoundUp((creditCharge + annualPremium), 2).ToString(),
                AverageMonthlyPremium = monthlyInst,
                IntialMonthlyPremium = firstMonthPremium,
                RemainingMonthlyPremium = (11 * Convert.ToDouble(monthlyInst)).ToString("0.00")
            };
        }

        /// <summary>
        /// Used for round Up to calculate upto req decimal places
        /// </summary>
        /// <param name="input"></param>
        /// <param name="places"></param>
        /// <returns></returns>
        public static double RoundUp(double input, int places)
        {
            double multiplier = Math.Pow(10, Convert.ToDouble(places));
            return Math.Ceiling(input * multiplier) / multiplier;
        }


        /// <summary>
        /// formatting the renewal letter template with member details
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="File"></param>
        /// <returns>Individual Renewal Letter</returns>
        public static string FormattingFile(ViewModel viewModel, StringBuilder File)
        {

            if (File.ToString().Contains(Date))
            {
                File = File.Replace(Date, string.Format("{0:d}", DateTime.Now.ToString("dd/MM/yyyy")));
            }
            if (File.ToString().Contains(ToNameFieldValue))
            {
                File = File.Replace(ToNameFieldValue, viewModel.MemDetails.Title + " " + viewModel.MemDetails.FirstName);
            }
            if (File.ToString().Contains(DearNameFieldvalue))
            {
                File = File.Replace(DearNameFieldvalue, viewModel.MemDetails.Title + " " + viewModel.MemDetails.SurName);
            }
            if (File.ToString().Contains(ProductName))
            {
                File = File.Replace(ProductName, viewModel.MemDetails.ProductName);

            }
            if (File.ToString().Contains(AnnualPremium))
            {
                File = File.Replace(AnnualPremium, viewModel.MemDetails.AnnualPremium.ToString());
            }
            if (File.ToString().Contains(CreditCharge))
            {
                File = File.Replace(CreditCharge, viewModel.RenewalDetails.CreditCharge);
            }

            if (File.ToString().Contains(Payout))
            {
                File = File.Replace(Payout, viewModel.RenewalDetails.TotalPremium);
            }

            if (File.ToString().Contains(IntialMonthly))
            {
                File = File.Replace(IntialMonthly, viewModel.RenewalDetails.IntialMonthlyPremium.ToString());
            }

            if (File.ToString().Contains(OtherPayment))
            {
                File = File.Replace(OtherPayment, viewModel.RenewalDetails.AverageMonthlyPremium.ToString());
            }
            if (File.ToString().Contains(TotalCost))
            {
                File = File.Replace(TotalCost, viewModel.RenewalDetails.TotalPremium.ToString());
            }
            return File.ToString();
        }


    }
}
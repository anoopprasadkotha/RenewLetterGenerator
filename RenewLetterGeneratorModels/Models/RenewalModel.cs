using System;

namespace RenewalLetterGenerator.Models
{
    /// <summary>
    /// Stores the calculated Values for renewal
    /// </summary>
    public class RenewalModel
    {
        /// <summary>
        ///gets or sets CreditCharge
        /// </summary>
        public string CreditCharge { get; set; }
        /// <summary>
        ///gets or sets TotalPremium
        /// </summary>
        public string TotalPremium { get; set; }
        /// <summary>
        ///gets or sets AverageMonthlyPremium
        /// </summary>
        public string AverageMonthlyPremium { get; set; }
        /// <summary>
        ///gets or sets IntialMonthlyPremium
        /// </summary>
        public Double IntialMonthlyPremium { get; set; }
        /// <summary>
        ///gets or sets RemainingMonthlyPremium
        /// </summary>
        public string RemainingMonthlyPremium { get; set; }
    }
}
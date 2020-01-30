using System;

namespace RenewalLetterGenerator.Models
{
    /// <summary>
    /// Member Details 
    /// </summary>
    public class MemberDetailsModel
    {

        #region Fields from Excel 
        /// <summary>
        /// Gets or sets Member Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Member Title
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Gets or sets Member FirstName
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// Gets or sets Member SurName
        /// </summary>
        public string SurName { get; set; }


        /// <summary>
        /// Gets or sets Member ProductName
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets Member PayoutAmount
        /// </summary>
        public Double PayoutAmount { get; set; }

        /// <summary>
        /// Gets or sets Member AnnualPremium
        /// </summary>
        public Double AnnualPremium { get; set; }

        #endregion

        /// <summary>
        /// indicates wether the file is generated now or past
        /// </summary>
        public bool IsGeneratedNow { get; set; }


    }
}
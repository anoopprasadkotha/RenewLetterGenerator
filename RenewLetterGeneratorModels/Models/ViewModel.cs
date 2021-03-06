﻿namespace RenewalLetterGenerator.Models
{
    /// <summary>
    /// Complete details of every member
    /// </summary>
    public class ViewModel
    {
        /// <summary>
        /// individual Member Details From the Excel
        /// </summary>
        public MemberDetailsModel MemDetails { get; set; }

        /// <summary>
        ///Calculated parameters required for the formating of Renewal letter
        /// </summary>
        public RenewalModel RenewalDetails { get; set; }

      
    }
}
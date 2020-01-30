using System;

namespace RenewalLetterGenerator.Models
{
    public class MemberDetailsModel
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string ProductName { get; set; }

        public Double PayoutAmount { get; set; }

        public Double AnnualPremium { get; set; }

        //indicates wether the file is generated now or past

        public bool IsGeneratedNow { get; set; }


    }
}
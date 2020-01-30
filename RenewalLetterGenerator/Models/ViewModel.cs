namespace RenewalLetterGenerator.Models
{
    public class ViewModel
    {
        //individual Member Details From the Excel

        public MemberDetailsModel MemDetails { get; set; }

        //Calculated parameters required for the formating of Renewal letter

        public RenewalModel RenewalDetails { get; set; }

      
    }
}
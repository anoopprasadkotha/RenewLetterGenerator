using RenewalLetterGenerator.Helper;
using RenewalLetterGenerator.Models;
using System;
using System.Data;
using System.Text;

namespace RenewalLetterGenerator.ModelBinder
{
    public class ViewModelBinder
    {
        // private static Logger _logger = new Logger();



        public static ViewModel Bind(DataTable csvTable, int i)
        {
            var viewModel = new ViewModel();
            if (ValidateRow(csvTable, i))
            {
                viewModel = new ViewModel()
                {
                    MemDetails = new MemberDetailsModel
                    {
                        Id = Convert.ToInt32(RemoveAdditionalChars(csvTable.Rows[i][0].ToString())),
                        Title = RemoveAdditionalChars(csvTable.Rows[i][1].ToString()),
                        FirstName = RemoveAdditionalChars(csvTable.Rows[i][2].ToString()),
                        SurName = RemoveAdditionalChars(csvTable.Rows[i][3].ToString()),
                        ProductName = RemoveAdditionalChars(csvTable.Rows[i][4].ToString()),
                        PayoutAmount = Convert.ToDouble(RemoveAdditionalChars(csvTable.Rows[i][5].ToString())),
                        AnnualPremium = Convert.ToDouble(RemoveAdditionalChars(csvTable.Rows[i][6].ToString()))
                    },
                    RenewalDetails = GenericHelper.CalculatePremiumVariables(Convert.ToDouble(csvTable.Rows[i][6]))
                };
            }
            else
            {
                viewModel = null;

            }
            return viewModel;
        }
        /// <summary>
        /// validating wether the row is as per the model
        /// </summary>
        /// <param name="csvTable"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool ValidateRow(DataTable csvTable, int i)
        {
            var str = new StringBuilder(String.Format("The data in the row{0} is not valid and fields which are not valid are ", i));

            bool isValid;
            int id;
            float premium;
            isValid = Int32.TryParse((csvTable.Rows[i][0].ToString()), out id);
            if (!Int32.TryParse((csvTable.Rows[i][0].ToString()), out id)) str.Append(String.Format("ID ={0},", (csvTable.Rows[i][0].ToString())));
            isValid = isValid && float.TryParse((csvTable.Rows[i][5].ToString()), out premium);
            if (!float.TryParse((csvTable.Rows[i][0].ToString()), out premium)) str.Append(String.Format("PayoutAmount ={0} ,", (csvTable.Rows[i][0].ToString())));
            isValid = isValid && float.TryParse((csvTable.Rows[i][6].ToString()), out premium);
            if (!float.TryParse((csvTable.Rows[i][6].ToString()), out premium)) str.Append(String.Format("AnnualPremium={0}", (csvTable.Rows[i][0].ToString())));
            //after checking the data is invalid log  the stringbuilder in log
            if (!isValid)
            {
                // _logger.Log(str.ToString());
            }
            return isValid;
        }

        /// <summary>
        /// Removes /n if it is present in any string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>empty string if the input is only /n</returns>
        private static string RemoveAdditionalChars(string input)
        {
            // need to be extended for various other escape sequence
            if (input == null) input = "";
            return input.Contains("\n") ? input.Replace("\n", "") : input;

        }
    }
}
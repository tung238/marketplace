using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TNMarketplace.Core.Extensions
{
    public static class StringExtension
    {
        public static string ConvertToUnSign(this string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'd').ToLower();
        }

        public static string ConvertToSlug(this string s)
        {
            return string.Join("-", s.ConvertToUnSign().Split(new char[] { ' ', '/', '-', ',' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public static double GetDoubleValue(this string s)
        {
            double number;
            if (double.TryParse(s, out number))
            {
                return number;
            }
            return 0;
        }
    }
}

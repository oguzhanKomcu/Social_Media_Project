using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Extensions
{
    public static class StringExtensions
    {

        
        public static string GenerateSlug(this string slug)
        {

            var sb = new StringBuilder();
            foreach (char character in slug )
            {
                if (!char.IsPunctuation(character))
                {
                    sb.Append(character);
                }
            }
            return sb.ToString().ToLower().Replace(" ", "-");

        }
        
        public static bool EqualNoCase(this string text , string toCompare)
        {
            return text?.ToLower() == toCompare?.ToLower();
        }
        

        
    }
}

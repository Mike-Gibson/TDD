using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Library
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string value, StringComparison comparer)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(source))
            {
                return false;
            }

            return source.IndexOf(value, comparer) >= 0;
        }

        public static bool Contains(this IEnumerable<char> source, char value, StringComparison comparer)
        {
            if (source == null)
            {
                return false;
            }

            return new string(source.ToArray()).Contains(value.ToString(), comparer);
        }
    }
}

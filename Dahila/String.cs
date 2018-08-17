using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvEmUpdate
{
    public static class String
    {

        public static IEnumerable<string> EnumerateByLength(this string text, int length)
        {
            int index = 0;
            while (index < text.Length)
            {
                int charCount = Math.Min(length, text.Length - index);
                yield return text.Substring(index, charCount);
                index += length;
            }
        }

        public static string[] SplitByLength(this string text, int length)
        {
            return text.EnumerateByLength(length).ToArray();
        }
    }
}

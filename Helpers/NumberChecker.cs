using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    public static class NumberChecker
    {
        public static bool isNumber (string input)
        {
            return int.TryParse(input, out int result) || string.IsNullOrWhiteSpace(input) || input.Length < 3;
        }
    }
}

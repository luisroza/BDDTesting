using System;
using System.Linq;

namespace WebStore.BDD.Tests.Config
{
    public static class ExtentionMethods
    {
        public static int OnlyNumbers(this string value)
        {
            return Convert.ToInt16(new string(value.Where(char.IsDigit).ToArray()));
        }
    }
}

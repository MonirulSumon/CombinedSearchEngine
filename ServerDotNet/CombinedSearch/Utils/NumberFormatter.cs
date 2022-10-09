using System.Globalization;

namespace CombinedSearch.Utils
{
    public static class NumberFormatter
    {
        public static string GetNumericFormattedNumber(string number) => long.TryParse(number, out long num)
            ? FormatNumber(num) : "Invalid number";

        public static string ToNumericFormat(this int num) => FormatNumber(num);
        public static string ToNumericFormat(this long num) => FormatNumber(num);

        private static string FormatNumber(long number)
        {
            return number switch
            {
                > 999999999 or < -999999999 => number.ToString("0,,,.###B", CultureInfo.CurrentCulture),
                > 999999 or < -999999 => number.ToString("0,,.##M", CultureInfo.CurrentCulture),
                > 999 or < -999 => number.ToString("0,.#K", CultureInfo.CurrentCulture),
                _ => number.ToString(CultureInfo.CurrentCulture),
            };
        }
    }
}

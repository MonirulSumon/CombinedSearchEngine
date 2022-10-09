namespace CombinedSearch.Utils
{
    public static class Extensions
    {
        public static string OrEmpty(this string value)
        {
            return value is null ? string.Empty : value;
        }

        public static bool IsNotNull(this string value)
        {
            return value is not null;
        }
        public static bool IsNull(this string value)
        {
            return value is null;
        }
    }
}

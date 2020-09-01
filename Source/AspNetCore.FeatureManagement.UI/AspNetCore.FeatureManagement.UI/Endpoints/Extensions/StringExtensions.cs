namespace System
{
    public static class StringExtensions
    {
        public static string AsRelativeResource(this string resourcePath)
        {
            return resourcePath.StartsWith("/") ? resourcePath[1..] : resourcePath;
        }
    }
}
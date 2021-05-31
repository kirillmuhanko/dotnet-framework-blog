using BlogApplication.Shared.Mathematics;

namespace BlogApplication.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string text, int length)
        {
            if (string.IsNullOrEmpty(text)) return text;

            length = BasicMath.Clamp(length, 0, text.Length);

            var str = text.Substring(0, length);
            return str;
        }
    }
}
using System;
using BlogApplication.Models.Components.Controls;
using BlogApplication.Models.Constants.StringSymbols;

namespace BlogApplication.Shared.Extensions
{
    public static class TextComponentExtensions
    {
        public static string Truncate(
            this LabelComponent boxComponent,
            int length,
            string ellipsis = null,
            bool removeLastWord = true)
        {
            var text = $"{boxComponent?.Text}";

            if (length >= text.Length) return text;

            var str = text.Truncate(length);
            var index = str.LastIndexOf(StringSymbols.Space, StringComparison.Ordinal);

            if (removeLastWord && index == 1) str = str.Remove(index);

            str += $"{ellipsis}";
            return str;
        }
    }
}
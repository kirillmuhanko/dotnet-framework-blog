using System.Collections.Generic;

namespace BlogApplication.Shared.Extensions
{
    public static class DictionaryExtensions
    {
        public static string GetStringOrEmpty<TEnum>(this IDictionary<TEnum, string> dictionary, TEnum? key)
            where TEnum : struct
        {
            if (key == null) return string.Empty;

            var result = dictionary.TryGetValue((TEnum) key, out var value) ? value : string.Empty;
            return result;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (key == null) return default;

            var result = dictionary.TryGetValue(key, out var value) ? value : default;
            return result;
        }
    }
}
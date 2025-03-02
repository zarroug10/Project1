namespace web.Helpers;
    public static class Helpers
    {
        /// <summary>
        /// Set a string to PascalCase format (first letter is upper case).
        /// </summary>
        /// <param name="value"></param>
        /// <param name="charsToIgnores"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string value, params char[] charsToIgnores)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            if (value.Length == 1)
                return value.ToUpperInvariant();

            if (!value.Intersect(charsToIgnores).Any())
                return char.ToUpperInvariant(value[0]) + value[1..];

            return string.Join(string.Empty, value.Split(charsToIgnores).Select(x => x.ToPascalCase()));
        }
    }
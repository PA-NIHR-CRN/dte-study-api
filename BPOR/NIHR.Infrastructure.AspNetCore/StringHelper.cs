namespace NIHR.Infrastructure.AspNetCore
{
    public static class StringHelper
    {
        public static string RemoveWhitespace(this string input)
        {
        
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return new string(input
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}
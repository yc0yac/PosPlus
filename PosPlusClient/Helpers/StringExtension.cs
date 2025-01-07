namespace PosPlusClient.Helpers;

public static class StringExtension
{
    public static string GetInitials(this string? name) 
        => name != null
        ? string.Concat(name.Split(' ')
                .Where(palabra => !string.IsNullOrEmpty(palabra))
                .Select(palabra => palabra[0]))
                .ToUpper()
        : string.Empty;
}
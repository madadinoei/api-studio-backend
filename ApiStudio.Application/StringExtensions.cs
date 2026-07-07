using System.Globalization;
using System.Text.RegularExpressions;

namespace ApiStudio.Application;

public static class StringExtensions
{
    public static string ToPascalCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        // تبدیل جداکننده‌ها (فاصله، خط تیره و آندرلاین) به فاصله عادی برای پردازش یکدست
        string cleanInput = Regex.Replace(input, @"[-_ ]+", " ");

        TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
        string titleCase = textInfo.ToTitleCase(cleanInput.ToLower());

        // حذف فاصله‌ها
        return titleCase.Replace(" ", "");
    }
}
using System.Text;

namespace maui_training.Helpers;

public static class PhonewordTranslator
{

    public static string? ToNumber(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return null;

        raw = raw.ToUpperInvariant();

        var newNumber = new StringBuilder();      // debug here what happens if just stringbuilder?? what to return?

        foreach (var c in raw)
        {
            if ("-0123456789".Contains(c))
            {
                newNumber.Append(c);
            }
            else
            {
                var translatedNumber = TranslateToNumber(c);
                if (translatedNumber != null)
                {
                    newNumber.Append(translatedNumber);
                }
                else return null;
            }
        }
        return newNumber.ToString();
    }


    static readonly string[] digits =
      [
        "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
      ];



    private static int? TranslateToNumber(char c)
    {
        for (int i = 0; i < digits.Length; i++)
        {
            if (digits[i].Contains(c)) return 2 + i;
        }
        return null;
    }
}

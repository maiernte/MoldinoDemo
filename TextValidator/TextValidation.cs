using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace TextValidator;

public static class TextValidation
{
    /// <summary>
    /// Count the occurrences of request.Words in request.Text.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>
    /// 
    /// </returns>
    /// <exception cref="ArgumentException"></exception>
    public static CountWordResponse CountWords(CountWordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text) || string.IsNullOrWhiteSpace(request.Words))
        {
            throw new ArgumentException("Text and Words are required.");
        }

        var wordArray = request.Words.Split(';', StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
        var response = new CountWordResponse();
        response.CheckType = wordArray.Any(w => w.Length > 1) ? "word" : "letter";

        foreach (var word in wordArray)
        {
            response.WordCounter[word] = response.CheckType == "word" ? 
                  CountWord(request.Text, word) 
                : CountLetter(request.Text, word.ToCharArray().First());
        }

        return response;
    }

    /// <summary>
    /// Check whether the input is a base64 encoded string.
    /// An empty string is not considered base64 encoded.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsBase64Coding(string input)
    {
        try
        {
            ArgumentException.ThrowIfNullOrEmpty(input);
            _ = Convert.FromBase64String(input);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    /// <summary>
    /// Check whether the input is an valid email address.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsEmail(string input)
    {
        try
        {
            ArgumentException.ThrowIfNullOrEmpty(input);
            var mail = new MailAddress(input);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
       
    }

    /// <summary>
    /// Convert a text to a decimal number.
    /// </summary>
    /// <param name="input">
    /// A text that contains numbers, comma, dot, underscore  and letter m.
    /// </param>
    /// <returns>
    /// if success: return the decimal number as string
    /// if failed: return "0"
    /// </returns>
    public static string DecimalConverter(string input)
    {
        string pattern = @"^[0-9m,._' ]+$";
        if (!Regex.IsMatch(input, pattern))
        {
            return "0";
        }
        
        // step 1: find the real position of decimal separator
        int idxLastComma = input.LastIndexOf(',');
        int idxLastDot = input.LastIndexOf('.');
        int index = Math.Max(idxLastComma, idxLastDot);
        
        // step 2: replace the decimal separator with "#"
        string tmp = string.Empty;
        if(index >= 0)
        {
            tmp = input.Remove(index, 1);
            tmp = tmp.Insert(index, "#");
        }
        else
        {
            tmp = input;
        }
        
        // step 3: remove all other characters
        tmp = tmp.Replace(",", string.Empty)
            .Replace(".", string.Empty)
            .Replace("_", string.Empty)
            .Replace("'", string.Empty)
            .Replace(" ", string.Empty)
            .Replace("m", string.Empty);
        
        // step 4: reset the decimal separator
        string res = tmp.Replace("#", ",");
        return res;
    }

    private static int CountLetter(string text, char letter)
    {
        char[] items = text.ToCharArray();
        int num = items.Count(l => l == letter);
        return num;
    }
    
    private static int CountWord(string text, string word)
    {
        string pattern = $@"\b{Regex.Escape(word)}\b";
        return Regex.Matches(text, pattern, RegexOptions.IgnoreCase).Count;
    }
}
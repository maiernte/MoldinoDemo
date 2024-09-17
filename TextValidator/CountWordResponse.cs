using System.ComponentModel.DataAnnotations;

namespace TextValidator;


public class CountWordResponse
{
    [AllowedValues("word", "letter")]
    public string CheckType { get; set; } = string.Empty;

    public Dictionary<string, int> WordCounter { get; } = new Dictionary<string, int>();
}
namespace TextValidator;

public class CountWordRequest
{
    /// <summary>
    /// The text in which to count the occurrences of the specified words.
    /// </summary>
    public string Text { get; set; } = string.Empty;
    
    /// <summary>
    /// One or more words, separated by semicolons, to search for within the text.
    /// </summary>
    public string? Words { get; set; } = string.Empty; 
}
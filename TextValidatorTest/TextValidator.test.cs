using TextValidator;

namespace TextValidatorTest;

public class TextValidatorTest
{
    private string _text = "Der Sommer bringt nicht nur Sonnenschein, sondern auch Zecken mit sich. Die Bisse der kleinen Blutsauger können aber ganz schön gefährlich werden: Im schlimmsten Fall können sie Krankheiten wie Borreliose und FSME übertragen. Mit diesem genailen Mittel aus der Küche schützt du dich ganz ohne Chemie!Beim Spaziergang, Radfahren oder Waldlauf – im hohen Gras, in Büschen am Wegesrand oder im Unterholz lauern Zecken in Wartestellung auf frisches Blut. Davon abgesehen, dass die Mini-Blutsauger nervig sind, können sie auch gefährliche Krankheiten wie Frühsommer-Meningoenzephalitis, kurz FSME, und Borreliose übertragen.";
    [SetUp]
    public void Setup()
    {
        // do nothin
    }

    /// <summary>
    /// Test für Aufgabe 1
    /// </summary>
    [Test]
    public void CountWordTest()
    {
        string words = "mit";
        var res = TextValidation.CountWords(new CountWordRequest { Text = _text, Words = words });
        Assert.That(res.WordCounter.Count, Is.EqualTo(1));
        Assert.That(res.WordCounter["mit"], Is.EqualTo(2));
        Assert.That(res.CheckType, Is.EqualTo("word"));
        
        words = "mit;sie";
        res = TextValidation.CountWords(new CountWordRequest { Text = _text, Words = words });
        Assert.That(res.WordCounter.Count, Is.EqualTo(2));
        Assert.That(res.WordCounter["mit"], Is.EqualTo(2));
        Assert.That(res.WordCounter["sie"], Is.EqualTo(2));
        Assert.That(res.CheckType, Is.EqualTo("word"));
    }

    /// <summary>
    /// Test für Aufgabe 1
    /// </summary>
    [Test]
    public void CountWordTest_WithBadRequest()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            var res = TextValidation.CountWords(new CountWordRequest { Text = _text, Words = string.Empty });
        });
        
        Assert.That(exception.Message, Is.EqualTo("Text and Words are required."));
        
        exception = Assert.Throws<ArgumentException>(() =>
        {
            var res = TextValidation.CountWords(new CountWordRequest { Text = string.Empty, Words = "sie" });
        });
        
        Assert.That(exception.Message, Is.EqualTo("Text and Words are required."));
    }

    /// <summary>
    /// Test für Aufgabe 2
    /// </summary>
    [Test]
    public void CheckWordExistTest()
    {
        string words = "mit";
        var res = TextValidation.CountWords(new CountWordRequest { Text = _text, Words = words });
        Assert.That(res.WordCounter.Count, Is.EqualTo(1));
        Assert.That(res.WordCounter["mit"], Is.GreaterThan(0));
        
        words = "mit;sie";
        res = TextValidation.CountWords(new CountWordRequest { Text = _text, Words = words });
        Assert.That(res.WordCounter.Count, Is.EqualTo(2));
        Assert.That(res.WordCounter["mit"], Is.GreaterThan(0));
        Assert.That(res.WordCounter["sie"], Is.GreaterThan(0));
    }
    
    /// <summary>
    /// Test für Aufgabe 3
    /// </summary>
    [Test]
    public void CountLetterTest()
    {
        string words = "m";
        var res = TextValidation.CountWords(new CountWordRequest { Text = _text, Words = words });
        Assert.That(res.WordCounter.Count, Is.EqualTo(1));
        Assert.That(res.WordCounter["m"], Is.EqualTo(14));
        
        words = "m;v";
        res = TextValidation.CountWords(new CountWordRequest { Text = _text, Words = words });
        Assert.That(res.WordCounter.Count, Is.EqualTo(2));
        Assert.That(res.WordCounter["m"], Is.EqualTo(14));
        Assert.That(res.WordCounter["v"], Is.EqualTo(2));
    }
    
    /// <summary>
    /// Test für Aufgabe 4
    /// </summary>
    [Test]
    public void CheckLetterExistTest()
    {
        string words = "m";
        var res = TextValidation.CountWords(new CountWordRequest { Text = _text, Words = words });
        Assert.That(res.WordCounter.Count, Is.EqualTo(1));
        Assert.That(res.WordCounter["m"], Is.GreaterThan(0));
        
        words = "m;F";
        res = TextValidation.CountWords(new CountWordRequest { Text = _text, Words = words });
        Assert.That(res.WordCounter.Count, Is.EqualTo(2));
        Assert.That(res.WordCounter["m"], Is.GreaterThan(0));
        Assert.That(res.WordCounter["F"], Is.GreaterThan(0));
    }
    
    /// <summary>
    /// Test für Aufgabe 5
    /// </summary>
    [Test]
    public void CheckBase64Test()
    {
        string text = "Hallo, Moldino !";
        var bayes = System.Text.Encoding.UTF8.GetBytes(text);
        var textBase64 = System.Convert.ToBase64String(bayes);
        Assert.That(TextValidation.IsBase64Coding(textBase64), Is.True);
        
        textBase64 = "VGhpcyBpcyBhIGJhc2U2NCBjb2Rpbmc";
        Assert.That(TextValidation.IsBase64Coding(textBase64), Is.False);
        
        Assert.That(TextValidation.IsBase64Coding(string.Empty), Is.False);
    }
    
    /// <summary>
    /// Test für Aufgabe 6
    /// </summary>
    [Test]
    public void CheckEmailAddressTest()
    {
        string mail = "muster.mr@moldino.com";
        Assert.That(TextValidation.IsEmail(mail), Is.True);
        
        mail = ".mr@moldino.com";
        Assert.That(TextValidation.IsEmail(mail), Is.False);
        
        mail = string.Empty;
        Assert.That(TextValidation.IsEmail(mail), Is.False);
    }
    
    /// <summary>
    /// Test für Aufgabe Bonus 2
    /// </summary>
    [Test]
    public void ConvertDecimalTest()
    {
        string numberText = TextValidation.DecimalConverter("f1500.3025");
        Assert.That(numberText, Is.EqualTo("0"));
        
        Assert.That(TextValidation.DecimalConverter("1500.3025"), Is.EqualTo("1500,3025"));
        Assert.That(TextValidation.DecimalConverter("1500,3025"), Is.EqualTo("1500,3025"));
        Assert.That(TextValidation.DecimalConverter("1500, 3025"), Is.EqualTo("1500,3025"));
        Assert.That(TextValidation.DecimalConverter("1500. 3025"), Is.EqualTo("1500,3025"));
        Assert.That(TextValidation.DecimalConverter("1500,00302500"), Is.EqualTo("1500,00302500"));
        Assert.That(TextValidation.DecimalConverter("1500.00302500"), Is.EqualTo("1500,00302500"));
        Assert.That(TextValidation.DecimalConverter("1,500.3025"), Is.EqualTo("1500,3025"));
        Assert.That(TextValidation.DecimalConverter("1.500.3025"), Is.EqualTo("1500,3025"));
        Assert.That(TextValidation.DecimalConverter("1,600,500.3025"), Is.EqualTo("1600500,3025"));
        Assert.That(TextValidation.DecimalConverter("1.600,500.3025"), Is.EqualTo("1600500,3025"));
        Assert.That(TextValidation.DecimalConverter("1,6.00,500.3025"), Is.EqualTo("1600500,3025"));
        Assert.That(TextValidation.DecimalConverter("1,600.500.3025"), Is.EqualTo("1600500,3025"));
        Assert.That(TextValidation.DecimalConverter("1_6_00_500_3025"), Is.EqualTo("16005003025"));
        Assert.That(TextValidation.DecimalConverter("1_6_00_500.3025"), Is.EqualTo("1600500,3025"));
        Assert.That(TextValidation.DecimalConverter("1_6_00_500_3025.01"), Is.EqualTo("16005003025,01"));
        Assert.That(TextValidation.DecimalConverter("1_6_00_500.3025.01"), Is.EqualTo("16005003025,01"));
        Assert.That(TextValidation.DecimalConverter("1,6.00.500.3025m"), Is.EqualTo("1600500,3025"));
        Assert.That(TextValidation.DecimalConverter("1,6.00,500.3025m"), Is.EqualTo("1600500,3025"));
        Assert.That(TextValidation.DecimalConverter("f1,6.00,00.3025"), Is.EqualTo("0"));
        Assert.That(TextValidation.DecimalConverter("f1.6,00,00.3025"), Is.EqualTo("0"));
    }
}
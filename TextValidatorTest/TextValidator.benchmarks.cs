using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NUnit.Framework;
using System.Threading;
using TextValidator;

namespace TextValidatorTest;

[TestFixture]
public class TextValidatorBenchmarkTests
{
    private string _bytesForCheckBase64 =  System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("Hallo, Moldino !"));
    
    [Benchmark]
    public void BenchmarkCountWord()
    {
        string _text = "Der Sommer bringt nicht nur Sonnenschein, sondern auch Zecken mit sich. Die Bisse der kleinen Blutsauger können aber ganz schön gefährlich werden: Im schlimmsten Fall können sie Krankheiten wie Borreliose und FSME übertragen. Mit diesem genailen Mittel aus der Küche schützt du dich ganz ohne Chemie!Beim Spaziergang, Radfahren oder Waldlauf – im hohen Gras, in Büschen am Wegesrand oder im Unterholz lauern Zecken in Wartestellung auf frisches Blut. Davon abgesehen, dass die Mini-Blutsauger nervig sind, können sie auch gefährliche Krankheiten wie Frühsommer-Meningoenzephalitis, kurz FSME, und Borreliose übertragen.";
        var words = "mit;sie";
        var res = TextValidation.CountWords(new CountWordRequest { Text = _text, Words = words });
    }
    
    [Benchmark]
    public void BenchmarkCheckBase64()
    {
        _ = TextValidation.IsBase64Coding(this._bytesForCheckBase64);
    }
    
    [Benchmark]
    public void BenchmarkCheckEmail()
    {
        TextValidation.IsEmail("muster.mr@moldino.com");
    }
    
    [Benchmark]
    public void BenchmarkConvertDecimal()
    {
        TextValidation.DecimalConverter("1_6_00_500.3025.01");
    }
    
    [Test]
    public void RunBenchmarks()
    {
        var summary = BenchmarkRunner.Run<TextValidatorBenchmarkTests>();
        Console.WriteLine($"Log file will be found at: {summary.LogFilePath}");
    }
}
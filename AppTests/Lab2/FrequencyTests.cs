using App.Lab2;

namespace AppTests.Lab2;

public class FrequencyTests
{
    [TestCase("She stood up. Then she left.", new[] { "she left", "stood up", "then she", "she stood up", "then she left" })]
    [TestCase("a b c d. b c d. e b c a d.", new[] { "a b", "b c", "c d", "e b", "a b c", "b c d", "e b c", "c a d" })]
    [TestCase("", new string[0])]
    [TestCase("     ", new string[0])]
    [TestCase("hello", new string[0])]
    [TestCase("a. b. c.", new string[0])]
    [TestCase("a! b? c.", new string[0])]
    [TestCase("...!!!???", new string[0])]
    [TestCase("a...b???c!!!", new string[0])]
    [TestCase("!!!a???", new string[0])]
    [TestCase("a b.", new[] { "a b" })]
    [TestCase("...a b!!!", new[] { "a b" })]
    [TestCase("a b!!! c d???", new[] { "a b", "c d" })]
    [TestCase("   a    b   ", new[] { "a b" })]
    [TestCase("a,b;,:c", new[] { "a b", "b c", "a b c" })]
    [TestCase("a a a a", new[] { "a a", "a a a" })]
    [TestCase("ABC abc.", new[] { "abc abc" })]
    [TestCase("a b. c", new[] { "a b" })]
    [TestCase("a. b c", new[] { "b c" })]
    public void TestPasses_When_Result_Correct(string inputText, string[] dictionaryLines)
    {
        var result = Frequency.FrequencyAnalysis(inputText);
        var actualLines = new List<string>();

        foreach (var pair in result)
            actualLines.Add(pair.Key + " " + pair.Value);

        Assert.That(actualLines, Is.EquivalentTo(dictionaryLines));
    }
}
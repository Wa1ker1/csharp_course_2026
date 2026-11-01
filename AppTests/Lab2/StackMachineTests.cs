using App.Lab2;

namespace AppTests.Lab2;

public class StackMachineTests
{
    [TestCase(new[] { "push Привет! Это снова я! Пока!","pop 5","push Как твои успехи? Плохо?","push qwertyuiop","push 1234567890","pop 27" }, "Привет! Это снова я! Как твои успехи?")]
    [TestCase(new[] { "push Hello,", "push World!" }, "Hello,World!")]
    [TestCase(new[] { "push Hello" }, "Hello")]
    [TestCase(new[] { "push Hello", "pop 2" }, "Hel")]
    [TestCase(new[] { "push Hello", "pop 5" }, "")]
    [TestCase(new[] { "push abc", "push def" }, "abcdef")]
    [TestCase(new[] { "push abc", "pop 1", "push de" }, "abde")]
    [TestCase(new[] { "push Hello", "pop 0" }, "Hello")]
    [TestCase(new[] { "push Hello World", "pop 6" }, "Hello")]
    [TestCase(new string[0], "")]
    public void TestPasses_When_Result_Correct(string[] codeLines, string expected)
    {
        var actual = StackMachine.CalculateString(codeLines);

        Assert.That(actual, Is.EqualTo(expected));
    }
}
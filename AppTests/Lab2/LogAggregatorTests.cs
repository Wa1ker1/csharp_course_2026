
using App.Lab2;

namespace AppTests.Lab2;

public class LogAggregatorTests
{
    [Test]
    public void MergeLogs_OnlyGarbage_ReturnsEmptyString()
    {
        var result = LogAggregator.MergeLogs("garbage", "[invalid]", null);

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void MergeLogs_SingleGroup_SortsByTime()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:02] [trace-A] AuthService: Second",
            "[2023-10-25 14:05:01] [trace-A] AuthService: First"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: First",
            "[2023-10-25 14:05:02] AuthService: Second"
        });

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MergeLogs_EmptyInput_ReturnsEmptyString()
    {
        Assert.That(LogAggregator.MergeLogs(), Is.Empty);
        Assert.That(LogAggregator.MergeLogs((string[])null), Is.Empty);
    }

    [TestCase("")]
    [TestCase("   ")]
    [TestCase("!!!???...")]
    [TestCase("[2023-02-29 12:00:00] [A] Auth: Test")]
    [TestCase("[2024-02-30 12:00:00] [A] Auth: Test")]
    [TestCase("[2024-04-31 12:00:00] [A] Auth: Test")]
    [TestCase("[2024-13-01 12:00:00] [A] Auth: Test")]
    [TestCase("[2024-01-01 24:00:00] [A] Auth: Test")]
    [TestCase("[2024-01-01 12:60:00] [A] Auth: Test")]
    [TestCase("[2100-02-29 12:00:00] [A] Auth: Test")]
    [TestCase("[2024-01-01 12:00:00] [] Auth: Test")]
    public void MergeLogs_InvalidInput_ReturnsEmptyString(string line)
    {
        var result = LogAggregator.MergeLogs(line);

        Assert.That(result, Is.Empty);
    }

    [TestCase("2024-02-29 23:59:59")]
    [TestCase("2000-02-29 00:00:00")]
    [TestCase("2024-12-31 23:59:59")]
    public void MergeLogs_ValidDate_ReturnsLog(string timestamp)
    {
        var line = "[" + timestamp + "] [A] Auth: Test";
        var result = LogAggregator.MergeLogs(line);

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [A] ---",
            "[" + timestamp + "] Auth: Test"
        });

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MergeLogs_MultipleGroups_SortsCorrectly()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:03] [A] Auth: Later",
            "[2023-10-25 14:05:00] [B] Gateway: First",
            "[2023-10-25 14:05:01] [A] Auth: First"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [B] ---",
            "[2023-10-25 14:05:00] Gateway: First",
            "",
            "--- TraceId: [A] ---",
            "[2023-10-25 14:05:01] Auth: First",
            "[2023-10-25 14:05:03] Auth: Later"
        });

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MergeLogs_SameTime_SortsByServiceName()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:00] [A] Zeta: Last",
            "[2023-10-25 14:05:00] [A] Alpha: First"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [A] ---",
            "[2023-10-25 14:05:00] Alpha: First",
            "[2023-10-25 14:05:00] Zeta: Last"
        });

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MergeLogs_DuplicatesAndGarbage_PreservesValidLogs()
    {
        var line = "[2023-10-25 14:05:00] [A] Auth: Hello";
        var result = LogAggregator.MergeLogs(line, "garbage", line);

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [A] ---",
            "[2023-10-25 14:05:00] Auth: Hello",
            "[2023-10-25 14:05:00] Auth: Hello"
        });

        Assert.That(result, Is.EqualTo(expected));
    }
}

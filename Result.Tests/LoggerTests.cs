using Result.Exception;
using Result.Interfaces;
using Xunit;

namespace Result.Tests;

public class LoggerTests : IClassFixture<LoggerTests>
{
    public LoggerTests()
    {
        LogMessage = null;
        LogException = null;
        HelperMethods.Result.SetLogger(Logger);
    }

    private string? LogMessage { get; set; }

    private System.Exception? LogException { get; set; }

    private void Logger(System.Exception? exception)
    {
        LogMessage = exception?.Message;
        LogException = exception;
    }

    private static IFailureResult GetFailureResult(System.Exception exception)
    {
        return (IFailureResult)HelperMethods.Result.Wrap(() => throw exception);
    }

    [Fact]
    public void LoggerExceptionTest()
    {
        var exception = new ResultException("Exception");
        var failureResult = GetFailureResult(exception);
        Assert.IsAssignableFrom<IFailureResult>(failureResult);
        Assert.Equal(LogMessage, exception.Message);
        Assert.Equal(LogException, exception);
    }
}
using System;
using Result.Interfaces;
using Xunit;

namespace Result.Tests;

public class ReturnTests
{
    [Fact]
    public void ResultTestSuccessAndNone()
    {
        Assert.IsAssignableFrom<ISuccessResult>(HelperMethods.Result.Return("Hello"));
        Assert.IsAssignableFrom<ISuccessResult<string>>(HelperMethods.Result.Return("Hello"));
    }

    [Fact]
    public void ResultTestNone()
    {
        string testString = null;
        Assert.IsAssignableFrom<INoneResult>(HelperMethods.Result.Return<string>());
        Assert.IsAssignableFrom<INoneResult>(HelperMethods.Result.Return(testString));
        Assert.False(HelperMethods.Result.Return<string>() is IFailureResult);
    }

    [Fact]
    public void ResultPreventDoubleWrapTest()
    {
        var result = HelperMethods.Result.Return(10);
        Assert.IsAssignableFrom<ISuccessResult<int>>(result);

        var doubleResult = HelperMethods.Result.Return(result);
        Assert.IsAssignableFrom<ISuccessResult<int>>(doubleResult);
    }
}
using System.Diagnostics.CodeAnalysis;
using Result.Exception;
using Result.Interfaces;

namespace Result.HelperMethods;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "MemberCanBeInternal")]
public static partial class Result
{
    /// <summary>
    ///     Executes the <paramref name="function" /> and returns the result in an <see cref="ISuccessResult" />.
    ///     If the return value is null, returns a <see cref="INoneResult" />. If an exception is thrown, returns a
    ///     <see cref="IFailureResult" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="function">The function to be wrapped in a result.</param>
    /// <param name="exceptionHandler">
    ///     The exception handler. This function allows the caller to provide the
    ///     <see cref="IFailureResult" /> with a custom exception.
    /// </param>
    /// <returns></returns>
    public static IResult<T> Wrap<T>(Func<T> function, Func<System.Exception, System.Exception>? exceptionHandler = null)
    {
        try
        {
            return Return(function());
        }
        catch (System.Exception e)
        {
            return Wrap<T>(exceptionHandler, e);
        }
    }

    internal static IResult<T> Wrap<T>(Func<IResult<T>> function, Func<System.Exception, System.Exception>? exceptionHandler = null)
    {
        try
        {
            return Return(function());
        }
        catch (System.Exception e)
        {
            return Wrap<T>(exceptionHandler, e);
        }
    }

    /// <summary>
    ///     Executes the <paramref name="action" /> and returns an <see cref="ISuccessResult" />.
    ///     If an exception is thrown, returns a <see cref="IFailureResult" />.
    /// </summary>
    /// <param name="action">The function to be wrapped in a result.</param>
    /// <param name="exceptionHandler">
    ///     The exception handler. This function allows the caller to provide the
    ///     <see cref="IFailureResult" /> with a custom exception.
    /// </param>
    /// <returns></returns>
    public static IResult<bool> Wrap(Action action, Func<System.Exception, System.Exception>? exceptionHandler = null)
    {
        try
        {
            action();
            return Return(true);
        }
        catch (System.Exception e)
        {
            return Wrap<bool>(exceptionHandler, e);
        }
    }

    private static IFailureResult<T> Wrap<T>(Func<System.Exception, System.Exception>? exceptionHandler, System.Exception exception)
    {
        var handler = exceptionHandler ?? (e => e);
        try
        {
            return (IFailureResult<T>) Return<T, System.Exception>(handler(exception));
        }
        catch (System.Exception e)
        {
            return (IFailureResult<T>) Return<T, System.Exception>(new ResultException("Exception Handler threw an exception!", e));
        }
    }
}
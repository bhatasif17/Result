using Result.Interfaces;

namespace Result.Implementation;

internal sealed class FailureResult<T> : FailureResult, IFailureResult<T>
{
    public FailureResult(System.Exception exception) : base(exception)
    {
    }

    public FailureResult(IFailureResult failure) : base(failure)
    {
    }
}

internal class FailureResult : IFailureResult
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FailureResult" /> class.
    ///     Utilizes the Logger action in <see cref="HelperMethods.Result" /> to log exceptions.
    /// </summary>
    /// <param name="exception">The exception.</param>
    public FailureResult(System.Exception exception)
    {
        Exception = exception ?? new System.Exception("Null Exception Passed");
        HelperMethods.Result.Logger(Exception);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="FailureResult" /> class. Prevents multiple logging actions being fired
    ///     when changing between different types.
    /// </summary>
    /// <param name="failure">The failure.</param>
    protected FailureResult(IFailureResult failure) => Exception = failure.Exception;

    public System.Exception Exception { get; }
}
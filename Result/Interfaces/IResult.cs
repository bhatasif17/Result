// ReSharper disable UnusedTypeParameter

namespace  Result.Interfaces;

/// <inheritdoc cref="IResult" />
/// <typeparam name="T">The type of the value to be returned.</typeparam>
/// <seealso cref="IResult" />
public interface IResult<T> : IResult
{
}

/// <summary>
///     Provides an interface to wrap a returned value to indicate either success or failure
/// </summary>
public interface IResult
{
}
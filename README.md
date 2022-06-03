# Result

In order to solve this problem, I’ve created IResult<T>, which is based on the concept of an Option (or Maybe) type from functional programming languages.

IResult<T> comes in three flavors that correspond to the three possible results I discussed above:

ISuccessResult<T> represents a successful operation. It has a single, non-null property, T Value, which contains the desired object.
INoneResult represents, well, a “none” operation. INoneResult doesn’t have any properties, nor is it typed.

IFailureResult is an INoneResult, with one added bonus. It has a non-null Exception property that is populated when it’s created.

The actual implementations of the various flavors of IResult are abstracted away, so the only way to create an IResult is by using the methods in the static Result class: Result.Return<T>() and Result.Wrap<T>().

Bind is similar to LINQ’s SelectMany.
Map is similar to LINQ’s Select.
Iter is the same as LINQ ForEach.

#Examples
#Result.Return<T>()
Result.Return<T>() elevates whatever is passed to it the appropriate IResult<T>.
public void PrintPersonName(int personId)
{
IResult<Person> personResult = SafeGetPerson(personId);
switch (personResult)
{
case ISuccessResult<Person> success:
Console.WriteLine($"{success.Value.FirstName} {success.Value.LastName}");
            return;
        case IFailureResult failure:
            Console.WriteLine($"Database Error: {failure.Exception.Message}");
return;
default:
Console.WriteLine("Person Not Found.");
return;
}
}

public IResult<Person> SafeGetPerson(int personId)
{
try
{
return Result.Return(DBStuff.GetPersonById(personId));
}
catch (Exception e)
{
LogException(e);
return Result.Return<Person>(e);
}
}

#Result.Wrap<T>() and Result.SetLogger()

public void PrintPersonName(int personId)
{
Result.SetLogger(LogException);
IResult<Person> personResult = SafeGetPerson(personId);
if (personResult is ISuccessResult<Person> success)
{
Console.WriteLine($"{success.Value.FirstName} {success.Value.LastName}");
}
else
{
Console.WriteLine("Person Not Found.");
}
}

public IResult<Person> SafeGetPerson(int personId)
{
return Result.Wrap(() => DBStuff.GetPersonById(personId));
}

# Result

IResult<T>, is based on the concept of an Option (or Maybe) type from functional programming languages. IResult<T> comes in three flavors that correspond to the three possible results:

1. ISuccessResult<T> represents a successful operation. It has a single, non-null property, T Value, which contains the desired object.
2. INoneResult represents, well, a “none” operation. INoneResult doesn’t have any properties, nor is it typed.
3. IFailureResult is an INoneResult, with one added bonus. It has a non-null Exception property that is populated when it’s created.

The actual implementations of the various flavors of IResult are abstracted away, so the only way to create an IResult is by using the methods in the static Result class: Result.Return<T>() and Result.Wrap<T>().

- Bind is similar to LINQ’s SelectMany.
- Map is similar to LINQ’s Select.
- Iter is the same as LINQ ForEach.

## Examples

## Result.Return<T>()

```csharp
    public void PrintPersonName(int personId)
    {
        IResult<Person> personResult = SafeGetPerson(personId);
        switch (personResult)
        {
            case ISuccessResult<Person> success:
                Console.WriteLine($"{success.Value.FirstName}{success.Value.LastName");
                return;
            case IFailureResult failure:
                Console.WriteLine($"Database Error: {failure.Exception.Message}");
                return;
            default:
                Console.WriteLine("Person Not Found.");
                return;
        }
    }

    public IResult<Person> SafeGetPerson(int personId)
    {
        try
        {
            return Result.Return(DBStuff.GetPersonById(personId));
        }
        catch (Exception e)
        {
            LogException(e);
            return Result.Return<Person>(e);
        }
    }
```

## Result.Wrap<T>() and Result.SetLogger()

```csharp
    public void PrintPersonName(int personId)
    {
        Result.SetLogger(LogException);
        IResult<Person> personResult = SafeGetPerson(personId);
            if (personResult is ISuccessResult<Person> success)
                {
                 Console.WriteLine($"{success.Value.FirstName}{success.Value.LastName");
                }
            else
            {
                Console.WriteLine("Person Not Found.");
            }
    }

    public IResult<Person> SafeGetPerson(int personId)
    {
        return Result.Wrap(() => DBStuff.GetPersonById(personId));
    }
```

## Reading Input

```csharp
    private static IResult<string> ReadInput()
    {
        Console.Write("Enter Active Person Id: ");
        return Result.Wrap(Console.ReadLine);
    }
```

## Parsing Input

```csharp
    private static IResult<int> ParseInput(string input)
    {
        return Result.Wrap(() => int.Parse(input), e => new Exception("Invalid Input.", e));
    }
```

## Tech

- Net 6

## Installation

Result requires .Net 6 to run.

```sh
dotnet restore
dotnet build
```

## Nuget

```sh

```

## License

GNU General Public License v3.0

###### Reference https://github.com/hellourgo/FunctionalOOP.Result

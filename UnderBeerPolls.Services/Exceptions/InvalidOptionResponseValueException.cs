namespace UnderBeerPolls.Services.Exceptions;

public class InvalidOptionResponseValueException : Exception
{
    public List<long> FailedOptions { get; }

    public InvalidOptionResponseValueException(List<long> failedOptions) : base($"Invalid value type in {string.Join(", ",failedOptions)} options")
    {
        FailedOptions = failedOptions;
    }
}
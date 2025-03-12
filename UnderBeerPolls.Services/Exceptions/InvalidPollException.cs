namespace UnderBeerPolls.Services.Exceptions;

public class InvalidPollException(Guid id) : Exception($"Poll with id {id} doesn't exists or you don't have access to it")
{
    
}
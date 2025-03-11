namespace UnderBeerPolls.Api.Models.Base;

public class ErrorModel<T>
{
    public ErrorModel(T additionalInformation)
    {
        AdditionalInformation = additionalInformation;
    }
    
    public ErrorModel(string message, T additionalInformation)
    {
        Message = message;
        AdditionalInformation = additionalInformation;
    }
    public string Message { get; set; }

    public T AdditionalInformation { get; set; }
}

public class ErrorModel
{
    public ErrorModel()
    {
        
    }
    
    public ErrorModel(string message)
    {
        Message = message;
    }
    public string Message { get; set; }
}
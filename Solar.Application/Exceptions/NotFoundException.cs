namespace Solar.API.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string error) : base(error)
    {
        
    }
}
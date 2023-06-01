namespace Application.Common.Exceptions;

public class AlreadyExists : Exception
{
    public AlreadyExists(string name, object key) : base($"Entity \"{name}\" ({key}) already exists.")
    {
        
    }
}
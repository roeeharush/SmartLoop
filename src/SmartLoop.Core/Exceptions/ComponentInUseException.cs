namespace SmartLoop.Core.Exceptions;

public class ComponentInUseException : Exception
{
    public ComponentInUseException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

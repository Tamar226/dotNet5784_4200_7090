namespace BO;

[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
    public DalXMLFileLoadCreateException(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class DalNoFilterToQuery : Exception
{
    public DalNoFilterToQuery(string? message) : base(message) { }
    public DalNoFilterToQuery(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class DalErrorNotValueINput : Exception
{
    public DalErrorNotValueINput(string? message) : base(message) { }
    public DalErrorNotValueINput(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible(string? message) : base(message) { }
    public DalDeletionImpossible(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class DalErrorINput : Exception
{
    public DalErrorINput(string? message) : base(message) { }
    public DalErrorINput(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}




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
public class BlXMLFileLoadCreateException : Exception
{
    public BlXMLFileLoadCreateException(string? message) : base(message) { }
    public BlXMLFileLoadCreateException(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlNoFilterToQuery : Exception
{
    public BlNoFilterToQuery(string? message) : base(message) { }
    public BlNoFilterToQuery(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlErrorNotValueINput : Exception
{
    public BlErrorNotValueINput(string? message) : base(message) { }
    public BlErrorNotValueINput(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible(string? message) : base(message) { }
    public BlDeletionImpossible(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlErrorINput : Exception
{
    public BlErrorINput(string? message) : base(message) { }
    public BlErrorINput(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
    public BlNullPropertyException(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlPlanningOfProjectTimesException : Exception
{
    public BlPlanningOfProjectTimesException(string? message) : base(message) { }
    public BlPlanningOfProjectTimesException(string message, Exception innerException)
                : base(message, innerException) { }
}

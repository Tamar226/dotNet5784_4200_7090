namespace DO;
[Serializable]
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }

}
[Serializable]
public class DalErrorINput : Exception
{
    public DalErrorINput(string? message) : base(message) { }

}
[Serializable]
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }

}
[Serializable]
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible(string? message) : base(message) { }

}


[Serializable]
public class DalErrorNotValueINput : Exception
{
    public DalErrorNotValueINput(string? message) : base(message) { }

}
[Serializable]
public class DalNoFilterToQuery : Exception
{
    public DalNoFilterToQuery(string? message) : base(message) { }
}
[Serializable]
public class DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }

}




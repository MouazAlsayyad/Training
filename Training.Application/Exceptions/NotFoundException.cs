namespace Training.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public string EntityName { get; }
        public object Key { get; }

        public NotFoundException(string name, object key)
            : base($"{name} ({key}) was not found.")
        {
            EntityName = name;
            Key = key;
        }

        public NotFoundException(string name, object key, string message)
            : base(message)
        {
            EntityName = name;
            Key = key;
        }

        public NotFoundException(string name, object key, string message, Exception innerException)
            : base(message, innerException)
        {
            EntityName = name;
            Key = key;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Entity Name: {EntityName}, Key: {Key}";
        }
    }
}

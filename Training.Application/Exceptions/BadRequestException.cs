namespace Training.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public BadRequestException(string message, IDictionary<string, string[]> errors)
            : base(message)
        {
            Errors = errors ?? new Dictionary<string, string[]>();
        }

        public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();

        public override string ToString()
        {
            if (Errors == null || Errors.Count == 0)
            {
                return base.ToString();
            }

            var errorMessages = Errors
                .SelectMany(kv => kv.Value.Select(v => $"{kv.Key}: {v}"))
                .Aggregate((current, next) => $"{current}; {next}");

            return $"{base.ToString()}, Errors: {errorMessages}";
        }
    }
}

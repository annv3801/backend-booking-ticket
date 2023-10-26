namespace Domain.Exceptions
{
    [Serializable]
    public class ExceptionBase : Exception
    {
        private static string BuildMessage(string message)
        {
            return $"Message: {message}";
        }

        public ExceptionBase(string context, string key, string message) : base(message)
        {
            base.Data["Context"] = context;
            base.Data["Key"] = key;
        }

        public ExceptionBase(string context, string key, string message, Exception exception) : base(message, exception)
        {
            base.Data["Context"] = context;
            base.Data["Key"] = key;
        }

        public ExceptionBase WithData(string name, object value)
        {
            Data[name] = value;
            return this;
        }
    }
}
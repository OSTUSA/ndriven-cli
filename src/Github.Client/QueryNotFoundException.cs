using System;

namespace Github.Client
{
    [Serializable]
    public class QueryNotFoundException : Exception
    {
        public QueryNotFoundException() : base() { }
        public QueryNotFoundException(string message) : base(message) { }
        public QueryNotFoundException(string message, Exception inner) : base(message, inner) { }

        protected QueryNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}

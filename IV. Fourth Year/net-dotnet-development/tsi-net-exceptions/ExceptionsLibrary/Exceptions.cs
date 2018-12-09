using System;
using System.Runtime.Serialization;

namespace ExceptionsLibrary
{
    [Serializable]
    public class NegativeRadiusException : ApplicationException
    {
        public NegativeRadiusException() { }
        public NegativeRadiusException(string message) : base(message) { }
        public NegativeRadiusException(string message, Exception ex) : base(message, ex) { }
        protected NegativeRadiusException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class NegativeHeightException : ApplicationException
    {
        public NegativeHeightException() { }
        public NegativeHeightException(string message) : base(message) { }
        public NegativeHeightException(string message, Exception ex) : base(message, ex) { }
        protected NegativeHeightException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

}

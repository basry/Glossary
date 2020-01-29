using System;
using System.Runtime.Serialization;

namespace Glossary
{
    [Serializable]
    public class GlossaryException : Exception
    {
        public GlossaryException()
        {
        }

        public GlossaryException(string message) : base(message)
        {
        }

        public GlossaryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GlossaryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace NIJ.Controllers
{
    [Serializable]
    public class CustomExeception : Exception
    {
        public CustomExeception()
        {
        }

        public CustomExeception(string message) : base(message)
        {
        }

        public CustomExeception(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomExeception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
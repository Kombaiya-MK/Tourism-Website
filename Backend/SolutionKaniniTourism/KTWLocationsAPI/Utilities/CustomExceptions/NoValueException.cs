using System.Runtime.Serialization;

namespace KTWLocationsAPI.Utilities.CustomExceptions
{
    [Serializable]
    internal class NoValueException : Exception
    {
        public NoValueException()
        {
        }

        public NoValueException(string? message) : base(message)
        {
        }

        public NoValueException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
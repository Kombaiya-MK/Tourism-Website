using System.Runtime.Serialization;

namespace KTWTourPackages.Services.Commands
{
    [Serializable]
    internal class EmptyValueException : Exception
    {
        public EmptyValueException()
        {
        }

        public EmptyValueException(string? message) : base(message)
        {
        }

        public EmptyValueException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmptyValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
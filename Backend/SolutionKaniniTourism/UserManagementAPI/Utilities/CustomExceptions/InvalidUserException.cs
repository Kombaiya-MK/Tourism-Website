using System.Runtime.Serialization;

namespace UserManagementAPI.Services
{
    [Serializable]
    public class InvalidUserException : Exception
    {
        public InvalidUserException()
        {
        }

        public InvalidUserException(string? message) : base(message)
        {
        }

        public InvalidUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
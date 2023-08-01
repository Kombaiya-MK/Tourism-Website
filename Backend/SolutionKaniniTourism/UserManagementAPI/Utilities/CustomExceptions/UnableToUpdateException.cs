using System.Runtime.Serialization;

namespace UserManagementAPI.Services
{
    [Serializable]
    internal class UnableToUpdateException : Exception
    {
        public UnableToUpdateException()
        {
        }

        public UnableToUpdateException(string? message) : base(message)
        {
        }

        public UnableToUpdateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
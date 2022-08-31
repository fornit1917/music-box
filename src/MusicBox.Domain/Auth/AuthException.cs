using System;
using System.Runtime.Serialization;

namespace MusicBox.Domain.Auth
{
    [Serializable]
    public class AuthException : Exception
    {
        public AuthException(string? message) : base(message)
        {
        }

        public AuthException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AuthException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

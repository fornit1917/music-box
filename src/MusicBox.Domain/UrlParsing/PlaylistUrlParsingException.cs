using System;
using System.Runtime.Serialization;

namespace MusicBox.Domain.UrlParsing
{
    [Serializable]
    public class PlaylistUrlParsingException : Exception
    {
        public PlaylistUrlParsingException(string message) : base(message)
        {
        }

        public PlaylistUrlParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PlaylistUrlParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

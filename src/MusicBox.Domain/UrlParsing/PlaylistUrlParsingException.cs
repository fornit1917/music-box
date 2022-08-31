using System;

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
    }
}

namespace MusicBox.Domain.Models
{
    public class PlaylistUrlInfo
    {
        public Uri Uri { get; }
        public Service Service { get; }
        public PlaylistType PlaylistType { get; }
        public bool IsAuthenticationRequired { get; }

        public IReadOnlyDictionary<string, string> Params => _params;

        private Dictionary<string, string> _params { get; } = new Dictionary<string, string>();

        public PlaylistUrlInfo(Uri uri, Service service, PlaylistType playlistType, bool isAuthenticationRequired)
        {
            Uri = uri;
            Service = service;
            PlaylistType = playlistType;
            IsAuthenticationRequired = isAuthenticationRequired;
        }

        public void SetParam(string paramId, string paramValue)
        {
            _params[paramId] = paramValue;
        }

        public string? GetParam(string paramId)
        {
            return _params.ContainsKey(paramId) ? _params[paramId] : null;
        }
    }
}

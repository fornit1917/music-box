namespace MusicBox.Domain.UrlParsing
{
    public interface IPlaylistUrlParser
    {
        PlaylistUrlInfo ParseUrl(string url);
    }
}

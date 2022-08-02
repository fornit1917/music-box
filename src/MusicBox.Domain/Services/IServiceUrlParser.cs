using MusicBox.Domain.Models;
using System;

namespace MusicBox.Domain.Services
{
    public interface IServiceUrlParser
    {
        bool CanParseUrl(Uri uri);
        PlaylistUrlInfo ParseUrl(Uri uri);
    }
}

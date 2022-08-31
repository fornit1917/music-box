using System.Collections.Generic;

namespace MusicBox.Domain.Auth
{
    public class AuthResult
    {
        public IReadOnlyDictionary<string, string> Data { get; }

        public AuthResult(Dictionary<string, string> data)
        {
            Data = data;
        }
    }
}

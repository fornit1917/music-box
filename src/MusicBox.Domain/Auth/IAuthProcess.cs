using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBox.Domain.Auth
{
    public interface IAuthProcess
    {
        Task<AuthResult> SendCredentials(string userName, string password);
    }
}

using System;

namespace ThirdPartyTools
{
    public interface IAuthUser
    {
        bool IsAuthorised { get; set; }
        UserDetail UserDetail { get; set; }

        event EventHandler<bool> AuthorisedAllFileData;
    }
}
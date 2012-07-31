using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Social.Vkontakte
{
    public interface IVkAppConfig
    {
        string AppKey { get; }
        string AppSecret { get; }
    }
}

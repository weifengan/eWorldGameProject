using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public enum Module:byte
    {
        ServerList=1,
        Login=2
    }


    public enum ServerListOperation:byte
    {
        FetchCurrentServer=1,
        FetchAllServer=2
    }


    public enum LoginOperation:byte
    {
        UserLogin=1
    }
}

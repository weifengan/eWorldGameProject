using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 单个服务器信息
    /// </summary>
    [System.Serializable]
    public class ServerItemVo
    {
        public int id { get; set; }
        public string title { get; set; }
        public string host { get; set; }
        public int status { get; set; }
    }

    /// <summary>
    /// 服务器列表
    /// </summary>
    [System.Serializable]
    public class ServerListVo
    {
        public ServerItemVo[] servers;
    }

    /// <summary>
    /// 服务器配置
    /// </summary>
    [System.Serializable]
    public class DBConfigVo
    {
        public string host;
        public string dbname;
        public string dbuser;
        public string dbpwd;
    }

}

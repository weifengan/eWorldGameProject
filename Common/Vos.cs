
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
        public ServerItemVo[] servers { get; set; }

        public override string ToString()
        {
            string info = "";
            foreach(ServerItemVo vo in servers)
            {
                info += vo.id + " " + vo.title + " " + vo.host + " " + vo.status+"\n";
            }

            return info;
        }
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


    [System.Serializable]
    public class LoginResultVo
    {
        public int result;
        public string info;
    }

}

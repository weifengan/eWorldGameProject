using EWorldServer.db;
using EWorldServer.net;
using ExitGames.Logging;

namespace EWorldServer
{
    class Global
    {

        #region  日志输出相关 
        private static ILogger mLog;

        public static void Info(object message)
        {
            mLog.Info(message);
        }

        #endregion

        public static string AppRootFolder = "";

        /// <summary>
        ///  初始化全局对象
        /// </summary>
        public static void Init(ILogger logger, string appURL = "")
        {
            mLog = logger;
            AppRootFolder = appURL;
            HandlerManager.GetInstance().Init();
            DBManager.GetInstance().Init();
            Global.Info("【SYS】游戏服务器启动运行中......");
        }

    }
}

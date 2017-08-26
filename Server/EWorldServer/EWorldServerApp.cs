using EWorldServer.net;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWorldServer
{
    class EWorldServerApp : ApplicationBase
    {
        /// <summary>
        /// 有新客户端连接到服务器
        /// </summary>
        /// <param name="initRequest"></param>
        /// <returns></returns>
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            UserClient uk = new UserClient(initRequest);
            return uk;
        }

        #region 日志输出
        private static readonly ILogger m_log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
        protected override void Setup()
        {

            ExitGames.Logging.LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "bin_Win64\\log");
            GlobalContext.Properties["LogFileName"] = this.ApplicationName;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(this.BinaryPath, "log4net.config")));
            ///初始化日志和应用路径
            Global.Init(m_log, Path.Combine(this.ApplicationRootPath, "bin_Win64\\configs"));
        }

        #endregion 

        protected override void TearDown()
        {
            Global.Info("【SYS】服务器已经关闭.....");
        }
    }
}

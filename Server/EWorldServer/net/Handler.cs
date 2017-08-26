using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWorldServer.net
{
    interface IHandler
    {
        /// <summary>
        /// 消息处理类
        /// </summary>
        /// <param name="user">客户端对象</param>
        /// <param name="moduleCode">模块码</param>
        /// <param name="opCode">操作码</param>
        /// <param name="operationRequest">操作请求</param>
        /// <param name="sendParameters">发送参数</param>
        void Parse(UserClient user, int moduleCode, int operationCode, OperationRequest operationRequest, SendParameters sendParameters);
    }
}

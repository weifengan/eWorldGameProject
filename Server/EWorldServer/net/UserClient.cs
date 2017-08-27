using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotonHostRuntimeInterfaces;
using Common;

namespace EWorldServer.net
{
    class UserClient : ClientPeer
    {
        public UserClient(InitRequest initRequest) : base(initRequest)
        {
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            Global.Info("用户掉线了("+ reasonCode+")");
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            //获取模块码
            byte moudleCode = operationRequest.OperationCode;
            byte operationCode = (byte)operationRequest.Parameters[(byte)80];

            Global.Info("收到=" + (Module)moudleCode+","+operationRequest.Parameters[(byte)80]);

            Handler handler = HandlerManager.GetInstance().FindHandler(moudleCode,operationCode);
            if (handler != null)
            {
                handler.Parse(this,moudleCode, operationCode, operationRequest, sendParameters);
            }
        }
    }
}

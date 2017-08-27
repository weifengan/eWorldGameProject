using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using LitJson;

namespace EWorldServer.net.c2s
{
    class C2SFetchCurrentServer : Handler
    {
        public override void Parse(UserClient user, byte moduleCode, byte operationCode, OperationRequest operationRequest, SendParameters sendParameters)
        {

            ServerItemVo currentServer = new ServerItemVo();
            currentServer.id = 1;
            currentServer.title = "天府情缘";
            currentServer.host = "192.168.1.35:4531";
            currentServer.status = 2;


            S2CMessage msg = new S2CMessage((byte)moduleCode, (byte)operationCode);
            msg.Add(1, JsonMapper.ToJson(currentServer));

            OperationResponse response = new OperationResponse((byte)moduleCode, msg);
            user.SendOperationResponse(response, sendParameters);

            Global.Info("获取当前服务器列表");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using LitJson;
using System.IO;

namespace EWorldServer.net.c2s
{
    class C2SFetchAllServer : Handler
    {
        public override void Parse(UserClient user, byte moduleCode, byte operationCode, OperationRequest operationRequest, SendParameters sendParameters)
        {
            //构建消息
            S2CMessage msg = new S2CMessage((byte)Module.ServerList, (byte)ServerListOperation.FetchAllServer);

            //读取配置文件
            string json = File.ReadAllText(Global.AppRootFolder + "\\serverlist.json");
            ServerListVo svrinfo = JsonMapper.ToObject<ServerListVo>(json);

            //随机服务器状态
            foreach (ServerItemVo item in svrinfo.servers)
            {
                item.status = new Random().Next(1, 3);
            }
            //添加到发送字典 
            msg.Add(1, JsonMapper.ToJson(svrinfo));

            //发送反馈
            OperationResponse response = new OperationResponse((byte)moduleCode, msg);
            user.SendOperationResponse(response, sendParameters);

            Global.Info("收到客户端消息" + moduleCode + "," + operationCode);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using EWorldServer.db.action;
using EWorldServer.db.model;
using Common;
using LitJson;



/**********************************************
      C2SUserLogin 
  
      @author: 魏凤安
      @email: weifengan@163.com
	  @QQ: 1327707237
	  @date: 2017/8/27 7:58:19
  
***********************************************/

namespace EWorldServer.net.c2s
{
    class C2SUserLogin : Handler
    {
        public override void Parse(UserClient user, byte moduleCode, byte operationCode, OperationRequest operationRequest, SendParameters sendParameters)
        {
            //获取用户名和密码
            string username = operationRequest.Parameters[(byte)1].ToString();
            string userpwd = operationRequest.Parameters[(byte)2].ToString();

            AccountAction aa = new AccountAction();

            Account ac = aa.CheckLogin(username, userpwd);


            //构建消息
            S2CMessage msg = new S2CMessage((byte)Module.Login, (byte)LoginOperation.UserLogin);
            Global.Info("向客户端发送信息" + msg.moudleCode + "," + msg.operationCode);

            LoginResultVo lro = new LoginResultVo();


            //登录成功
            if (ac != null)
            {
                lro.result = 1;
                lro.info = "登录成功";
            }
            else
            {
                lro.result = 0;
                lro.info = "用户名或密码错误!";
            }
            msg.Add((byte)1, JsonMapper.ToJson(lro));

            //发送反馈
            OperationResponse response = new OperationResponse((byte)moduleCode, msg);
            user.SendOperationResponse(response, sendParameters);

        }
    }
}

using ExitGames.Client.Photon;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/**********************************************
      Handler 
  
      @author: 魏凤安
      @email: weifengan@163.com
	  @QQ: 1327707237
	  @date: 2017/8/12 1:07:51
  
***********************************************/

public class Handler
{

    public delegate void OnDataHandler(NetManager net, Enums.MoudleCode moudleCode, Enums.OperationCode opCode,OperationResponse response);
    public event OnDataHandler OnParseHandler = null;
    /// <summary>
    /// 消息处理类
    /// </summary>
    /// <param name="user">客户端对象</param>
    /// <param name="moudleCode">模块码</param>
    /// <param name="opCode">操作码</param>
    /// <param name="operationRequest">操作请求</param>
    /// <param name="sendParameters">发送参数</param>
    public virtual void Parse(NetManager net, Enums.MoudleCode moudleCode, Enums.OperationCode opCode, OperationResponse response)
    {
        if (OnParseHandler != null)
        {
            OnParseHandler(net, moudleCode, opCode, response);
        }
    }
}

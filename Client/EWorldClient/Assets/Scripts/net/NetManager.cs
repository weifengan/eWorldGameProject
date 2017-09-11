using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using System;
using EWordProject.Logic;

public class NetManager :MonoBehaviour,IPhotonPeerListener{

    #region  单例实现
    
    private static NetManager _instance = null;

    public static NetManager GetInstance()
    {
        if (_instance == null)
        {
            GameObject go = new GameObject("_NetManager");
            _instance = go.AddComponent<NetManager>();
        }
        return _instance;
    }


    #endregion

    private PhotonPeer peer;
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        if (Global.GetInstance() != null)
        {
            this.transform.SetParent(Global.GetInstance().transform);
            Debug.Log(this.GetType().Name + " 初始化完成！");
        }
        else
        {
            Debug.LogWarning("Global对象为空!");
        }
        ///创建客户端监听对象
        peer = new PhotonPeer(this, ConnectionProtocol.Tcp);
        peer.Connect("111.200.241.253:4531", "EWorldGame");
        Global.Info("初始化NetManager");
    }


    public void DebugReturn(DebugLevel level, string message)
    {
        
    }
 

    public void OnEvent(EventData eventData)
    {
      
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {

        //获取模块ID
        byte  moudle = (byte)operationResponse.OperationCode;
        byte operation = (byte)operationResponse.Parameters[(byte)80];

        Handler handler = HandlerManager.GetInstance().FindHandler((byte)moudle, (byte)operation);
        if (handler != null)
        {
            handler.Parse(moudle, operation, operationResponse);
        }else
        {
            Global.Info("收到未知消息"+moudle+","+operation);
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                Global.Info("成功连接到服务器!"+peer.ServerAddress);
                break;
            case StatusCode.DisconnectByServer:
                Global.Info("被服务器端强制断开连接");
                break;
            case StatusCode.Disconnect:
                Global.Info("你已经掉线了");
                break;
        }
    }

    public void SendMessage(C2SMessage msg)
    {
        peer.OpCustom((byte)msg.moudleCode,msg,true);
    }


    // Update is called once per frame
    void Update () {
        //调用循环服务
        peer.Service();
	}


    void OnDestroy()
    {
        peer.Disconnect();
    }
}

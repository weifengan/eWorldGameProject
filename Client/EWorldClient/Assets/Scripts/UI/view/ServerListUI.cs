using Common;
using EWordProject.Logic;
using Pathfinding.Serialization.JsonFx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerListUI : BaseUI {

    //服务器选择事件
    public event ServerItemUI.SvrSelectHandler OnSelectHandler = null;
    private Transform m_content;

    private Text txtCurSeverName;
    protected override void OnAwake()
    {
        base.OnAwake();
        this.InitSkin("UI/ServerListUI");

        m_content = this.Skin.transform.Find("bg/serverlist/ScrollRect/viewpoint/content");
        txtCurSeverName = this.Skin.transform.Find("bg/mcurrent/name").GetComponent<Text>();
        txtCurSeverName.text = (Global.GetInstance().mCurrentServerInfo.id < 10 ? "0" + Global.GetInstance().mCurrentServerInfo.id : Global.GetInstance().mCurrentServerInfo.id + "") + ". " + Global.GetInstance().mCurrentServerInfo.title;
    }
    
    void OnEnable()
    {
        HandlerManager.GetInstance().Add((byte)Module.ServerList, (byte)ServerListOperation.FetchAllServer);
        LoadServerData();
    }

    /// <summary>
    /// 从服务器下载服务器数据
    /// </summary>
    public void LoadServerData()
    {

        HandlerManager.GetInstance().FindHandler((byte)Module.ServerList, (byte)ServerListOperation.FetchAllServer).OnParseHandler += ServerListUI_OnParseHandler1;
        //向服务器要数据
        C2SMessage msg = new C2SMessage((byte)Module.ServerList, (byte)ServerListOperation.FetchAllServer);
        NetManager.GetInstance().SendMessage(msg);

    }

    private void ServerListUI_OnParseHandler1(byte moduleCode, byte opCode, ExitGames.Client.Photon.OperationResponse response)
    {
        HandlerManager.GetInstance().Remove((byte)Module.ServerList, (byte)ServerListOperation.FetchAllServer);
        //获取服务器返回的信息
        string json = response.Parameters[(byte)1].ToString();

        //转为json
        ServerListVo list = JsonReader.Deserialize<ServerListVo>(json);



        ServerItemUI[] oldSvrs = m_content.GetComponentsInChildren<ServerItemUI>();
        foreach (var item in oldSvrs)
        {
            Destroy(item.gameObject);
        }
        

        //根据服务器数据生成服务器列表
        foreach(ServerItemVo svr in list.servers)
        {
            ServerItemUI mSvr = UIManager.GetInstance().GetNewUI<ServerItemUI>();
            mSvr.SetServerData(svr);
            mSvr.OnSelectHandler += MSvr_OnSelectHandler;
            mSvr.transform.SetParent(m_content);

        }

        //根据内容调整滚动区域大小
        RectTransform rtf = m_content as RectTransform;
        rtf.sizeDelta = new Vector2(561, Mathf.CeilToInt((float)list.servers.Length/2) * 90);

        ScrollRect sr = GameObject.FindObjectOfType<ScrollRect>();
        sr.verticalNormalizedPosition = 1;
    }

    /// <summary>
    /// 服务器被选中处理
    /// </summary>
    /// <param name="data"></param>
    private void MSvr_OnSelectHandler(BaseUI me,ServerItemVo data)
    {
        if (OnSelectHandler != null)
        {
            OnSelectHandler(this,data);
            this.gameObject.SetActive(false);
        }
    }

}

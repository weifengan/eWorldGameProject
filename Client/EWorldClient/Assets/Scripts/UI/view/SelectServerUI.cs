using Common;
using EWordProject.Logic;
using Pathfinding.Serialization.JsonFx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectServerUI : BaseUI {

    private ServerListUI mlist=null;

    private Text txtServerTitle;
    
    protected override void OnAwake()
    {
        base.OnAwake();
        this.InitSkin("UI/SelectServerUI");


        txtServerTitle = this.Skin.transform.Find("mServer/title").GetComponent<Text>();

        HandlerManager.GetInstance().Add((byte)Module.ServerList, (byte)ServerListOperation.FetchCurrentServer).OnParseHandler += SelectServerUI_OnParseHandler; ;
 
        C2SMessage msg = new C2SMessage((byte)Module.ServerList, (byte)ServerListOperation.FetchCurrentServer);
        NetManager.GetInstance().SendMessage(msg);
    }

    private void SelectServerUI_OnParseHandler(byte moduleCode, byte opCode, ExitGames.Client.Photon.OperationResponse response)
    {
        
        //处理获得当前服务器
        HandlerManager.GetInstance().Remove((byte)Module.ServerList, (byte)ServerListOperation.FetchAllServer);
        string json = response.Parameters[(byte)1].ToString();

        //Json序列化成类
        ServerItemVo vo = JsonReader.Deserialize<ServerItemVo>(json);

        //临时存储服务器信息
        Global.GetInstance().mCurrentServerInfo = vo;
        
        //显示界面
        txtServerTitle.text = (vo.id < 10 ? "0" + vo.id : vo.id+"")+ ". " + vo.title;
    }

    protected override void OnClickHandler(GameObject go)
    {
        base.OnClickHandler(go);
        switch (go.name)
        {
            case "btnEnterGame":
                SceneManager.GetInstance().GotoRealScene("MainCity",delegate() {

                    UIManager.GetInstance().SwitchScene("MainCityUI");


                });
                break;
            case "btnChange":
                if (mlist == null)
                {
                    mlist = UIManager.GetInstance().GetNewUI<ServerListUI>();
                    mlist.transform.SetParent(this.transform);
                    mlist.OnSelectHandler += Mlist_OnSelectHandler;
                }
                else
                {
                    mlist.gameObject.SetActive(true);
                }
                break;
        }
    }

    private void Mlist_OnSelectHandler(BaseUI me,ServerItemVo vo)
    {
        //显示界面
        txtServerTitle.text = (vo.id < 10 ? "0" + vo.id : vo.id + "") + ". " + vo.title;
        Destroy(me.gameObject);
        
    }
}

using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerItemUI : BaseUI {

    public delegate void SvrSelectHandler(BaseUI me, ServerItemVo data);
    public event SvrSelectHandler OnSelectHandler = null;


    private Text txtTitle;
    private Image mStatus;
    private ServerItemVo data;

   
    protected override void OnAwake()
    {
        base.OnAwake();
        this.InitSkin("UI/ServerItemUI");
        txtTitle = this.Skin.transform.Find("name").GetComponent<Text>();
        mStatus = this.Skin.transform.Find("status").GetComponent<Image>();

        this.Skin.transform.GetComponent<Button>().onClick.AddListener(OnClickHandler);
    }

    private void OnClickHandler()
    {
        if (OnSelectHandler != null)
        {
            OnSelectHandler(this,data);
        }
    }

    public void SetServerData(ServerItemVo vo)
    {
        data = vo;
        txtTitle.text =(vo.id<10? "0"+vo.id: vo.id+"")+". "+vo.title;
       
    }


}

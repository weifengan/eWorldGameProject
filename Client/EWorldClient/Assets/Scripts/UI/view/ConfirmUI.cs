﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmUI : BaseUI {

    public delegate void Complete(BaseUI me,bool flag=false);
    public event Complete onClosedHandler = null;
    private Text mTxtTitle;
    private Text mTxtContent;
    private Text mTime;
    //停留时间
    public int displayTime = 5;
    protected override void OnAwake()
    {
        base.OnAwake();
        this.InitSkin("UI/ConfirmUI");
        mTxtTitle = this.GetComponentByName<Text>("title");
        mTxtContent = this.GetComponentByName<Text>("content");
        mTime = this.GetComponentByName<Text>("time");
        StartCoroutine(wait2closed());
    }

    private IEnumerator wait2closed()
    {
        displayTime = 5;
        while (true)
        {
            mTime.text = displayTime.ToString();
            yield return new WaitForSeconds(1);
            displayTime -= 1;
            if (displayTime <= 0)
            {
                Destroy(this.gameObject);
                break;
            }
        }
    }

    /// <summary>
    /// 设置标题和消息内容
    /// </summary>
    /// <param name="content"></param>
    /// <param name="title"></param>
    public void SetData(string content, string title = "系统消息")
    {
        mTxtTitle.text = title;
        mTxtContent.text = content;
    }



    protected override void OnClickHandler(GameObject go)
    {
        base.OnClickHandler(go);
        switch (go.name)
        {
            case "btnOk":
                if (onClosedHandler != null)
                {
                    onClosedHandler(this,true);
                }
                Destroy(this.gameObject);
                break;
            case "btnCancel":
                if (onClosedHandler != null)
                {
                    onClosedHandler(this,false);
                }
                Destroy(this.gameObject);
                break;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : BaseUI {
    private Image barImg;
    private Text txtPro;
    private float per = 0;


    public event SceneLoadComplete OnComplete = null;


    void OnEnable()
    {
        RectTransform rtf = this.transform as RectTransform;
        rtf.SetAsLastSibling();
    }
    public float Percent
    {
        get { return per; }
        set {
            per =Mathf.Clamp01(value);
            txtPro.text = Mathf.Ceil(per * 100).ToString() + "%";
            barImg.fillAmount = per;
            if(per>=1 && OnComplete != null)
            {
                OnComplete();
            }
        }
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        this.InitSkin("UI/LoadingUI");
        //this.InitSkin("UI/LoadingUI");
        barImg = this.Skin.transform.Find("loadingBar/bar").GetComponent<Image>();
        txtPro= this.Skin.transform.Find("loadingBar/pro").GetComponent<Text>();
    }
 
}

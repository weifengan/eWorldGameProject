using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : BaseUI {
    private Image barImg;
    private Text txtPro;
    private float per = 0;
    protected override void OnAwake()
    {
        base.OnAwake();
        this.InitSkin("UI/LoadingUI");
        //this.InitSkin("UI/LoadingUI");
        barImg = this.Skin.transform.Find("loadingBar/bar").GetComponent<Image>();
        txtPro= this.Skin.transform.Find("loadingBar/pro").GetComponent<Text>();
    }

    protected override void OnStart()
    {
        base.OnStart();
        StartCoroutine(doLoading());
    }

  
    private IEnumerator doLoading()
    {
        while (per < 1)
        {
            per += Time.deltaTime*2;
            txtPro.text = Mathf.CeilToInt(100 * per)+ "%";
            barImg.fillAmount = per;
            yield return new WaitForSeconds(0.05f);
        }
        barImg.fillAmount = 1;
        txtPro.text = "100%";

        UIManager.GetInstance().SwitchScene(this.args[0].ToString());

    }
}

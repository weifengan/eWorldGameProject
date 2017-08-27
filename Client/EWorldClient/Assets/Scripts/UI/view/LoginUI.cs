using Common;
using EWordProject.Logic;
using Pathfinding.Serialization.JsonFx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : BaseUI {

    private InputField mInputUser;
    private InputField mInputPwd;
    protected override void OnAwake()
    {
        base.OnAwake();
        this.InitSkin("UI/LoginUI");
        mInputUser = this.GetComponentByName<InputField>("InputUser");
        mInputPwd = this.GetComponentByName<InputField>("InputPwd");

        HandlerManager.GetInstance().Add((byte)Module.Login, (byte)LoginOperation.UserLogin).OnParseHandler += OnLoginResponse;
    }

    private void OnLoginResponse(byte moduleCode, byte opCode, ExitGames.Client.Photon.OperationResponse response)
    {
        string json = response.Parameters[1].ToString();

        LoginResultVo vo = JsonReader.Deserialize<LoginResultVo>(json);
        if (vo.result==1)
        {
            UIManager.GetInstance().SwitchScene("SelectServerUI");
            Global.Info("登录成功！");
        }
        else
        {
            
            Global.Info("登录失败!");
        }
    }

    protected override void OnClickHandler(GameObject go)
    {
        base.OnClickHandler(go);
        switch (go.name)
        {
            case "btnPlay":

                C2SMessage msg = new C2SMessage((byte)Module.Login,(byte)LoginOperation.UserLogin);
                msg.Add(1, mInputUser.text.Trim());
                msg.Add(2, mInputPwd.text.Trim());

                NetManager.GetInstance().SendMessage(msg);
                break;

            case "btnReg":
                UIManager.GetInstance().Alert("请到官网去注册账号!");
                break;
        }
    }
}

using Common;
using EWordProject.Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Global : MonoBehaviour {

    #region 单例类实现

    private static Global _instance = null;
    public static Global GetInstance()
    {
        return _instance;
    }

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            //调用初始化
            Init();
            DontDestroyOnLoad(this.gameObject);
        } else if (_instance != this.gameObject)
        {
            Destroy(this.gameObject);
            return;
        }
    }
    #endregion


    #region 变量

    public Transform m_UIRoot;

    public ServerItemVo mCurrentServerInfo;

    #endregion
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        //初始化UI的根容器
        m_UIRoot = this.transform.Find("UI");

        mCurrentServerInfo = new ServerItemVo();

        //初始化管理器 
        HandlerManager.GetInstance().Init();
       
        UIManager.GetInstance().Init();
        ResManager.GetInstance().Init();
        UIManager.GetInstance().SwitchScene("LoadingUI","LoginUI");

        NetManager.GetInstance().Init();



    }

    /// <summary>
    /// 向控制台输出内容
    /// </summary>
    /// <param name="objs"></param>
    public static void Info(params object[] objs)
    {
        string info = "";
        foreach (var item in objs)
        {
            info += item.ToString() + "  ";
        }
        Debug.Log(info);
    }
}

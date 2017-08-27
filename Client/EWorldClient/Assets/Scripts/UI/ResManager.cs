/**************************************
*  文 件 名: ResManager
*  描    述：
*  作　　者: 魏凤安
*  Q     Q: 1327797237
*  手机号码: 17746514110
*  电子邮箱: wefengan@163.com
*  博客地址: http://www.weifengan.com/
**************************************/
using UnityEngine;
using System.Collections;
using System;

public class ResManager : MonoBehaviour {

    private static ResManager _instance = null;

    public static ResManager GetInstance()
    {
        if (_instance == null)
        {
            GameObject go = new GameObject("_ResManager");
            _instance = go.AddComponent<ResManager>();
        }
        return _instance;
    }


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
       
    }
    //通过名称加载游戏对象
    public GameObject CreateGameObject(string path)
    {
        try
        {
            GameObject go = Resources.Load<GameObject>(path);
            GameObject ui = Instantiate(go);
            return ui;
        }
        catch
        {
            Global.Info("未找到皮肤资源" + path);
            return null;
        }
    }

}

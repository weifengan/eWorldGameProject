using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景加载完成时回调委托类型
/// </summary>
public delegate void SceneLoadComplete();

public class SceneManager : MonoBehaviour {


    public LoadingUI mLoading;
    private SceneManager()
    {

    }

    private static SceneManager _instance = null;
    public static SceneManager GetInstance()
    {
        if (_instance == null)
        {
            GameObject go = new GameObject("_SceneManager");
            _instance = go.AddComponent<SceneManager>();
        }
        return _instance;
    }


    public void Init()
    {
        if (Global.GetInstance() != null)
        {
            mLoading = UIManager.GetInstance().GetNewUI<LoadingUI>();
            this.transform.SetParent(Global.GetInstance().transform);
        }
        else
        {
            Debug.LogWarning("Global对象为空!");
        }
    }


    private AsyncOperation async;

    /// <summary>
    /// 跳转真是场景
    /// </summary>
    /// <param name="sceneName">要设置的场景名称</param>
    /// <param name="OnCallBack">要跳转场景加载完成后的回调函数</param>
    public void GotoRealScene(string sceneName, SceneLoadComplete OnCallBack = null)
    {
        mLoading.gameObject.SetActive(true);
        StartCoroutine(doLoadRealScene(sceneName, OnCallBack));
        

    }

    /// <summary>
    /// 跳转UI场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="OnCallBack"></param>
    public void GotoUIScene(string sceneName,SceneLoadComplete OnCallBack=null,params object[] args)
    {
        StartCoroutine(doLoadUIScene(sceneName, OnCallBack, args));
        mLoading.gameObject.SetActive(true);
    }

    /// <summary>
    /// 快速跳转UI场景（无加载界面)
    /// </summary>
    /// <param name="sceneName"></param>
    public void QuickGotoUIScene(String sceneName)
    {
        UIManager.GetInstance().SwitchScene(sceneName);
    }

    private IEnumerator doLoadUIScene(string sceneName, SceneLoadComplete onCallBack = null, params object[] args)
    {
        mLoading.Percent = 0;
        while (mLoading.Percent < 1)
        {
            mLoading.Percent += Time.deltaTime;
            yield return new WaitForSeconds(0.05f);
        }
        mLoading.Percent = 1;
        UIManager.GetInstance().SwitchScene(sceneName,args);
        mLoading.gameObject.SetActive(false);
        if (onCallBack != null) onCallBack();

    }

    private IEnumerator doLoadRealScene(string sceneName, SceneLoadComplete onCallBack=null)
    {
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
       
        while (async.progress<0.9)
        {
            mLoading.Percent = async.progress;
            print(async.progress);
            yield return new WaitForSeconds(0.01f);
            
        }
        if (async.isDone)
        {
            mLoading.Percent = 1;
            if (onCallBack != null)
            {
                onCallBack();
            }
           
            mLoading.gameObject.SetActive(false);
            
        }

    }

    


}

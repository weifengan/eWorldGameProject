/**************************************
*  文 件 名: SceneManager
*  描    述：
*  作　　者: 魏凤安
*  Q     Q: 1327797237
*  手机号码: 17746514110
*  电子邮箱: wefengan@163.com
*  博客地址: http://www.weifengan.com/
**************************************/
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public UIManager()
    {

    }

    private static UIManager _instance = null;
    public static UIManager GetInstance()
    {
        if (_instance == null)
        {
            GameObject go = new GameObject("_UIManager");
            _instance = go.AddComponent<UIManager>();
        }
        return _instance;
    }

    public GameObject current;
    public Transform m_UIRoot;


    public void Init()
    {
        if (Global.GetInstance() != null)
        {
            this.transform.SetParent(Global.GetInstance().transform);
            m_UIRoot = Global.GetInstance().m_UIRoot;
        }
        else
        {
            Debug.LogWarning("Global对象为空!");
        }
    }
    /// <summary>
    /// 参数
    /// </summary>
    /// <param name="name">场景名称</param>
    /// <param name="args">参数</param>
    public void SwitchScene(string name, params object[] args)
    {
        // GameObject scene = ResManager.GetInstance().CreateGameObject("UI/" + name);
        GameObject scene = new GameObject(name);
        RectTransform rtf = scene.AddComponent<RectTransform>();
        //设置UI容器大小
        rtf.anchorMin = Vector2.zero;
        rtf.anchorMax = Vector2.one;
        rtf.offsetMin = new Vector2(0, 0);
        rtf.offsetMax = new Vector2(0, 0);
        rtf.sizeDelta = new Vector2(Screen.width, Screen.height);

        BaseUI bi = scene.AddComponent(Type.GetType(name)) as BaseUI;
        bi.args = args;
        if (current != null)
        {
            GameObject.Destroy(current);
        }

        if (m_UIRoot == null)
        {
            m_UIRoot = GameObject.FindObjectOfType<Canvas>().transform;
        }

        scene.transform.SetParent( m_UIRoot);
        scene.transform.localScale = Vector3.one;
        scene.transform.localPosition = Vector3.zero;


        current = scene;
    }


    public T GetNewUI<T>() where T : BaseUI
    {
        GameObject go = new GameObject(typeof(T).Name);
        RectTransform rtf = go.AddComponent<RectTransform>();
        rtf.SetParent(m_UIRoot);
        rtf.anchoredPosition = Vector2.zero;
        T tmp = go.AddComponent<T>();

        // RectTransform rtf = tmp.transform as RectTransform;
        //rtf.transform.position = Vector3.one;
        return tmp;
    }

}

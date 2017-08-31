using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoleUI : BaseUI {


    enum RoleType
    {
        Warrior=0,Magic,Ancher
    }

    private Transform[] tran_normal;
    private Transform[] tran_over;
    private Transform[] models;
    private Text mDescTitle;
    private Text mDescBody;

    protected override void OnAwake()
    {
        base.OnAwake();
        this.InitSkin("UI/CreateRoleUI");

        //初始化角色类型按钮
        tran_normal = new Transform[3];
        tran_normal[0] = this.Skin.transform.Find("jobBtns/Job_ZhanShi/normal");
        tran_normal[1] = this.Skin.transform.Find("jobBtns/Job_magic/normal");
        tran_normal[2] = this.Skin.transform.Find("jobBtns/Job_arrow/normal");
        tran_over = new Transform[3];
        tran_over[0] = this.Skin.transform.Find("jobBtns/Job_ZhanShi/over");
        tran_over[1] = this.Skin.transform.Find("jobBtns/Job_magic/over");
        tran_over[2] = this.Skin.transform.Find("jobBtns/Job_arrow/over");

        models = new Transform[3];
        models[0]= this.Skin.transform.Find("Models/model_zhanshi");
        models[0].gameObject.AddComponent<CreateRoleAniController>();
        models[1] = this.Skin.transform.Find("Models/model_jingling");
        models[1].gameObject.AddComponent<CreateRoleAniController>();
        models[2] = this.Skin.transform.Find("Models/model_ancher");
        models[2].gameObject.AddComponent<CreateRoleAniController>();

        mDescTitle = this.GetComponentByName<Text>("descTitle");
        mDescBody = this.GetComponentByName<Text>("descBody");
        LoadConfig();
      
    }

    private XmlDocument mXml;
    private void LoadConfig()
    {
        TextAsset txt = Resources.Load<TextAsset>("Config/JobConfig");
        mXml = new XmlDocument();
        mXml.LoadXml(txt.text);
        DisplayRole(RoleType.Warrior);
    }

    protected override void OnClickHandler(GameObject go)
    {
        base.OnClickHandler(go);
        switch (go.name)
        {
            case "Job_ZhanShi":

                DisplayRole(RoleType.Warrior);

                break;
            case "Job_magic":
                DisplayRole(RoleType.Magic);
                break;
            case "Job_arrow":
                DisplayRole(RoleType.Ancher);
                break;
        }
       
    }


    void DisplayRole(RoleType type)
    {
        this.Skin.transform.Find("Models").eulerAngles = new Vector3(0, -180, 0);
        for (int i = 0; i < tran_normal.Length; i++)
        {
            this.tran_normal[i].gameObject.SetActive(true);
            this.tran_over[i].gameObject.SetActive(false);
            this.models[i].gameObject.SetActive(false);
        }
        this.tran_normal[(int)type].gameObject.SetActive(false);
        this.tran_over[(int)type].gameObject.SetActive(true);
        this.models[(int)type].gameObject.SetActive(true);

        XmlNodeList list = mXml.DocumentElement.SelectNodes("job");

        XmlNode node = list[(int)type];
      

        mDescTitle.text = node.SelectSingleNode("title").InnerText;

        string info = "";
        info += node.SelectSingleNode("desc/weapon").InnerText + "\n";
        info += node.SelectSingleNode("desc/family").InnerText + "\n";
        info += node.SelectSingleNode("desc/advance").InnerText + "\n";
        info += node.SelectSingleNode("desc/skil").InnerText + "\n";
        mDescBody.text = info;



    }


   






}

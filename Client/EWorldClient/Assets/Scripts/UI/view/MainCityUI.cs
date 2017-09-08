using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCityUI : BaseUI {
    protected override void OnAwake()
    {
        base.OnAwake();
        this.InitSkin("UI/MainCityUI");
    }
}

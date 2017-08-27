using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2SMessage:Dictionary<byte,object> {
    public byte moudleCode;
    public byte operationCode;
    
    public C2SMessage(byte moudleCode,byte operationCode):base()
    {
        this.moudleCode = moudleCode;
        this.operationCode = operationCode;
        //添加操作码
        this.Add(80, operationCode);
    }

    public override string ToString()
    {
        string info = "";
        foreach (byte  item in this.Keys)
        {
            info += item + " =" + this[item]+ " | ";
        }
        return info;
        
    }




}

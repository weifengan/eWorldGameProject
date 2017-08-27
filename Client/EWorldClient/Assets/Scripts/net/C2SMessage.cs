using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2SMessage:Dictionary<byte,object> {
    public Enums.MoudleCode moudleCode;
    public Enums.OperationCode operationCode;
    
    public C2SMessage(Enums.MoudleCode moudleCode,Enums.OperationCode operationCode):base()
    {
        this.moudleCode = moudleCode;
        this.operationCode = operationCode;
        //添加操作码
        this.Add(80, operationCode);
    }

    public void AddField(byte id,object value)
    {
        this.Add(id, value);
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

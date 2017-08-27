using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWorldServer.net.c2s
{
    class S2CMessage: Dictionary<byte,object>
    {
        public byte moudleCode;
        public byte operationCode;

        public S2CMessage(byte moudleCode, byte operationCode) : base()
        {
            this.moudleCode = moudleCode;
            this.operationCode = operationCode;
            //添加操作码
            this.Add(80, operationCode);
        }
 
        public override string ToString()
        {
            string info = "";
            foreach (byte item in this.Keys)
            {
                info += item + " =" + this[item] + " | ";
            }
            return info;
        }
    }
}

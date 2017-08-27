using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



/**********************************************
      HandlerManager 
  
      @author: 魏凤安
      @email: weifengan@163.com
	  @QQ: 1327707237
	  @date: 2017/8/12 1:26:34
      @desc:消息管理器
  
***********************************************/

namespace EWordProject.Logic
{
    class HandlerManager
    {
        private static HandlerManager _instance = null;
        public static HandlerManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new HandlerManager();
            }
            return _instance;
        }
        private Dictionary<byte, Dictionary<byte, Handler>> mDic;

        private HandlerManager()
        {
        }

        public void Init()
        {
            mDic = new Dictionary<byte, Dictionary<byte, Handler>>();
            Global.Info(this.GetType().Name + " 初始化成功!");
        }

        /// <summary>
        /// 向消息处理器中添加处理消息对象 
        /// </summary>
        /// <param name="moudleCode">模块码</param>
        /// <param name="opCode">操作码</param>
        /// <param name="handler">消息处理对象</param>
        /// <returns></returns>
        public Handler Add(byte moudleCode, byte opCode)
        {
            Dictionary<byte, Handler> oplist;
            Handler tempHandler = null;
            if (!mDic.ContainsKey(moudleCode))
            {
                oplist = new Dictionary<byte, Handler>();
                tempHandler = new Handler();
                oplist.Add(opCode, tempHandler);
                mDic.Add(moudleCode, oplist);
                return tempHandler;
            }
            else
            {
                oplist = mDic[moudleCode];
                if (!oplist.ContainsKey(opCode))
                {
                    tempHandler = new Handler();
                    oplist.Add(opCode, tempHandler);
                    return tempHandler;
                }
            }
            return null;
        }


        /// <summary>
        /// 从消息处理器中删除消息处理对象 
        /// </summary>
        /// <param name="moudleCode">模块码</param>
        /// <param name="opCode">操作码</param>
        /// <returns></returns>

        public bool Remove(byte moudleCode, byte opCode)
        {
            Dictionary<byte, Handler> oplist;
            if (mDic.ContainsKey(moudleCode))
            {
                oplist = mDic[moudleCode];
                if (oplist.ContainsKey(opCode))
                {
                    oplist.Remove(opCode);
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 通过模块和操作码查找处理Handler
        /// </summary>
        /// <param name="moudleCode"></param>
        /// <param name="opCode"></param>
        /// <returns></returns>
        public Handler FindHandler(byte  moudleCode, byte opCode)
        {
            Dictionary<byte, Handler> oplist;
            if (mDic.ContainsKey(moudleCode))
            {
                oplist = mDic[moudleCode];
                if (oplist.ContainsKey(opCode))
                {
                    return oplist[opCode];
                }
                return null;
            }
            return null;
        }
    }
}

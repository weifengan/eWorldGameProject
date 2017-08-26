using Common;
using EWorldServer.net.c2s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWorldServer.net
{
    class HandlerManager
    {
        //消息字典
        private Dictionary<byte, Dictionary<byte, IHandler>> mDic;

        #region 单例模式实现
        private static HandlerManager _instance = null;
        public static HandlerManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new HandlerManager();
            }
            return _instance;
        }

        private HandlerManager()
        {
        }

        #endregion


        public void Init()
        {
            mDic = new Dictionary<byte, Dictionary<byte, IHandler>>();

            //注册消息
            Add((byte)Module.ServerList, (byte)ServerListOperation.FetchCurrentServer, new C2SFetchCurrentServer());
           // Add(Module.ServerList, ServerListOperation.FetchAllServer, new C2SFetchServerList());

           // Add(Module.Login, LoginOperation.UserLogin, new C2SUserLogin());
            Global.Info("【OK】HandlerManager 初始化成功!");
        }

        /// <summary>
        /// 向消息处理器中添加处理消息对象 
        /// </summary>
        /// <param name="moudleCode">模块码</param>
        /// <param name="opCode">操作码</param>
        /// <param name="handler">消息处理对象</param>
        /// <returns></returns>
        public bool Add(byte moudleCode, byte opCode, IHandler handler)
        {
            Dictionary<byte, IHandler> oplist;
            if (!mDic.ContainsKey(moudleCode))
            {
                oplist = new Dictionary<byte, IHandler>();
                oplist.Add(opCode, handler);
                mDic.Add(moudleCode, oplist);
                return true;
            }
            else
            {
                oplist = mDic[moudleCode];
                if (!oplist.ContainsKey(opCode))
                {
                    oplist.Add(opCode, handler);
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 从消息处理器中删除消息处理对象 
        /// </summary>
        /// <param name="moudleCode">模块码</param>
        /// <param name="opCode">操作码</param>
        /// <returns></returns>

        public bool Remove(byte moudleCode, byte opCode)
        {
            Dictionary<byte, IHandler> oplist;
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
        public IHandler FindHandler(byte moudleCode, byte opCode)
        {
            Dictionary<byte, IHandler> oplist;
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

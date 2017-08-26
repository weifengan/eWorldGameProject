using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**********************************************
      Account 
  
      @author: 魏凤安
      @email: weifengan@163.com
	  @QQ: 1327707237
	  @date: 2017/8/27 1:36:12
  
***********************************************/

namespace EWorldServer.db.model
{
    /// <summary>
    /// 账户类型结构
    /// </summary>
    public class Account
    {
        public virtual int acc_id { get; set; }
        public virtual string acc_name { get; set; }
        public virtual string acc_pwd { get; set; }

        public virtual string acc_nick { get; set; }

        public virtual int acc_jobid { get; set; }
        public virtual int acc_level { get; set; }

        public virtual int acc_gold { get; set; }

        public virtual DateTime acc_regdate { get; set; }

        public virtual DateTime acc_lastlogin { get; set; }
    }
}

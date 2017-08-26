using EWorldServer.db.model;
using FluentNHibernate.Mapping;


/**********************************************
      AccountMapping 
  
      @author: 魏凤安
      @email: weifengan@163.com
	  @QQ: 1327707237
	  @date: 2017/8/27 1:41:33
  
***********************************************/

namespace EWorldServer.db.mapping
{
    /// <summary>
    /// 数据库映射类
    /// </summary>
    //数据库表格映射对象
    public class AccountMapping : ClassMap<Account>
    {
        public AccountMapping()
        {
            //1、设置ID属性为主键
            //2、x表示一个Account的对象
            Id(x => x.acc_id).Column("acc_id");
            Map(x => x.acc_name).Column("acc_name");
            Map(x => x.acc_pwd).Column("acc_pwd");
            Map(x => x.acc_nick).Column("acc_nick");
            Map(x => x.acc_jobid).Column("acc_jobid");
            Map(x => x.acc_level).Column("acc_level");
            Map(x => x.acc_gold).Column("acc_gold");
            Map(x => x.acc_regdate).Column("acc_regdate");
            Map(x => x.acc_lastlogin).Column("acc_lastlogin");
            Table("account");
        }

    }
}

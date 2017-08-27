using EWorldServer.db.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**********************************************
      AccountAction 
  
      @author: 魏凤安
      @email: weifengan@163.com
	  @QQ: 1327707237
	  @date: 2017/8/27 2:13:52
  
***********************************************/

namespace EWorldServer.db.action
{
    public class AccountAction
    {
        //获取数据库中所有用户信息
        public IList<Account> GetAllAccount()
        {
            using (var session = DBManager.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userlist = session.QueryOver<Account>();
                    transaction.Commit();
                    return userlist.List();
                }
            }
        }

        public Account CheckLogin(string name, string pwd)
        {
            using (var session = DBManager.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userList = session.QueryOver<Account>().Where(x => x.acc_name == name);
                    transaction.Commit();
                    if (userList.List().Count > 0)
                    {
                        Account account = userList.List()[0];
                        if (account.acc_pwd == pwd)
                        {
                            return account;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}

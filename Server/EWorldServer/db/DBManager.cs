using Common;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using LitJson;
using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWorldServer.db
{
    
    /// <summary>
    /// 数据库管理类
    /// </summary>
    public class DBManager
    {
        public static DBConfigVo mConfig;
        #region 单例模式
        private static DBManager _instance = null;
        private DBManager() { }

        public static DBManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DBManager();
            }
            return _instance;
        }
        #endregion

        private static ISessionFactory sessionFactory = null;

        private static void InitializeSessionFactory()
        {
            sessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(db => db
               .Server(mConfig.host)
               .Database(mConfig.dbname)
               .Username(mConfig.dbuser)
               .Password(mConfig.dbpwd)))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DBManager>())
                //.ExposeConfiguration(e => e.Properties.Add("hbm2ddl.keywords", "none"))
                .BuildSessionFactory();
        }

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                    InitializeSessionFactory();

                return sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public void Init()
        {
            try
            {
                //读取配置文件
                string json = File.ReadAllText(Global.AppRootFolder + "\\dbconfig.json");
                mConfig = JsonMapper.ToObject<DBConfigVo>(json);
                Global.Info(string.Format("【OK】初始化数据库管理类 {0} 成功!", DBManager.mConfig.dbname));
            }
            catch (Exception e)
            {
                Global.Info("【Error】数据库初始化失败!" + e.Message);
            }

        }
    }
}

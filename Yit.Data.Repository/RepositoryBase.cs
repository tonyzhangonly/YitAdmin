using SqlSugar;
using System;
using Yit.Util;

namespace Yit.Data.Repository
{
    /// <summary>
    /// SqlSugar仓储
    /// </summary>
    public class RepositoryBase<T> : SimpleClient<T> where T : class, new()
    {
        /// <summary>
        /// 创建仓储
        /// </summary>
        /// <param name="context"></param>
        public RepositoryBase(ISqlSugarClient context = null) : base(context)//注意这里要有默认值等于null
        {
            string dbType = GlobalContextUtil.SystemConfig.DBProvider;
            string dbConnectionString = GlobalContextUtil.SystemConfig.DBConnectionString;
            object sugarDbType = null;
            var isSugarDb = Enum.TryParse(typeof(DbType), dbType, out sugarDbType);
            if (!isSugarDb)
            {
                throw new Exception("不支持数据库连接类型，连接类型为 MySql,SqlServer,Sqlite,Oracle,PostgreSQL,Dm,Kdbndp");
            }
            if (context == null)
            {
                base.Context = new SqlSugarClient(new ConnectionConfig()
                {
                    DbType = (DbType)sugarDbType,
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true,
                    ConnectionString = dbConnectionString
                });
            }
        }
    }
}

using System;
using _91_app_test.Common.Settings;
using Microsoft.Extensions.Configuration;

namespace _91_app_test.Common
{
    public interface IDBConfig
    {
        string GetLocalDBConnStr();
    }
    public class DBConfig : IDBConfig
    {
        private IConfiguration _config;

        public DBConfig(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// 取得db連線字串
        /// </summary>
        /// <returns></returns>
        public string GetLocalDBConnStr()
        {
            return GetConnStr("TestDB");
        }

        private string GetConnStr(string section)
        {
            var setting = new DbSetting();

            _config.GetSection(section).Bind(setting);

            return $"Data Source={setting.HOSTNAME},{setting.PORT};Initial Catalog={setting.DB_NAME};" +
                $"User ID={setting.USERNAME};Password={setting.PASSWORD};" +
                $"MinimumPoolSize={setting.MIN_CONN};MaximumPoolSize={setting.MAX_CONN};Allow User Variables=True";

        }

    }
}

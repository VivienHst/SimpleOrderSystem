using System;
using _91_app_test.Common;
using _91_app_test.Common.Tool;
using _91_app_test.Models;
using _91_app_test.Models.DBData;
using _91_app_test.Models.FormModel;
using Dapper;

namespace _91_app_test.Services
{
    public interface IAccountService
    {
        int AddUser(AccountForm account);
        UserData GetUser(string account);
        bool CheckAccountExists(string account);
    }

    public class AccountService : IAccountService
    {
        private IDatabaseService _databaseService;
        private IDBConfig _dbConfig;

        public AccountService(IDatabaseService databaseService, IDBConfig dbConfig)
        {
            _databaseService = databaseService;
            _dbConfig = dbConfig;

        }


        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int AddUser(AccountForm account)
        {
            // 取得字尾
            string salt = PasswordTools.GetRandomString(10);
            // 取得加密字串
            string encryptPassword = PasswordTools.GetSHA256Encrypt(account.password + salt);

            UserData userData = new UserData()
            {
                Account = account.account,
                Password = encryptPassword,
                Salt = salt
            };
            string sqlStatement = " insert into TB_User (Account, Password, Salt, CreateDate, Status) " +
                " values (@Account, @Password, @Salt, now(), 1)";
             return _databaseService.Insert(
                 new DatabaseObject(_dbConfig.GetLocalDBConnStr(), sqlStatement, userData));

        }

        public bool CheckAccountExists(string account)
        {
            string sqlStatement = "select exists(select account from TB_User where account = @in_account)";
            var _params = new DynamicParameters();
            _params.Add("@in_account", account, System.Data.DbType.String, size: 20);
            return _databaseService.SingleQuery<bool>(
                 new DatabaseObject(_dbConfig.GetLocalDBConnStr(), sqlStatement, _params));
        }

        /// <summary>
        /// 取得用戶資訊
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public UserData GetUser(string account)
        {
            string sqlStatement = "select * from TB_User where account = @in_account";
            var _params = new DynamicParameters();
            _params.Add("@in_account", account, System.Data.DbType.String, size: 20);
            return _databaseService.SingleQuery<UserData>(
                 new DatabaseObject(_dbConfig.GetLocalDBConnStr(), sqlStatement, _params));
        }
    }
}

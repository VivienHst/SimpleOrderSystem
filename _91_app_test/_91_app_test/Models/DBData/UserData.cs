using System;
namespace _91_app_test.Models.DBData
{
    public class UserData
    {
        public UserData()
        {
        }
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 加密字尾
        /// </summary>
        public string Salt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _91_app_test.Common.Tool;
using _91_app_test.Models;
using _91_app_test.Models.DBData;
using _91_app_test.Models.FormModel;
using _91_app_test.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _91_app_test.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register([FromBody] AccountForm account)
        {
            // 檢查參數
            if (!ModelState.IsValid)
            {
                return Json(new ResponseModel(-2, ModelState.Values.ToString()));
            }
            // 檢查帳號有沒有重複
            if (_accountService.CheckAccountExists(account.account))
            {
                return Json(new ResponseModel(-1, "Account has exists"));
            }

            _accountService.AddUser(account);

            return Json(new ResponseModel(account));
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login([FromBody] AccountForm account)
        {
            UserData user = _accountService.GetUser(account.account);
            // 檢查帳號有沒有存在
            if(user == null) return Json(new ResponseModel(-1, "Account not exists"));
            string encryptPassword = PasswordTools.GetSHA256Encrypt(account.password + user.Salt);
            // 檢查密碼對不對
            if (!encryptPassword.Equals(user.Password)) return Json(new ResponseModel(-1, "Password error"));
            return Json(new ResponseModel(account));
        }

    }
}

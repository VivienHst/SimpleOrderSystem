using System;
using System.ComponentModel.DataAnnotations;

namespace _91_app_test.Models.FormModel
{
    public class AccountForm
    {
        public AccountForm()
        {
        }
        /// <summary>
        /// 帳號
        /// </summary>
        [Required(ErrorMessage = "Required {0}")]
        public string account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Required(ErrorMessage = "Required {0}")]
        public string password { get; set; }

    }
}

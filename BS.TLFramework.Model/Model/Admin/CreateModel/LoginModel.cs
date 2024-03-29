﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.TLFramework.Model.Admin
{
    public class LoginModel
    {
        /// <summary>
        /// 商户名
        /// </summary>
        [Required(ErrorMessage = "商户名不能为空")]
        public string MerchantCode { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserCode { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        //[StringLength(16, MinimumLength = 5,ErrorMessage="长度必须在5-16位之间")]
        public string Password { get; set; }

        /// <summary>
        /// 记住我
        /// </summary>
        public bool IsRememberMe { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class CurrentUser
    {
        public CurrentUser()
        {
            AllAction = new List<ActionModel>();
            CurrnetAction = new List<string>();
        }

        public long ID { get; set; }

        public string UserCode { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsRememberMe { get; set; }
        public bool IsCustomer { get; set; }

        public bool IsAdmin { get; set; }
        /// <summary>
        ///  1 管理员登陆   2 培训师  3 学生
        /// </summary>
        public int LoginUserType { get; set; }
        public long MerchantID { get; set; }

        public List<ActionModel> AllAction { get; set; }

        public List<string> CurrnetAction { get; set; }

        public string openID { get; set; }
        /// <summary>
        /// 分店ID
        /// </summary>
        public long BranchesID { get; set; }

        public string Email { get; set; }
        public string Mobile { get; set; }
        public int AccountBank { get; set; }
    }
}

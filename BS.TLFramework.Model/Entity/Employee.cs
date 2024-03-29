﻿using Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.TLFramework.Model
{
    public class Employee : BaseEntity
    {

        [Display(Name = "商户ID")]
        [Required(ErrorMessage = "{0}不能为空")]
        public long MerchantID { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Display(Name = "账号")]
        [Required(ErrorMessage = "{0}不能为空")]
        [MinLength(2, ErrorMessage = "{0}不能少于2位")]
        [MaxLength(16, ErrorMessage = "{0}不能多于16位")]
        public string Code { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "{0}不能为空")]
        [MinLength(2, ErrorMessage = "{0}不能少于2位")]
        [MaxLength(64, ErrorMessage = "{0}不能多于32位")]
        public string Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "Email")]
        [MaxLength(64, ErrorMessage = "{0}不能多于32位")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [Display(Name = "手机")]
        [MinLength(11, ErrorMessage = "{0}必须是11位数字")]
        [MaxLength(11, ErrorMessage = "{0}必须是11位数字")]
        public string Mobile { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [MaxLength(32, ErrorMessage = "{0}不能多于32位")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Password { get; set; }

        public int IsAdmin { get; set; }

        /// <summary>
        /// 查看更多
        /// </summary>
        [Display(Name = "查看更多")]
        public int IsMore { get; set; }

        /// <summary>
        /// 权限的部门ID集合
        /// </summary>
        [Display(Name = "权限的部门ID集合")]
        public string AuthDepartmentIDs { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long? EmployeeRoleID { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long? EmployeeDepartmentID { get; set; }

        //----------------------------------------------------------------------

        public virtual Merchant Merchant { get; set; }

        public string MerchantCode { get { return Merchant.Code; } }
        public string MerchantName { get { return Merchant.Name; } }

        public virtual EmployeeRole EmployeeRole { get; set; }
        public string EmployeeRoleName { get { return EmployeeRole?.Name; } }

        public virtual EmployeeDepartment EmployeeDepartment { get; set; }
        public string EmployeeDepartmentName { get { return EmployeeDepartment?.Name; } }

        public string StatusDesc { get { return ((EmployeeStatus)this.Status).ToDescription(); } }

        [NotMapped]
        public IList<long> MenuActionList { get { return EmployeeRole?.MenuActionList ?? new List<long>(); } }

        public List<long?> AuthDepartmentIDList
        {
            get
            {
                return AuthDepartmentIDs.ToLongList();
            }
        }

    }
}

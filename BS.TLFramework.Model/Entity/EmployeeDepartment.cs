﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.TLFramework.Model
{
    public class EmployeeDepartment : BaseEntity
    {

        [Display(Name = "商户ID")]
        [Required(ErrorMessage = "{0}不能为空")]
        public long MerchantID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [MinLength(2, ErrorMessage = "{0}不能少于2位")]
        [MaxLength(16, ErrorMessage = "{0}不能多于16位")]
        [Index(IsUnique = true)]
        public string Name { get; set; }


        /// <summary>
        /// 上级部门ID
        /// </summary>
        [Display(Name = "上级部门")]
        public long? ParentID { get; set; }

        public virtual IList<Employee> EmployeeList { get; set; }

        /// <summary>
        /// 顶级部门
        /// </summary>
        public virtual EmployeeDepartment TopDepartment
        {
            get
            {
                var current = ParentDepartment != null ? ParentDepartment : this;
                while (current.ParentDepartment != null)
                {
                    current = current.ParentDepartment;
                }
                return current;
            }
        }

        /// <summary>
        /// 上级部门
        /// </summary>
        public virtual EmployeeDepartment ParentDepartment { get; set; }



        /// <summary>
        /// 子部门
        /// </summary>
        public virtual IList<EmployeeDepartment> ChildDepartment { get; set; }



        //--------------------------------------------------------------
        public virtual Merchant Merchant { get; set; }

        /// <summary>
        /// 部门ID集合
        /// </summary>
        [NotMapped]
        public long[] DepartmentIDs { get; set; }

        [NotMapped]
        public string LevelCode { get; set; }


        /// <summary>
        /// 排序后的名称
        /// </summary>
        [NotMapped]
        public string OrderByName { get; set; }

        [NotMapped]
        public int Level
        {
            get { return !string.IsNullOrWhiteSpace(LevelCode) ? LevelCode.Length / 2 : 0; }
        }

        /// <summary>
        /// 当前等级的序号
        /// </summary>
        [NotMapped]
        public int LevelSort { get; set; }
    }
}

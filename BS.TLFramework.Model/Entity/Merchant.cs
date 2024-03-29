﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.TLFramework.Model
{
    public class Merchant : BaseEntity
    {
        /// <summary>
        /// 商户号
        /// </summary>
        [Display(Name = "商户号")]
        [Required(ErrorMessage = "{0}不能为空")]
        [MinLength(4, ErrorMessage = "{0}不能少于4位")]
        [MaxLength(16, ErrorMessage = "{0}不能多于16位")]
        public string Code { get; set; }

        /// <summary>
        /// 商户名称(注册名称)
        /// </summary>
        [Display(Name = "商户名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [MinLength(2, ErrorMessage = "{0}不能少于2位")]
        [MaxLength(64, ErrorMessage = "{0}不能多于64位")]
        public string Name { get; set; }

        /// <summary>
        /// 联系人名称
        /// </summary>
        [Display(Name = "联系人名称")]
        [MaxLength(16, ErrorMessage = "{0}不能多于16位")]
        public string Contact { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        [Display(Name = "联系人电话")]
        [MaxLength(16, ErrorMessage = "{0}不能多于16位")]
        public string Mobile { get; set; }

        /// <summary>
        /// 二类户所属银行
        /// </summary>
        //  public int AccountBank { get; set; }


        //-----------------------------------------------------------
        //  public virtual List<Equipment> EquipmentList { get; set; }
    }
}

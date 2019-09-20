using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.TLFramework.Model
{

    public enum ErrorCode
    {
        /// <summary>
        /// 1.编码不能为空
        /// </summary>       
        [Description("编码不能为空")]
        CodeIsNotNull = 1,

        /// <summary>
        /// 2.编码已存在
        /// </summary>
        [Description("编码已存在")]
        CodeIsExists = 2,

        /// <summary>
        /// 3.系统错误
        /// </summary>
        [Description("系统错误")]
        Exception = 3,

        /// <summary>
        /// 4.记录不存在
        /// </summary>
        [Description("记录不存在")]
        RecordNoExists = 4,

        /// <summary>
        /// 5.正在使用，无法删除
        /// </summary> 
        [Description("正在使用，无法删除")]
        InUseNotDelete = 5,

        /// <summary>
        /// 6.名称已存在
        /// </summary>
        [Description("名称已存在")]
        NameIsExists = 6,


        /// <summary>
        /// 7.名称不能为空
        /// </summary>
        [Description("名称不能为空")]
        NameIsNotNull = 7,


        /// <summary>
        /// 8.明细不能为空
        /// </summary>
        [Description("明细不能为空")]
        DetailIsNotNull = 8,


        /// <summary>
        /// 9.用户不存在
        /// </summary>
        [Description("用户不存在")]
        UserNotExists = 9,


        /// <summary>
        /// 10.用户名称错误
        /// </summary>
        [Description("用户名称错误")]
        UserNameError = 10,


        /// <summary>
        /// 11.激活码错误
        /// </summary>
        [Description("激活码错误")]
        ActiveCodeError = 11,

        /// <summary>
        /// 12.激活码不能为空
        /// </summary>
        [Description("激活码不能为空")]
        ActiveCodeIsNotNull = 12,

        /// <summary>
        /// 13.微信ID不能为空
        /// </summary>
        [Description("微信ID不能为空")]
        WXIDIsNotNull = 13,

        /// <summary>
        /// 14.微信ID不能为空
        /// </summary>
        [Description("微信ID不存在")]
        WXIDNotExists = 14,

        /// <summary>
        /// 15.密码不正确
        /// </summary>
        [Description("密码不正确")]
        PasswordError = 15,

        /// <summary>
        /// 16.密码不能为空
        /// </summary>
        [Description("密码不能为空")]
        PasswordNotNull = 16,

    }
    public enum CacheKey
    {
        /// <summary>
        /// 地区
        /// </summary>
        Area = 0,

        /// <summary>
        /// 控制台 菜单
        /// </summary>
        Menu = 2,

        /// <summary>
        /// 控制台 菜单操作
        /// </summary>
        MenuAction = 3,

    }

    public enum EmployeeStatus
    {

        /// <summary>
        /// 锁定
        /// </summary>
        [Description("锁定")]
        Lock = -2,

        /// <summary>
        /// 停用
        /// </summary>
        [Description("停用")]
        Disable = -1,

        /// <summary>
        /// 用户未激活（默认值）
        /// </summary>
        [Description("未激活")]
        NotActive = 0,

        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable = 1,
    }
    public enum UserStatus
    {

        /// <summary>
        /// 锁定
        /// </summary>
        [Description("锁定")]
        Lock = -2,

        /// <summary>
        /// 停用
        /// </summary>
        [Description("停用")]
        Disable = -1,

        /// <summary>
        /// 用户未激活（默认值）
        /// </summary>
        [Description("未激活")]
        NotActive = 0,

        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable = 1,
    }
}

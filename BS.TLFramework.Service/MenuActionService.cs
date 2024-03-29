﻿using BS.TLFramework.IService;
using BS.TLFramework.Model;
using Framework.Cache;
using Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.TLFramework.Service
{
    public class MenuActionService : BusinessService<MenuAction>, IMenuActionService
    {
        public void SetCache()
        {
            var list = Gets(t => t.Status > 0).Select(
                t => new ActionModel
                {
                    Controller = t.Menu.Controller.ToLower(),
                    Action = t.Code.ToLower(),
                }).ToList<ActionModel>();

            CacheHelper.SetCache(CacheKey.MenuAction.ToString(), list, DateTime.Now.AddYears(100));
        }
    }
}

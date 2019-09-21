﻿using BS.TLFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.TLFramework.IService
{
    public interface IEmployeeMenuService : IBusinessService<EmployeeMenu>
    {
        IList<EmployeeMenu> GetEmployeeMenuList();

        void SetCache();
    }
}

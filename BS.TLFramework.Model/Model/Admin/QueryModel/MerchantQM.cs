﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.TLFramework.Model.Admin
{
    public class MerchantQM : BaseQueryModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
    }
}

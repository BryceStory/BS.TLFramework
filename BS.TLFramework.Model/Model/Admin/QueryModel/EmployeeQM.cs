﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.TLFramework.Model
{
    public class EmployeeQM : BaseQueryModel
    {
        public string MerchantName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }

        public long? MerchantID { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.TLFramework.Model.Admin
{
    public class MenuLM : BaseListModel
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public string ParentCode { get; set; }

        public string Controller { get; set; }
    }
}

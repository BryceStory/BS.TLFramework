using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.TLFramework.Model.Admin
{
    public class UserQM : BaseQueryModel
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }

        public long? DepartmentID { get; set; }

        public long? RoleID { get; set; }
    }
}

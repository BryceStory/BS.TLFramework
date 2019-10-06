using BS.TLFramework.IService;
using BS.TLFramework.Model;
using BS.TLFramework.Model.Admin;
using Framework.Common;
using Framework.Expression;
using Framework.Web.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace BS.TLFramework.Service
{
    public class MerchantService : BusinessService<Merchant>, IMerchantService
    {    
        public override Expression<Func<Merchant, bool>> GetExpress<QM>(QM model)
        {
            Expression<Func<Merchant, bool>> express = PredicateBuilder.True<Merchant>();

            var m = model as MerchantQM;

            if (!string.IsNullOrWhiteSpace(m.Code))
            {
                express = express.And(t => t.Code.Contains(m.Code.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(m.Name))
            {
                express = express.And(t => t.Name.Contains(m.Name.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(m.Mobile))
            {
                express = express.And(t => t.Mobile == m.Mobile.Trim());
            }

            if (m.Status != null)
            {
                express = express.And(t => t.Status == m.Status);
            }


            return express;
        }
    }
}

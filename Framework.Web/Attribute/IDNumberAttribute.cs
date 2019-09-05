using Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Web.Attribute
{
    public class IDNumberAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.Format("{0}不合法", name);
        }

        public override bool IsValid(object value)
        {
            var text = value as string;

            if (string.IsNullOrWhiteSpace(text))
                return true;

            return StringHelper.IsIDNumber(text);
        }
    }
}

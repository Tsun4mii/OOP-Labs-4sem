using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab_2
{
    sealed public class IdentifAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string id = value.ToString();
                if (!id.StartsWith("F"))
                    return true;
                else this.ErrorMessage = "Неправильный формат Id";
            }
            return false;
        }
    }
}

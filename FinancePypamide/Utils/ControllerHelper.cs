using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancePypamide.Utils
{
    public static class ControllerHelper
    {
        public static string GetControllerName(string fullName)
        {
            return fullName.Replace("Controller", "");
        }
    }
}
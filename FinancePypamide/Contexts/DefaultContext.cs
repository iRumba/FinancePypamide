using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinancePypamide.Contexts
{
    public class DefaultContext : DbContext
    {
        public DefaultContext() : base("DatabaseContainer")
        {

        }
    }
}
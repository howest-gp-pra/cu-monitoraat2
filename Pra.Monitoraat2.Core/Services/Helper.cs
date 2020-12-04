using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.Monitoraat2.Core.Services
{
    public class Helper
    {
        public static string GetConnectionString()
        {
            return @"Data Source=(local)\SQLEXPRESS;Initial Catalog=monitoraat2;Integrated security=true;";
        }
        public static string HandleQuotes(string waarde)
        {
            return waarde.Trim().Replace("'", "''");
        }
    }
}

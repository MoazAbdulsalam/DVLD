using System;
using System.Configuration;

using System.Data.Common;
namespace DVLDDataAccessLayer
{
    public class clsDataAccessSettings
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    }
}

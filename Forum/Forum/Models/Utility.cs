using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public static class Utility
    {
        public static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                                                "Initial Catalog = ForumDB; " +
                                                "Integrated Security = True";
    }
}
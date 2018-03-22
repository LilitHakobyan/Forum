using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Models
{
    interface IRepasitory:IDisposable
    {
        SqlCommand CreateCommand();
         void SaveChanges();
    }
}

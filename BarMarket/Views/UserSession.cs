using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarMarket.DB
{
    public static class UserSession
    {
        public static User CurrentUser { get; set; }
    }
}

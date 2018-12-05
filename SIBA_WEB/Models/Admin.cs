using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Models
{
    public class Admin
    {
        public Admin() { }

        public String NoControl { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public String Password { get; set; }
        public long PermissionLevel { get; set; } // 1 - SuperAdmin permission || 9 - Lowest Admin 
    }
}

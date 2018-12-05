using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Models.Users
{
    public class AdminEntity : TableEntity
    {
        public AdminEntity() { }
        public AdminEntity(String noControl)
        {
            this.PartitionKey = noControl;
            this.RowKey = "Admin";
        }
        public String Name { get; set; }
        public String LastName { get; set; }
        public String Password { get; set; }
        public int PermissionLevel { get; set; }

    }
}

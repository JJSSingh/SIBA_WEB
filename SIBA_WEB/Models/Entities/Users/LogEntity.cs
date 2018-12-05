using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Models.Users
{
    public class LogEntity : TableEntity
    {
        public LogEntity() { }
        public LogEntity(String noControl)
        {
            this.PartitionKey = noControl;
            this.RowKey = "Log";
        }
        public String Action { get; set; }
        public String ActionDate { get; set; }

    }
}

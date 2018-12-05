using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Models.Users
{
    public class EncargadaEntity : TableEntity
    {
        public EncargadaEntity() { }
        public EncargadaEntity(String noControl)
        {
            this.PartitionKey = noControl;
            this.RowKey = "Encargada";
        }
        public String Name { get; set; }
        public String Password { get; set; }
        public String LastName { get; set; }
        public String Turn { get; set; }
    }
}

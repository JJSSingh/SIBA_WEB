using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Models.Comida
{
    public class PlatilloEntity : TableEntity
    {
        public PlatilloEntity() { }
        public PlatilloEntity(String Name,String Id)
        {
            this.PartitionKey = Name;
            this.RowKey = Id;
        }
        public String Description { get; set; }
        public long Price { get; set; }


    }
}

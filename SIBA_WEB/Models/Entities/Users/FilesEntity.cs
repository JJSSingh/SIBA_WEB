using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Models.Users
{
    public class FilesEntity : TableEntity
    {
        public FilesEntity() { }
        public FilesEntity(String noControl)
        {
            this.PartitionKey = noControl;
            this.RowKey = "Files";
        }

    }
}

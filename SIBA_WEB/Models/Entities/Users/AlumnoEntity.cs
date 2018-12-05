using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Models.Users
{
    public class AlumnoEntity : TableEntity
    {
        public AlumnoEntity() { }
        public AlumnoEntity(String NoControl, String Email)
        {
            this.PartitionKey = NoControl;
            this.RowKey = Email;
        }
        
        public String Password { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public int Semester { get; set; }
        public String Status { get; set; }
        public String Date { get; set; }
        public Boolean canCreateToken { get; set; }
    }
}

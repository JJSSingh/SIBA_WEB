using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Models.Users
{
    public class TokenEntity : TableEntity
    {
        public TokenEntity() { }
        public TokenEntity(String noControl)
        {
            this.PartitionKey = noControl;
            this.RowKey = "Token";
        }
        public String Token { get; set; }
        public String TokenCreated { get; set; }  //FECHA DE CREACION
        public String TokenModified { get; set; } //Fecha de consumo
        public bool IsTokenModified { get; set; } //Boolean de confirmacion.
    }
}

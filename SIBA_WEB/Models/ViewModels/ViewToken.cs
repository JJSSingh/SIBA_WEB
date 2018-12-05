using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Models.ViewModels
{
    public class ViewToken
    {
        public ViewToken() { }

        //public String noControl { get; set; }
        public String token { get; set; }
        public String TokenCreated { get; set; }
        public Boolean isTokenUsed { get; set; }
        public String Status { get; set; }


    }
}

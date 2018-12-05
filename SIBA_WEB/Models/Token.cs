using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Models
{
    public class Token
    {
        public Token() { }

        public String NoControl { get; set; }
        public String token { get; set; }
        public String TokenCreated { get; set; }
        public String TokenModified { get; set; }
        public Boolean IsTokenUsed { get; set; }
    }
}

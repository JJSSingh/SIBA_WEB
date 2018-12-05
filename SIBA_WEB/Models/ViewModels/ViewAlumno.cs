using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Models.ViewModels
{
    public class ViewAlumno
    {
        public ViewAlumno() { }

        public String NoControl { get; set; }
        public String Email { get; set; }
        public String Name { get; set; }

        public String Comment { get; set; }

        public List<String> Urls { get; set; }

    }
}

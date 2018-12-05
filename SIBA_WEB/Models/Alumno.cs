using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Models
{
    public class Alumno
    {
        public Alumno() { }

        public String NoControl { get; set; }
        public String Email { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public String Password { get; set; }
        public int Semester { get; set; }
        public Boolean CanCreateToken { get; set; }
        public String Status { get; set; } //pendiente, en proceso, documentacion aceptada -> Activo | No aceptado.
        public String Date { get; set; } //fecha de cita en caso de ser documentacion aceptada.
        // Date -> Comentarios !!!!
        //Pendiente -> En Proceso -> Revision -> Activo .
    }
}

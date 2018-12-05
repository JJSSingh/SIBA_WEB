using SIBA_WEB.Models;
using SIBA_WEB.Models.Comida;
using SIBA_WEB.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Services
{
    public class AzureParser
    {
        public AzureParser() { }

        public Platillo ToPlatillo(PlatilloEntity ep)
        {

            Platillo pl = new Platillo();
            if (!(ep == null))
            {
                pl.Name = ep.PartitionKey;
                pl.Description = ep.Description;
                pl.Price = ep.Price;
            }
            return pl;
        }
        public PlatilloEntity ToPlatilloEntity(Platillo pl)
        {
            PlatilloEntity ep = new PlatilloEntity();
            ep.PartitionKey = pl.Name;
            ep.RowKey = "01";
            ep.Description = pl.Description;
            ep.Price = pl.Price;
            return ep;
        }
        public Token ToToken(TokenEntity tk)
        {
            Token t = new Token();
            if (!(tk == null)){
                t.NoControl = tk.PartitionKey;
                t.token = tk.Token;
                t.TokenCreated = tk.TokenCreated;
                t.TokenModified = tk.TokenModified;
                t.IsTokenUsed = tk.IsTokenModified;
            }
            return t;
        }
        public TokenEntity ToTokenEntity(Token tk)
        {
            TokenEntity t = new TokenEntity();
            t.PartitionKey = tk.NoControl;
            t.RowKey = "Token";
            t.Token = tk.token;
            t.TokenCreated = tk.TokenCreated;
            t.TokenModified = tk.TokenModified;
            t.IsTokenModified = tk.IsTokenUsed;
            return t;
        }

        public Alumno toAlumno(AlumnoEntity al)
        {
            Alumno a = new Alumno();
            if (!(al == null))
            {
                a.NoControl = al.PartitionKey;
                a.Email = al.RowKey;
                a.Name = al.Name;
                a.LastName = al.LastName;
                a.Password = al.Password;
                a.Semester = al.Semester;
                a.CanCreateToken = al.canCreateToken;
                a.Status = al.Status;
                a.Date = al.Date;
            }
            
            return a;
        }
        public AlumnoEntity toAlumnoEntity(Alumno al)
        {
            AlumnoEntity a = new AlumnoEntity();
            a.PartitionKey = al.NoControl;
            a.RowKey = al.Email;
            a.Password = al.Password;
            a.Name = al.Name;
            a.LastName = al.LastName;
            a.Semester = al.Semester;
            a.canCreateToken = al.CanCreateToken;
            a.Status = al.Status;
            a.Date = al.Date;

            return a;
        }
        public EncargadaEntity ToEncargadaEntity(Encargada e)
        {
            EncargadaEntity ee = new EncargadaEntity();
            ee.PartitionKey = e.NoControl;
            ee.RowKey = "Encargada";
            ee.Name = e.Name;
            ee.LastName = e.LastName;
            ee.Password = e.Password;
            ee.Turn = e.Turn;
            return ee;
        }
        public Encargada ToEncargada(EncargadaEntity ee)
        {
            Encargada e = new Encargada();
            if (!(ee == null))
            {
                e.NoControl = ee.PartitionKey;
                e.Name = ee.Name;
                e.LastName = ee.LastName;
                e.Password = ee.Password;
                e.Turn = ee.Turn;

            }
            return e;
        }
    }
}

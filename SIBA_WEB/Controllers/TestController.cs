using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SIBA_WEB.Models;
using SIBA_WEB.Models.Comida;
using SIBA_WEB.Models.Users;
using SIBA_WEB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB.Controllers
{
    public class TestController : Controller
    {
        private IRepository _repo;
        private readonly UserManager<IdentityUser> _uManager;

        public TestController(IRepository repo, UserManager<IdentityUser> uManager)
        {
            this._repo = repo;
            this._uManager = uManager;
        }

        public String Index()
        {
            var t = _uManager.Users;
            var x = User.Identity.Name;
            return "init:" + _repo.qwe() + ":end" + "=====" + x;
        }
        public List<Platillo> Prueba2()
        {
            return _repo.getAllPlatillos();
        }
        public Platillo prueba3()
        {
            return _repo.getPlatillo("Nachos");
        }
        public PlatilloEntity prueba4()
        {
            Platillo p = new Platillo();
            p.Name = "QUESADILLAS";
            p.Description = "Sin carnita pa";
            p.Price = 34;

            return _repo.createPlatillo(p);
        }

        public TokenEntity asdf1()
        {
            Token t = new Token();
            t.NoControl = "12170695";
            t.token = "1217Singh";
            t.TokenCreated = "dsafs";
            t.TokenModified = "aasd";
            t.IsTokenUsed = true;

            return _repo.createToken(t);
        }
        public TokenEntity asdf2()
        {
            Token t = new Token();
            t.NoControl = "12170696";
            t.token = "1217Pepinillo";
            t.TokenCreated = "dsafs";
            t.TokenModified = "aasd";
            t.IsTokenUsed = true;

            return _repo.createToken(t);
        }
        public Token token1()
        {
            return _repo.getToken("12170695");
        }
        public List<Token> token2()
        {
            return _repo.getAllTokens();
        }
        public AlumnoEntity al01()
        {
            Alumno al = new Alumno();
            al.NoControl = "12170695";
            al.Email = "jose.singh17@gmail.com";
            al.Name = "Jose";
            al.LastName = "Singh";

            return _repo.createAlumno(al);
        }
        public AlumnoEntity al02()
        {
            Alumno al = new Alumno();
            al.NoControl = "12170696";
            al.Email = "jose_singh_0000@hotmail.com";
            al.Name = "Jesus";
            al.LastName = "Singh";

            return _repo.createAlumno(al);
        }
        
        public Alumno Alumno1()
        {
            return _repo.getAlumnoByEmail("jose_singh_0000@hotmail.com");
        }
        public Alumno ALumno2()
        {
            return _repo.getAlumnoByNoControl("121703edfs");
        }
        public EncargadaEntity En()
        {
            Encargada e = new Encargada();
            e.NoControl = "00000002";
            e.Name = "Doña chuyita2";
            e.LastName = "Sanchez2";
            e.Turn = "Mañana2";
            return _repo.createEncargada(e);
        }
        public Encargada encargada()
        {
            return _repo.getEncargada("00000002");
        }
        public String token()
        {
            return _repo.createTokenWithValidation("12170695");
        }
        public Token testo()
        {
            return _repo.getToken("12170695");
        }
        
        public List<String> blobs()
        {
            return _repo.ListBlobs("12170691");
        }

        public String download()
        {
            _repo.DownloadBlobs("12170687");

            return "aversi se bajo";
        }

        public List<String> url()
        {
           return _repo.BlobUrlList("12170691");
        }




    }
}

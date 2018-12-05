using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Table;
using SIBA_WEB.Models;
using SIBA_WEB.Models.Comida;
using SIBA_WEB.Models.Users;

namespace SIBA_WEB.Services
{
    public class Repository : IRepository
    {
        AzureStorage Astorage;
        BlobStorage Bstorage;
        AzureParser AParser;
        //String asd;
        //String asd2;
        

        public Repository(String AccountName, String AccountKey)
        {
            this.Astorage = new AzureStorage(AccountName, AccountKey);
            this.Bstorage = new BlobStorage(AccountName, AccountKey);
            this.AParser = new AzureParser();

            //asd = AccountName;
            //asd2 = AccountKey;
        }
        public String qwe()
        {
            return "sdf";//asd + asd2;
        }


        /// <summary>
        /// /////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public Platillo getPlatillo(String nombre)
        {

            Platillo pl = AParser.ToPlatillo(Astorage.GetItem_Platillos("FoodTable", nombre, "01").Result);
            return pl;

            //PlatilloEntity pl2 = (PlatilloEntity) Astorage.GetItem("FoodTable",nombre,"01").Result;
        }

        public List<Platillo> getAllPlatillos()
        {
            List<Platillo> list = new List<Platillo>();

            List<PlatilloEntity> auxList = Astorage.GetList_Platillos("FoodTable").Result;

            foreach(var en in auxList)
            {
                Platillo pl = AParser.ToPlatillo(en);
                list.Add(pl);
            }
            return list;

            //throw new NotImplementedException();
        }
        public PlatilloEntity createPlatillo(Platillo pl)
        {
            PlatilloEntity pe = AParser.ToPlatilloEntity(pl);

            String value = Astorage.InsertOrUpdate("FoodTable", pe).Result;

            return pe;

        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        /// 

        public Token getToken(String noControl)
        {
            Token t = AParser.ToToken(Astorage.GetItem_Token("Users",noControl,"Token").Result);
            return t;
        }

        public List<Token> getAllTokens()
        {

            List<Token> list = new List<Token>();

            List<TokenEntity> auxList = Astorage.GetList_Token("Users").Result;

            foreach (var q in auxList)
            {
                Token t = AParser.ToToken(q);
                list.Add(t);
            }
            return list;

            //throw new NotImplementedException();
        }

        public TokenEntity createToken(Token t)
        {
            TokenEntity te = AParser.ToTokenEntity(t);

            String Value = Astorage.InsertOrUpdate("Users", te).Result;

            return te;
        }

        public String createTokenWithValidation(String noControl)
        {
            Token t = getToken(noControl);
            Token to = new Token();
            String str = "";

            if(t.token == null || !(t.TokenCreated == formatedActualDate()))
            {
                to.NoControl = noControl;
                to.token = tokenContent();
                to.TokenCreated = formatedActualDate();
                to.IsTokenUsed = false;
                str = "Successful Creation";
                var x = createToken(to);
            }
            else
            {
                if(t.TokenCreated == formatedActualDate())
                {
                    str =  "Ya creaste token hoy";
                }
                if(t.IsTokenUsed == true)
                {
                    str =  "Ya usaste tu token del dia de hoy";
                }
            }
            return str;

        }
        /// <summary>
        /// 
        /// </summary>
        /// ></param>
        /// <returns></returns>
        /// 

        public Alumno getAlumnoByNoControl(string noControl)
        {
            Alumno al = new Alumno();
            List<AlumnoEntity> Aal = Astorage.GetItem_Alumno("Users", noControl).Result;

            foreach (var a in Aal)
            {
                al = AParser.toAlumno(a);
            }

            return al;
        }

        public Alumno getAlumnoByEmail(string email)
        {
            Alumno al = new Alumno();
            List<AlumnoEntity> Aal = Astorage.GetItem_Alumno2("Users",email).Result;

            foreach (var a in Aal)
            {
               al = AParser.toAlumno(a);
            }
            //Token t = AParser.ToToken(Astorage.GetItem_Token("Users",noControl,"Token").Result);

            return al;
        }

        public List<Alumno> getAllAlumnos()
        {
            List<Alumno> list = new List<Alumno>();

            List<AlumnoEntity> auxList = Astorage.GetList_Alumno("Users").Result;

            foreach (var q in auxList)
            {
                Alumno t = AParser.toAlumno(q);
                list.Add(t);
            }
            return list;
        }

        public AlumnoEntity createAlumno(Alumno a)
        {
            AlumnoEntity te = AParser.toAlumnoEntity(a);

            String Value = Astorage.InsertOrUpdate("Users", te).Result;

            return te;
        }

        public Encargada getEncargada(String noControl)
        {
            Encargada t = AParser.ToEncargada(Astorage.GetItem_Encargada("Users", noControl, "Encargada").Result);
            return t;
        }
        public EncargadaEntity createEncargada(Encargada e)
        {
            EncargadaEntity ee = AParser.ToEncargadaEntity(e);

            String Value = Astorage.InsertOrUpdate("Users", ee).Result;

            return ee;
        }

        
        public String tokenContent()
        {
            Random r = new Random();
            return r.Next(1000) + "";
        }
        public String formatedActualDate()
        {
            DateTime dt = DateTime.Now;
            return dt.ToString("dd-MM-yy");   
        }


        public String TokenStatus(String noControl, String Token)
        {
            String a = "";

            Token t = getToken(noControl);

            if(t.token == Token)
            {
                if (t.TokenCreated == formatedActualDate())
                {
                    if (t.IsTokenUsed == false)
                    {
                        a = "Token Valido";
                    }
                    else
                    {
                        a = "Token Invalido => Ya fue usado.";
                    }
                }
                else
                {
                    a = "Token Invalido => Token Antiguo.";
                }
            }
            else
            {
                a = "Token Invalido => No concuerda con el Numero de Control.";
            }
            
            return a;
        }
        public bool ValidateToken(String noControl, String Token)
        {
            bool a = false;

            Token t = getToken(noControl);

            if (t.token == Token)
            {
                if (t.TokenCreated == formatedActualDate())
                {
                    if (t.IsTokenUsed == false)
                    {
                        a = true;
                    }
                    
                }
                
            }
            return a;
        }

        public bool ChangeTokenStatus(String noControl)
        {
            Token t = getToken(noControl);
            t.IsTokenUsed = true;
            var x = createToken(t);
            return true;
        }

        public String UploadFile(IFormFile file, String noControl)
        {
            //Stream Fl, string Container, string FileName

            return Bstorage.UploadBlob("container", noControl, file).Result;


        }
        public List<String> ListBlobs(String noControl)
        {
            return Bstorage.ListBlobs("container", noControl).Result;
        }
        public void DownloadBlobs(String noControl)
        {
            Bstorage.DownloadBlob("container", noControl).Wait();

        }
        public List<String> BlobUrlList(String noControl)
        {
            return Bstorage.ListBlobsUrl("container", noControl).Result;
        }

        public void AsignAlumnoRoleToCurrentUser()
        {
            /*
            Task<bool> hasRole = _rManager.RoleExistsAsync("Alumno");
            hasRole.Wait();

            if (!hasRole.Result)
            {
                Task<IdentityResult> roleResult = _rManager.CreateAsync(new IdentityRole("Alumno"));
                roleResult.Wait();
            }
            Task<IdentityUser> cUser = _uManager.GetUserAsync();
            cUser.Wait();

            if (cUser.IsCompletedSuccessfully)
            {
                System.Threading.Thread.Sleep(1000);
                _uManager.AddToRoleAsync(cUser.Result, "Alumno").Wait();
            }
            */
        }
    }






    public interface IRepository
    {
        Platillo getPlatillo(String nombre);
        List<Platillo> getAllPlatillos();
        PlatilloEntity createPlatillo(Platillo pl);

        Token getToken(String noControl);
        List<Token> getAllTokens();
        TokenEntity createToken(Token t);
        String createTokenWithValidation(String noControl);
        bool ChangeTokenStatus(String noControl);
        String TokenStatus(String noControl, String Token);
        bool ValidateToken(String noControl, String Token);

        Alumno getAlumnoByNoControl(String noControl);
        Alumno getAlumnoByEmail(String email);
        List<Alumno> getAllAlumnos();
        AlumnoEntity createAlumno(Alumno a);

        Encargada getEncargada(String noControl);
        EncargadaEntity createEncargada(Encargada e);

        String formatedActualDate();
        String tokenContent();

        List<String> ListBlobs(String noControl);
        void DownloadBlobs(String noControl);

        void AsignAlumnoRoleToCurrentUser();

        List<String> BlobUrlList(String noControl);
        
        //Task<String> UploadFile(Stream Fl,String Container,String FileName);
        //Task UploadFile(Stream Fl, String Container, String FileName);
        String UploadFile(IFormFile file, String noControl);

        //----
        //-Validaciones para views
        //String getStatusByEmail(String mail);

        //########################################
        String qwe();
        //Alumno prueba();
        //Task<Alumno> prueba2();
    }


}

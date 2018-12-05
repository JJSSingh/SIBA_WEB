using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SIBA_WEB.Models;
using SIBA_WEB.Models.ViewModels;
using SIBA_WEB.Services;

namespace SIBA_WEB.Controllers
{
    public class AlumnoController : Controller
    {
        private IRepository _repo;
        private readonly UserManager<IdentityUser> _uManager;
        private readonly RoleManager<IdentityRole> _rManager;

        public AlumnoController(IRepository repo, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this._repo = repo;
            this._uManager = userManager;
            this._rManager = roleManager;
        }
        public void put()
        {
            Task<bool> hasRole = _rManager.RoleExistsAsync("Alumno");
            hasRole.Wait();

            if (!hasRole.Result)
            {
                Task<IdentityResult> roleResult = _rManager.CreateAsync(new IdentityRole("Alumno"));
                roleResult.Wait();
            }
            Task<IdentityUser> cUser = _uManager.GetUserAsync(User);
            cUser.Wait();

            _uManager.AddToRoleAsync(cUser.Result, "Alumno").Wait();

            if (cUser.IsCompletedSuccessfully)
            {
                _uManager.AddToRoleAsync(cUser.Result, "Alumno").Wait();
            }
        }
        [Authorize]
        public ActionResult Completar()
        {

            put();

            if (User.Identity.IsAuthenticated)
            {
                
                var alumno = _repo.getAlumnoByEmail(User.Identity.Name);

                if (alumno.Email == User.Identity.Name)
                {
                    return RedirectToAction("Profile");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
            
        }

        [HttpPost]
        public ActionResult Completar(Alumno model)
        {
            String mail = User.Identity.Name;
            model.Email = mail;
            model.Status = "Pendiente";

            var modelEntity = _repo.createAlumno(model);

            return RedirectToAction("Index","Home"); // ~&&
        }

        [Authorize]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = _repo.getAllAlumnos();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        [Authorize]
        public ActionResult Profile()
        {
            if (User.Identity.IsAuthenticated)
            {

                return View(_repo.getAlumnoByEmail(User.Identity.Name));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize]
        public ActionResult UploadFiles()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        [Authorize, HttpPost]
        public ActionResult UploadFiles(IFormFileCollection files)
        {
            if (User.Identity.IsAuthenticated)
            {
                Alumno al = _repo.getAlumnoByEmail(User.Identity.Name);

                foreach (IFormFile file in files)
                {
                    _repo.UploadFile(file, al.NoControl);
                }

                al.Status = "Revision";

                _repo.createAlumno(al);

                return RedirectToAction("Index","Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize]
        public ActionResult TokenDetails()
        {
            if (User.Identity.IsAuthenticated)
            {
                Alumno al = _repo.getAlumnoByEmail(User.Identity.Name);

                String status = _repo.createTokenWithValidation(al.NoControl);

                Token tk = _repo.getToken(al.NoControl);

                ViewToken vtk = new ViewToken();
                //vtk.noControl = tk.NoControl;
                vtk.token = tk.token;
                vtk.TokenCreated = tk.TokenCreated;
                vtk.isTokenUsed = tk.IsTokenUsed;
                vtk.Status = status;

                return View(vtk);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        
    }
}
using Microsoft.AspNetCore.Mvc;
using SIBA_WEB.Models;
using SIBA_WEB.Services;
using SIBA_WEB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SIBA_WEB.Controllers
{
    public class AdminController : Controller
    {
        private IRepository _repo;

        public AdminController(IRepository repo)
        {
            this._repo = repo;
        }
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public ActionResult Details(String id)
        {

            if (User.Identity.IsAuthenticated)
            {
                List<String> a = _repo.BlobUrlList(id);
                ViewBag.data = a;
                return View(_repo.getAlumnoByNoControl(id));

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult ChangeStatus(String id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var al = _repo.getAlumnoByNoControl(id);
                al.Date = "Favor de pasar al CESA para entrevista o marcar al XXXXXXXXXXXXX";
                al.Status = "Entrevista";
                _repo.createAlumno(al);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            } 
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ChangeStatusToActive(String id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var al = _repo.getAlumnoByNoControl(id);
                al.Date = "Usted fue aceptado en el programa de becas del CESA";
                al.Status = "Activo";
                _repo.createAlumno(al);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        //Metodo para cambiar el estatus del alumno, al siguiente.
        [HttpPost]
        public ActionResult DetailsAlumno(Alumno model)
        {
            return View();
        }
        public ActionResult CreateAlumno()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAlumno(Alumno model)
        {
            return View();
        }
        //Presentacion de los nombres de los archivos.
        public ActionResult ViewFiles(String id)
        {
            return View();
        }
        //Descarga de archivos (?) por path (?)
        [HttpPost]
        public ActionResult DownloadFiles(String id)
        {
            return View();
        }






        public ActionResult IndexPlatillo()
        {
            return View();
        }
        public ActionResult DetailsPlatillo(String id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult DetailsPlatillo(Platillo model)
        {
            return View();
        }
        public ActionResult CreatePlatillo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePlatillo(Platillo model)
        {
            return View();
        }


    }
}

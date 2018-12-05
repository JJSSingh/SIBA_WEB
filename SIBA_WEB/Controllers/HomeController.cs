using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIBA_WEB.Models;
using SIBA_WEB.Services;

namespace SIBA_WEB.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;

        public HomeController(IRepository repo)
        {
            this._repo = repo;
        }
        public IActionResult Index()
        {
            Alumno al = _repo.getAlumnoByEmail(User.Identity.Name);
            return View(al);
        }


        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            Alumno al = _repo.getAlumnoByEmail(User.Identity.Name);
                
            //foreach (IFormFile file in files)
            //{
                _repo.UploadFile(file, al.NoControl);
            //}
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UploadFile2(IFormFileCollection files)
        {
            Alumno al = _repo.getAlumnoByEmail(User.Identity.Name);

            foreach (IFormFile file in files)
            {
                _repo.UploadFile(file, al.NoControl);
            }
            return RedirectToAction("Index");
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public String test()
        {
            return "sdf" + "asdf";
        }
    }
}

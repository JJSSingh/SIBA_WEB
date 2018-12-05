using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIBA_WEB.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using SIBA_WEB.Models.Comida;
using SIBA_WEB.Models.Users;
using SIBA_WEB.Services;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIBA_WEB.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]

    public class ApiController : ControllerBase
    {
        private IRepository context;

        public ApiController(IRepository context)
        {
            this.context = context;
        }

        [HttpGet("getEncargada/{id}")]
        public ActionResult<Encargada> Get(String id)
        {
            Encargada e = context.getEncargada(id);
            return e;
        }

        [HttpGet("getAlumno/{id}")]
        public ActionResult<Alumno> Get1(String id)
        {
            return context.getAlumnoByNoControl(id);
        }

        [HttpGet("getToken/{id}")]
        public ActionResult<Token> get2(String id)
        {
            return context.getToken(id);
        }

        [HttpGet("TokenStatus/{noControl}/{Token}")]
        public ActionResult<String> Get(String noControl, String Token)
        {
            return context.TokenStatus(noControl, Token);
        }

        [HttpGet("ValidateToken/{noControl}/{Token}")]
        public ActionResult<bool> Get2(String noControl, String Token)
        {
            return context.ValidateToken(noControl, Token);
        }

        [HttpPost("ChangeToken/{noControl}")]
        public ActionResult<bool> Post(String noControl)
        {
            if (noControl == null)
            {
                return BadRequest();
            }
            return context.ChangeTokenStatus(noControl);
        }


        
        

        
               



        



        /// ///////////////////////////////////////////////-------------------------------------

        
    }
}

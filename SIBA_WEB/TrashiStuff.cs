using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBA_WEB
{
    public class TrashiStuff
    {

        /*
         *
         * [HttpPost("CreateToken")]
        public ActionResult Post(Token t)
        {
            if (t == null)
            {
                return NotFound();
            }

            context.createToken(t);

            return Ok();
        }
         *[HttpGet("{id}", Name = "Alumnos")]
        public ActionResult<List<Alumno>> Get()
        {
            return context.getAllAlumnos();
        } 
         * <li>
                <a asp-area="Identity" asp-page="/Account/Manage/Index" title="Manage">Hello @UserManager.GetUserName(User)!</a>
            </li>


         * <li><a asp-area="" asp-controller="Home" asp-action="Index">Home</a></li>
                    <li><a asp-area="" asp-controller="Home" asp-action="About">About</a></li>
                    <li><a asp-area="" asp-controller="Home" asp-action="Contact">Contact</a></li>
        [HttpGet]
        public ActionResult getAllPlatillos()
        {
            return context.getAllPlatillos();
        }

        [HttpGet]
        public JsonResult getAllAlumnos()
        {
            return context.getAllAlumnos();
        }

        [HttpPost]
        public ActionResult CreatePlatillo(String json)
        {
            return context.CreatePlatillo(json);
        }
        [HttpPut]
        public ActionResult EditPlatillo(String json)
        {
            return context.EditPlatillo(json);
        }
        
        [HttpGet]
        public ActionResult ValidateToken(String json)
        {
            return context.ValidateToken(json);
        } 
        [HttpPost]
        public ActionResult GenerateToken(String json)
        {
            return context.GenerateToken(json);
        }
        ==============================================================

         [HttpGet]
         public JsonResult GetAllPlatillos2()
         {
             List<Platillo> list = new List<Platillo>();

             CloudTable FoodTable = ctFood();

             TableQuery<PlatilloEntity> tablequery = new TableQuery<PlatilloEntity>();

             TableContinuationToken token = null;

             do
             {
                 TableQuerySegment<PlatilloEntity> qSegment = ExecuteQuerySegment(FoodTable, tablequery, token).Result;
                 token = qSegment.ContinuationToken;

                 foreach (PlatilloEntity entity in qSegment.Results)
                 {

                     list.Add(new Platillo { Name = entity.PartitionKey, Description = entity.Description, Price = entity.Price });
                 }
             } while (token != null);


             String  jsons = JsonConvert.SerializeObject(list);

             //return Content(jsons, "application/json");
             //return Json();
             //return jsons;

             JsonResult x = new JsonResult(jsons);
             return x;

         }



         public String asd()
         {
             return "Prruebaaca";
         }

         public String showplatillo()
         {
             string str;
             str = "";

             CloudTable FoodTable = ctFood();

             TableQuery<PlatilloEntity> tablequery = new TableQuery<PlatilloEntity>();

             TableContinuationToken token = null;

             do
             {
                 TableQuerySegment<PlatilloEntity> qSegment = ExecuteQuerySegment(FoodTable, tablequery, token).Result;
                 token = qSegment.ContinuationToken;

                 foreach (PlatilloEntity entity in qSegment.Results)
                 {

                     str = "::" + str + entity.PartitionKey + " :" + entity.Description; 

                 }
             } while (token != null);


             return "platillos: " + str;
         }

         async Task<TableQuerySegment<PlatilloEntity>> ExecuteQuerySegment(CloudTable table,TableQuery<PlatilloEntity> query, TableContinuationToken token)
         {
             return await table.ExecuteQuerySegmentedAsync(query, token);
         }

         public String createplatillo()
         {
             CloudTable FoodTable = ctFood();

             PlatilloEntity plat = new PlatilloEntity();
             plat.PartitionKey = "Torta ASADA";
             plat.RowKey = "01";
             plat.Description = "Con carnita pa";
             plat.Price = 40;

             InsertTable(plat, FoodTable);

             return "se hizo 1 platillo";
         }

         /// <summary>
         /// 
         /// Private Methods....
         /// Conections and CloudTables retrieve from azure...
         /// 
         /// </summary>
         /// <returns></returns>
         ///
         private void InsertTable(TableEntity entity, CloudTable table)
         {
             TableOperation insert = TableOperation.Insert(entity);
             ExecuteInsert(table,insert);
         }
         async void ExecuteInsert(CloudTable table, TableOperation op)
         {
             await table.ExecuteAsync(op);
         }

         private CloudTableClient Connection()
         {
             CloudStorageAccount storage = new CloudStorageAccount(
             new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
             "sibatec", "IHXskJMSxkL+ZGNrRjfBZU62D0meJ1YuyXdJElXNMC4AHP7cJakBmv/hH4GEiehxWS53b9Q+SQQ2Wyhmm2q5sA=="), true);

             CloudTableClient tClient = storage.CreateCloudTableClient();

             return tClient;
         }
         private CloudTable ctFood()
         {
             CloudTable FoodTable = Connection().GetTableReference("FoodTable");
             createTableAsync(FoodTable);
             return FoodTable;
         }
         private CloudTable ctUsers()
         {
             CloudTable UsersTable = Connection().GetTableReference("UsersTable");
             createTableAsync(UsersTable);
             return UsersTable;
         }
         async void createTableAsync(CloudTable table)
         {
             await table.CreateIfNotExistsAsync();
         }

        
        public Alumno prueba()
        {
            return CreateOne();
        }
        

        public Alumno CreateOne()
        {
            Alumno al = new Alumno();
            al.NoControl = "123";
            al.Name = "dsfsd";
            al.LastName = "jose";
            al.Password = "123@sas";
            al.Semester = 10;
            al.CanCreateToken = false;
            al.Status = "pendiente";
            al.Date = "10/10/10";

            return al;
        }
        public Task<Alumno> prueba2()
        {
            var alumno = CreateOne();
            return Task.FromResult(alumno);
        }
        [HttpGet]
        public async Task<ActionResult<Alumno>> Prueba2()
        {
            //var res = await context.prueba2();
            return res;
        }


        [HttpGet]
        public ActionResult<Alumno> Prueba()
        {
            //Alumno qw = context.prueba();
            return qw;
        }



        */

    }
}

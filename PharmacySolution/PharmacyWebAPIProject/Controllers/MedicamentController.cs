using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PharmacyWebAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyWebAPIProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MedicamentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MedicamentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetAllMedicament()
        {
            try
            {
                using (var db = new MedicamentContext())
                {
                    var medicaments = db.Medicaments.ToList();

                    return new JsonResult(medicaments);
                }
            }
            catch (Exception e)
            {
                return new JsonResult(e);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PharmacyWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MedicamentController : ControllerBase
    {

        [HttpPost]
        public JsonResult AddMedicament(MedicamentModel medicament)
        {
            try
            {
                using (var db = new PharmContext())
                {
                    var _medicament = new MedicamentModel()
                    {
                        Title = medicament.Title,
                        Price = medicament.Price
                    };

                    db.Medicaments.Add(_medicament);
                    db.SaveChanges();

                    return new JsonResult("Added Successfully");
                }
            }
            catch (Exception e)
            {
                return new JsonResult(e);
            }
        }


        [HttpGet]
        public JsonResult GetAllMedicaments()
        {
            try
            {
                using (var db = new PharmContext())
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

        [HttpDelete("{id}")]
        public JsonResult DeleteMedicament(int id)
        {
            try
            {
                using (var db = new PharmContext())
                {
                    var _medicament = db.Medicaments.Where(m => m.Id == id).Single();
                    db.Medicaments.Remove(_medicament);

                    db.SaveChanges();

                    return new JsonResult("Deleted Successfully");
                }
            }
            catch (Exception e)
            {
                return new JsonResult(e);
            }
        }

        [HttpPut]
        public JsonResult UpdateMedicament(MedicamentModel medicament)
        {
            try
            {
                using (var db = new PharmContext())
                {
                    var _medicament = db.Medicaments.Where(m => m.Id == medicament.Id).Single();

                    db.Entry(_medicament).CurrentValues.SetValues(medicament);

                    db.SaveChanges();

                    return new JsonResult("Updated Successfully");
                }
            }
            catch (Exception e)
            {
                return new JsonResult(e);
            }
        }
    }
}

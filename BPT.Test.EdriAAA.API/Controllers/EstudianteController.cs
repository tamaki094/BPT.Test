using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BPT.Test.EdriAAA.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BPT.Test.EdriAAA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            using (Models.BdEstudiantesContext db = new BdEstudiantesContext())
            {
                var lst = (from e in db.Estudiantes
                           select e).ToList();

                return Ok(lst);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Models.Request.EstudianteRequest model)
        {
            using (Models.BdEstudiantesContext db = new BdEstudiantesContext())
            {
                Models.Estudiantes estudiante = new Models.Estudiantes();
                estudiante.Nombre = model.Nombre;
                estudiante.FechaNacimiento = model.FechaNacimiento;

                db.Estudiantes.Add(estudiante);
                db.SaveChanges();

                return Ok();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public ActionResult GetEstudiantes()
        {
            using (Models.BdEstudiantesContext db = new BdEstudiantesContext())
            {
                var lst = (from e in db.Estudiantes
                           select e).ToList();

                return Ok(lst);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetEstudianteById(int id)
        {
            using (Models.BdEstudiantesContext db = new BdEstudiantesContext())
            {
                var lst = (from e in db.Estudiantes
                           where e.Id == id
                           select e).FirstOrDefault();

                return Ok(lst);
            }
        }

        [HttpPost]
        public ActionResult PostEstudiante([FromBody] Models.Request.EstudianteRequest model)
        {
            try
            {
                using (Models.BdEstudiantesContext db = new BdEstudiantesContext())
                {
                    Models.Estudiantes estudiante = new Models.Estudiantes();
                    estudiante.Nombre = model.Nombre;
                    estudiante.FechaNacimiento = model.FechaNacimiento;

                    db.Estudiantes.Add(estudiante);
                    db.SaveChanges();

                    object respuesta = new { Codigo = 1, Mensje = "Guardado" };
                    return Ok(HttpStatusCode.OK);
                }
            }
            catch (Exception)
            {
                return Ok(HttpStatusCode.BadRequest);
            }
           
        }

        [HttpPut]
        public ActionResult PutEstudiante([FromBody] Models.Request.EstudianteIdRequest model)
        {
            try
            {
                using (Models.BdEstudiantesContext db = new BdEstudiantesContext())
                {
                    Models.Estudiantes estudiante = db.Estudiantes.Find(model.ID);
                    estudiante.Nombre = model.Nombre;
                    estudiante.FechaNacimiento = model.FechaNacimiento;

                    db.Entry(estudiante).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    return Ok(HttpStatusCode.OK);
                }
            }
            catch (Exception ex )
            {

                return Ok(HttpStatusCode.BadRequest);
            }
           
        }


        [HttpDelete]
        public ActionResult DeleteEstudiante([FromBody] Models.Request.EstudianteIdRequest model)
        {
            try
            {
                using (Models.BdEstudiantesContext db = new BdEstudiantesContext())
                {
                    Models.Estudiantes estudiante = db.Estudiantes.Find(model.ID);
                    db.Estudiantes.Remove(estudiante);
                    db.SaveChanges();

                    return Ok(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Ok(HttpStatusCode.BadRequest);
            }
            
        }
    }
}
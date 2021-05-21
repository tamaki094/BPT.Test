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
    public class AsignacionesEstudianteController : ControllerBase
    {
        [HttpGet("ConsultarAsignacionesEstudiante")]
        public ActionResult GetAsignacionesEstudiante()
        {
            using (Models.BdEstudiantesContext db = new BdEstudiantesContext())
            {
                var lst = (from ae in db.AsignacionesEstudiante
                           join e in db.Estudiantes on ae.IdEstudiante equals e.Id
                           join a in db.Asignaciones on ae.IdAsignacion equals a.Id
                           select new { 
                                Estudiante = e.Nombre,
                                Asignacion = a.Nombre,
                                IdEstudiante = e.Id,
                                IdAsignacion = a.Id
                           }
                         ).ToList();

                return Ok(lst);
            }
        }
        [HttpGet("ConsultarAsignacionesByEstudiante/{id}")]
        public ActionResult GetAsignacionesByEstudiante(int id)
        {
            using (Models.BdEstudiantesContext db = new BdEstudiantesContext())
            {
                var lst = (from ae in db.AsignacionesEstudiante
                           join e in db.Estudiantes on ae.IdEstudiante equals e.Id
                           join a in db.Asignaciones on ae.IdAsignacion equals a.Id
                           where e.Id == id
                           select new
                           {
                               Estudiante = e.Nombre,
                               Asignacion = a.Nombre,
                               IdEstudiante = e.Id,
                               IdAsignacion = a.Id
                           }
                         ).ToList();

                return Ok(lst);
            }
        }

        [HttpPost("AsignarEstudiante")]
        public ActionResult PostEstudianteAsignacion([FromBody] Models.Request.AsignacionEstudianteRequest model)
        {
            try
            {
                using (Models.BdEstudiantesContext db = new BdEstudiantesContext())
                {
                    Models.AsignacionesEstudiante ae = new Models.AsignacionesEstudiante();
                    ae.IdAsignacion = model.IdAsignacion;
                    ae.IdEstudiante = model.IdEstudiante;

                    db.AsignacionesEstudiante.Add(ae);
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
    }
}

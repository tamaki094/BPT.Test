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
    public class AsignacionesController : ControllerBase
    {

        [HttpGet("ConsultarAsignaciones")]
        public ActionResult GetAsignaciones()
        {
            using (Models.BdEstudiantesContext db = new BdEstudiantesContext())
            {
                var lst = (from a in db.Asignaciones
                           select a).ToList();

                return Ok(lst);
            }
        }


        [HttpPost("GuardarAsignacion")]
        public ActionResult PostAsignacion([FromBody] Models.Request.EstudianteRequest model)
        {
            try
            {
                using (Models.BdEstudiantesContext db = new BdEstudiantesContext())
                {
                    Models.Asignaciones asignacion = new Models.Asignaciones();
                    asignacion.Nombre = model.Nombre;

                    db.Asignaciones.Add(asignacion);
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

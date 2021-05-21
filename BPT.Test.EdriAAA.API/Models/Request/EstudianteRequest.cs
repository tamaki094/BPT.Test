using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPT.Test.EdriAAA.API.Models.Request
{
    public class EstudianteRequest
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}

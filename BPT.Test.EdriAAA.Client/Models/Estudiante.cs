using System;
using System.Collections.Generic;

namespace BPT.Test.EdriAAA.Client.Models
{
    public partial class Estudiantes
    {
        public Estudiantes()
        {
            AsignacionesEstudiante = new HashSet<AsignacionesEstudiante>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public ICollection<AsignacionesEstudiante> AsignacionesEstudiante { get; set; }
    }
}

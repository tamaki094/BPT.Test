using System;
using System.Collections.Generic;

namespace BPT.Test.EdriAAA.API.Models
{
    public partial class AsignacionesEstudiante
    {
        public int Id { get; set; }
        public int? IdEstudiante { get; set; }
        public int? IdAsignacion { get; set; }

        public Asignaciones IdAsignacionNavigation { get; set; }
        public Estudiantes IdEstudianteNavigation { get; set; }
    }
}

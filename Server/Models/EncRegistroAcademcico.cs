using System;
using System.Collections.Generic;

namespace BlazorAcademico.Server.Models
{
    public partial class EncRegistroAcademcico
    {
        public EncRegistroAcademcico()
        {
            DetRegistroAcademico = new HashSet<DetRegistroAcademico>();
        }

        public int IdEncRegistroAcad { get; set; }
        public DateTime Fecha { get; set; }
        public int? EstudianteId { get; set; }
        public int? DetRegId { get; set; }

        public virtual Estudiante Estudiante { get; set; }
        public virtual ICollection<DetRegistroAcademico> DetRegistroAcademico { get; set; }
    }
}

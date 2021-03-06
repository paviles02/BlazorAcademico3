using System;
using System.Collections.Generic;

namespace BlazorAcademico.Server.Models
{
    public partial class DetRegistroAcademico
    {
        //public DetRegistroAcademico()
        //{
        //    Cursos = new HashSet<Cursos>();
        //}

        public int DetRegistroIdAcad { get; set; }
        public int? CursosId { get; set; }
        public int? EncRegistroAcademicoId { get; set; }

        public virtual EncRegistroAcademcico EncRegistroAcademico { get; set; }
        public virtual Cursos Cursos { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BlazorAcademico.Server.Models
{
    public partial class CuposCurso
    {
        public int IdCuposCurso { get; set; }
        public int Cupo { get; set; }
        public byte[] RowVersion { get; set; }
        public int? CursosId { get; set; }
        public int? Year { get; set; }

        public virtual Cursos Cursos { get; set; }
    }
}

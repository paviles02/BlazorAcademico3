using System;
using System.Collections.Generic;

namespace BlazorAcademico.Server.Models
{
    public partial class Cursos
    {
        public Cursos()
        {
            CuposCurso = new HashSet<CuposCurso>();
            Estudiante = new HashSet<Estudiante>();
        }

        public int CursoId { get; set; }
        public string NombreCurso { get; set; }
        public string Descripcion { get; set; }
        public int? OnLineMaestrosId { get; set; }
        public int? PresencialMaestrosId { get; set; }
        //public int? DetRegistroAcademicoDetRegistroIdAcad { get; set; }

        //public virtual DetRegistroAcademico DetRegistroAcademicoDetRegistroIdAcadNavigation { get; set; }
        public virtual Maestros OnLineMaestros { get; set; }
        public virtual Maestros PresencialMaestros { get; set; }
        public virtual ICollection<CuposCurso> CuposCurso { get; set; }
        public virtual ICollection<Estudiante> Estudiante { get; set; }
    }
}

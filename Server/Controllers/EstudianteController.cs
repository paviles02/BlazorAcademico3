using Microsoft.AspNetCore.Mvc;
using BlazorAcademico.Server.Models;
using BlazorAcademico.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAcademico.Server.Controllers
{
    [ApiController]
    public class EstudianteController : Controller
    {
        [HttpGet]
        [Route("api/Estudiante/GetEstudiantes")]
        public List<EstudiantesCls> GetEstudiantes()
        {
            List<EstudiantesCls> lst = new List<EstudiantesCls>();
            using (RegistroAcaContext db = new RegistroAcaContext())
            {
                lst = (from estudiante in db.Estudiante
                       select new EstudiantesCls()
                       {
                           IdEstudiante = estudiante.IdEstudiante,
                           Nombre = estudiante.Nombre
                       }).ToList();

            }
            return lst;
        }
    }
}

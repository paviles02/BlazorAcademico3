using BlazorAcademico.Server.Models;
using Microsoft.AspNetCore.Mvc;
using BlazorAcademico.Client.Repositorio;
using BlazorAcademico.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;


namespace BlazorAcademico.Server.Controllers
{
    [ApiController]
    public class RegistroAcademicoController : Controller
    {
        [HttpPost]
        [Route("api/RegistroAcad/Guardar/{data?}")]
        public ActionResult<int> Guardar(EncRegistroAcademicoCls reg)
        {


            EncRegistroAcademcico ec = null;

            using (TransactionScope transacion = new TransactionScope())
            {
                using (var db = new RegistroAcaContext())
                {
                    //1. Verificamos si hay cupo
                    var cursoAux = reg.DetRegistroAcademicosDet.First().CursoEstudiante;
                    Cursos cursoSelected = new Cursos()
                    {
                        CursoId = cursoAux.CursoId,
                        NombreCurso = cursoAux.NombreCurso
                    };

                    var cupo = db.CuposCurso.Where(cp => cp.CursosId == cursoSelected.CursoId && cp.Year == DateTime.Now.Year).First();

                    var reservas = db.DetRegistroAcademico.Where(c => c.Cursos.CursoId == cursoSelected.CursoId).Count();
                    int existencia = cupo.Cupo - int.Parse(reservas.ToString());


                    if (existencia > 0)
                    {


                        //2. Guardamos Estudiante
                        Estudiante oEstudiante = new Estudiante()
                        {
                            IdEstudiante = reg.EstudianteReg.IdEstudiante,
                            Nombre = reg.EstudianteReg.Nombre

                        };
                        db.Estudiante.Attach(oEstudiante);



                        ec = new EncRegistroAcademcico()
                        {
                            Fecha = DateTime.Now,
                            Estudiante = oEstudiante

                        };
                        ec.DetRegistroAcademico = new List<DetRegistroAcademico>();
                        DetRegistroAcademico dt = new DetRegistroAcademico();
                        foreach (var item in reg.DetRegistroAcademicosDet)
                        {
                            item.CursoEstudiante.CursoId = item.CursoEstudiante.CursoId;
                            db.Cursos.Attach(cursoSelected);
                            dt.Cursos = cursoSelected;
                            dt.CursosId = item.CursosId;
                            dt.EncRegistroAcademicoId = ec.EstudianteId;
                            dt.EncRegistroAcademico = ec;
                            ec.DetRegId = dt.DetRegistroIdAcad;
                            ec.DetRegistroAcademico.Add(dt);
                        }

                        db.EncRegistroAcademcico.Add(ec);
                         db.SaveChanges();

                        //3. Descontamos de cupo
                        var cupoUpdate = db.CuposCurso.Where(c => c.CursosId == cupo.CursosId).First();
                        cupoUpdate.Cupo -= 1;
                         db.SaveChanges();
                        transacion.Complete();
                        return 0;
                    }
                    else
                    {
                        return  View("1");

                    }
                }
            }





        }



    }
}

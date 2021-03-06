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
    public class GradoController : Controller
    {

        [HttpGet]
        [Route("api/Grado/obtenerGrado/{idGrado}")]
        public GradoCls obtenerGrado(int idGrado)
        {
            GradoCls clteCls = new GradoCls();
            using (RegistroAcaContext db = new RegistroAcaContext())
            {
                clteCls = (from grado in db.Grado
                           where grado.GradoId == idGrado
                           select new GradoCls
                           {
                               GradoId = grado.GradoId,
                               GradoNombre = grado.GradoNombre,
                               Seccion = grado.Seccion

                           }).First();

            }
            return clteCls;
        }

        [HttpPost]
        [Route("api/Grado/Guardar")]
        public async Task<ActionResult<int>> Guardar(GradoCls gradoCls)
        {

            int rpta = 0;
            try
            {

                using (RegistroAcaContext db = new RegistroAcaContext())
                {
                    Grado oGrado = new Grado();
                    if (gradoCls.GradoId == 0)
                    {
                        oGrado.GradoId = gradoCls.GradoId;
                        oGrado.GradoNombre = gradoCls.GradoNombre;
                        oGrado.Seccion = gradoCls.Seccion;
                        db.Grado.Add(oGrado);
                    }
                    else
                    {
                        Grado g = db.Grado.Where(g => g.GradoId == gradoCls.GradoId).FirstOrDefault();
                        g.GradoNombre = gradoCls.GradoNombre;
                        g.Seccion = gradoCls.Seccion;
                    }
                    await db.SaveChangesAsync();
                    rpta = 1;
                }


            }
            catch (Exception)
            {
                rpta = 0;
            }
            return rpta;
        }





        [HttpGet]
        [Route("api/Grado/EliminarGrado/{data?}")]
        public int eliminarGrado(string data)
        {
            int rpta = 0;
            try
            {
                using (RegistroAcaContext db = new RegistroAcaContext())
                {
                    int idGrado = int.Parse(data);
                    Grado grado = db.Grado.Where(g => g.GradoId == idGrado).First();
                    db.Attach(grado);
                    db.Remove(grado);
                    db.SaveChanges();
                    rpta = 1;
                }
            }
            catch (Exception)
            {

                rpta = 0;
            }
            return rpta;
        }




        [HttpGet]
        [Route("api/Grado/Filtrar/{data?}")]
        public List<Grado> Filtrar(string data)
        {
            List<Grado> lista = new List<Grado>();
            using (RegistroAcaContext db = new RegistroAcaContext())
            {
                if (data == null)
                {
                    lista = (from gd in db.Grado
                             select new Grado
                             {
                                 GradoId = gd.GradoId,
                                 GradoNombre = gd.GradoNombre,
                                 Seccion = gd.Seccion
                             }).ToList();
                }

                else
                {
                    lista = (from g in db.Grado
                             where g.GradoId.ToString().Contains(data) || g.GradoNombre.Contains(data) ||
                                   g.Seccion.Contains(data)
                             select new Grado
                             {
                                 GradoId = g.GradoId,
                                 GradoNombre = g.GradoNombre,
                                 Seccion = g.Seccion
                             }).ToList();

                }
            }
            return lista;

        }


        [HttpGet]
        [Route("api/Grado/Get")]
        public List<GradoCls> Get()
        {
            List<GradoCls> lista = new List<GradoCls>();
            using (RegistroAcaContext db = new RegistroAcaContext())
            {
                lista = (from g in db.Grado
                         select new GradoCls
                         {
                             GradoId = g.GradoId,
                             GradoNombre = g.GradoNombre,
                             Seccion = g.Seccion
                         }).ToList();
            }
            return lista;

        }



    }
}

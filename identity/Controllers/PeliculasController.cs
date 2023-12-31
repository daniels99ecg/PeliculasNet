﻿using identity.Areas.Identity.Data;
using identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace identity.Controllers
{
    public class PeliculasController : Controller
    {

        public readonly ApplicationDbContext _context;
        //Creamos el contructor
        public PeliculasController(ApplicationDbContext context)
        {
            _context = context;
        }
        // metodo Get index
        public IActionResult Index()
        {
            //Creamos lista
            IEnumerable<Peliculas> ListPeliculas = _context.Peliculas;

           
            return View(ListPeliculas);//Retorna la lista

           
        }
        //HTTP Get Create


        /// HTTP Get Create
        public IActionResult CreateA()
        {
            return View();
        }

        //Grafica
        public IActionResult Graficas()
        {
            List<Peliculas>Lista = (from peliculas in _context.Peliculas
                         group peliculas by peliculas.nombre into gr
                         orderby gr.Max(Peliculas => Peliculas.calificacion) descending
                         select new Peliculas
                         {

                             nombre = gr.Key,
                             calificacion = gr.Max(p=>p.calificacion),
                         }
                        ).Take(5).ToList();


            return StatusCode(StatusCodes.Status200OK, Lista);

            //return View();
        }

        /// POST Create
        [HttpPost]
        // para que no cargen la bd con basura - protección del formulario
        [ValidateAntiForgeryToken]
        public IActionResult CreateA(Peliculas peliculas)
        {
            if (ModelState.IsValid)
            {
                _context.Peliculas.Add(peliculas);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirige a la acción "Index"
            }
            return View();
        }
        private IActionResult RedirecToAction(string v)
        {
            throw new NotImplementedException();
        }


        /// get para editar
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            /// obtener el libro que queremos editar
            var usuario = _context.Peliculas.Find(id);
            if (usuario == null)
            {
                return NotFound("Hubo un error");
            }
            return View(usuario);
        }


        /// POST update
        [HttpPost]
        // para que no cargen la bd con basura - protección del formulario
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Peliculas peliculas)
        {
            if (ModelState.IsValid)
            {
                _context.Peliculas.Update(peliculas);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirige a la acción "Index"
            }
            return View();
        }

        // GET para eliminar directamente
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var peliculas = _context.Peliculas.Find(id);
            if (peliculas == null)
            {
                return NotFound("Hubo un error");
            }

            _context.Peliculas.Remove(peliculas);
            _context.SaveChanges();

            return RedirectToAction("Index"); // Redirige a la acción "Index" después de eliminar
        }

    }
}

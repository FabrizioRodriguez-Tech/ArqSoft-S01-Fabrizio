using Catalogo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Catalogo.Controllers
{
    public class CatalogoController : Controller
    {
        private static List<Item> _items = new()
        {
            new Item
            {
                ImagenUrl = "/images/agents/jett.png",
                Id = 1,
                Nombre = "Jett",
                Genero = "Femenino",
                Origen = "Corea del Sur",
                Rol = "Duelista",
                Descripcion = "Jett usa el viento para moverse rápidamente," +
                " evadir ataques y eliminar enemigos antes de que reaccionen"
            },
            new Item
            {
                ImagenUrl = "/images/agents/Chamber.png",
                Id = 2,
                Nombre = "Chamber",
                Genero = "Masculino",
                Origen = "Francia",
                Rol = "Centinela",
                Descripcion = "Chamber combina tecnología avanzada con armamento de alta precisión " +
                "para eliminar enemigos desde la distancia y controlar el mapa."
            },
            new Item
            {
                ImagenUrl = "/images/agents/Neon_icon-2.png",
                Id = 3,
                Nombre = "Neon",
                Genero = "Femenino",
                Origen = "Filipinas",
                Rol = "Duelista",
                Descripcion = "Neon canaliza energía eléctrica para moverse a gran velocidad " +
                "y atacar antes de que el enemigo pueda reaccionar."
            },
            new Item
            {
                ImagenUrl = "/images/agents/Miks.png",
                Id = 4,
                Nombre = "Miks",
                Genero = "Masculino",
                Origen = "Croacia",
                Rol = "Controlador",
                Descripcion = "Miks usa energía sonora y ondas musicales para apoyar a su equipo, " +
       "controlando zonas, curando aliados y desestabilizando enemigos mientras coordina el ritmo del combate."
            },
            new Item
            {
                ImagenUrl = "/images/agents/Clove_icon-2.png",
                Id = 5,
                Nombre = "Clove",
                Genero = "Femenino",
                Origen = "Escocia",
                Rol = "Controlador",
                Descripcion = "Clove manipula el campo de batalla con humo y habilidades de apoyo, " +
                "ayudando al equipo incluso después de morir."
            },
        };

        // Lista — con filtro opcional por género
        public IActionResult Index(string? genero)
        {
            var resultado = string.IsNullOrEmpty(genero)
                ? _items
                : _items.Where(i => i.Genero == genero).ToList();

            ViewBag.Generos = _items.Select(i => i.Genero).Distinct().ToList();
            ViewBag.GeneroActual = genero;

            return View(resultado);
        }

        // Detalle
        public IActionResult Detalle(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            return item == null ? NotFound() : View(item);
        }

        // Formulario — GET
        public IActionResult Agregar()
        {
            return View();
        }

        // Formulario — POST
        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);

            return RedirectToAction("Index");
        }
    }
}
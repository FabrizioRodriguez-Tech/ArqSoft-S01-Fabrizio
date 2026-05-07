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
                Id = 1,
                Nombre = "Jett",
                Genero = "Femenino",
                Origen = "Corea del Sur",
                Rol = "Duelista",
                Descripcion = "Jett usa el viento para moverse rápidamente, evadir ataques y eliminar enemigos antes de que reaccionen.",
                ImagenUrl = "/images/agents/jett.png",
                ImagenFullUrl = "/images/agents/Jett-Full.png",
                // Stats Canónicos: Alta movilidad, Daño explosivo
                StatDano = 9,
                StatUtilidad = 3,
                StatMovilidad = 10,
                StatControl = 4,
                StatSupervivencia = 6
            },
            new Item
            {
                Id = 2,
                Nombre = "Chamber",
                Genero = "Masculino",
                Origen = "Francia",
                Rol = "Centinela",
                Descripcion = "Chamber combina tecnología avanzada con armamento de alta precisión para eliminar enemigos desde la distancia y controlar el mapa.",
                ImagenUrl = "/images/agents/Chamber.png",
                ImagenFullUrl = "/images/agents/Chamber-full.png",
                // Stats Canónicos: Precisión letal, baja utilidad de equipo
                StatDano = 10,
                StatUtilidad = 2,
                StatMovilidad = 5,
                StatControl = 7,
                StatSupervivencia = 4
            },
            new Item
            {
                Id = 3,
                Nombre = "Neon",
                Genero = "Femenino",
                Origen = "Filipinas",
                Rol = "Duelista",
                Descripcion = "Neon canaliza energía eléctrica para moverse a gran velocidad y atacar antes de que el enemigo pueda reaccionar.",
                ImagenUrl = "/images/agents/Neon_icon-2.png",
                ImagenFullUrl = "/images/agents/Neon-full.png",
                // Stats Canónicos: Velocidad máxima, daño moderado
                StatDano = 7,
                StatUtilidad = 5,
                StatMovilidad = 10,
                StatControl = 6,
                StatSupervivencia = 5
            },
            new Item
            {
                Id = 4,
                Nombre = "Iso",
                Genero = "Masculino",
                Origen = "China",
                Rol = "Duelista",
                Descripcion = "Iso es un mercenario chino que entra en un estado de flujo para desmantelar a sus enemigos. Reconfigura la energía ambiental en un escudo a prueba de balas y avanza con determinación hacia su próximo duelo a muerte.",
                ImagenUrl = "/images/agents/Iso.png",
                ImagenFullUrl = "/images/agents/Iso_Full.png",
                // Stats Personalizados: Alta utilidad y control de zona
                StatDano = 8,
                StatUtilidad = 4,
                StatMovilidad = 5,
                StatControl = 6,
                StatSupervivencia = 9
            },
            new Item
            {
                Id = 5,
                Nombre = "Clove",
                Genero = "Femenino", // En el lore es No Binario, pero mantenemos tu filtro actual
                Origen = "Escocia",
                Rol = "Controlador",
                Descripcion = "Clove manipula el campo de batalla con humo y habilidades de apoyo, ayudando al equipo incluso después de morir.",
                ImagenUrl = "/images/agents/Clove_icon-2.png",
                ImagenFullUrl = "/images/agents/Clove_Full.png",
                // Stats Canónicos: Alta supervivencia (incluso post-muerte)
                StatDano = 6,
                StatUtilidad = 8,
                StatMovilidad = 5,
                StatControl = 7,
                StatSupervivencia = 10
            },
        };

        public IActionResult Index(string? genero)
        {
            var resultado = string.IsNullOrEmpty(genero)
                ? _items
                : _items.Where(i => i.Genero == genero).ToList();

            ViewBag.Generos = _items.Select(i => i.Genero).Distinct().ToList();
            ViewBag.GeneroActual = genero;

            return View(resultado);
        }

        public IActionResult Detalle(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            return item == null ? NotFound() : View(item);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            item.Id = _items.Count + 1;
            // Si el usuario no pone stats, les damos 5 por defecto para que el gráfico no rompa
            if (item.StatDano == 0) item.StatDano = 5;
            // ... repetir lógica si es necesario ...

            _items.Add(item);
            return RedirectToAction("Index");
        }
    }
}
using Catalogo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Catalogo.Controllers
{
    public class CatalogoController : Controller
    {
        // Lista maestra de agentes (Base de datos temporal)
        private static List<Item> _items = new()
        {
            new Item
            {
                Id = 1,
                Nombre = "Jett",
                Genero = "Femenino",
                Origen = "Corea del Sur",
                Rol = "Duelista",
                Descripcion = "Jett es una agente originaria de Corea del Sur reconocida por su velocidad, agilidad y estilo de combate agresivo. Utiliza el poder del viento para desplazarse rápidamente por el mapa, alcanzar posiciones elevadas y escapar de situaciones peligrosas en cuestión de segundos. Gracias a su movilidad extrema y precisión letal, puede eliminar enemigos antes de que tengan tiempo de reaccionar. Su estilo de juego recompensa los reflejos rápidos, la puntería y la toma de decisiones instantánea, convirtiéndola en una de las duelistas más populares y peligrosas de Valorant.",
                ImagenUrl = "/images/agents/jett.png",
                ImagenFullUrl = "/images/agents/Jett-Full.png",
                StatDano = 9, StatUtilidad = 3, StatMovilidad = 10, StatControl = 4, StatSupervivencia = 6
            },
            new Item
            {
                Id = 2,
                Nombre = "Chamber",
                Genero = "Masculino",
                Origen = "Francia",
                Rol = "Centinela",
                Descripcion = "Chamber es un sofisticado agente francés especializado en precisión, estrategia y control del mapa. A diferencia de otros centinelas, combina habilidades defensivas con un estilo ofensivo basado en armas de alta tecnología y disparos letales a larga distancia. Su capacidad para colocar trampas y reposicionarse rápidamente le permite mantener el control de zonas importantes mientras elimina enemigos con gran eficacia. Elegante, frío y calculador, Chamber destaca por convertir cada enfrentamiento en una demostración de precisión y disciplina táctica.",
                ImagenUrl = "/images/agents/Chamber.png",
                ImagenFullUrl = "/images/agents/Chamber-full.png",
                StatDano = 10, StatUtilidad = 2, StatMovilidad = 5, StatControl = 7, StatSupervivencia = 4
            },
            new Item
            {
                Id = 3,
                Nombre = "Neon",
                Genero = "Femenino",
                Origen = "Filipinas",
                Rol = "Duelista",
                Descripcion = "Neon es una duelista filipina capaz de canalizar enormes cantidades de energía eléctrica para aumentar su velocidad y poder ofensivo. Su estilo de combate se basa en la rapidez, permitiéndole correr, deslizarse y entrar agresivamente en las zonas enemigas antes de que puedan reaccionar. Además de su movilidad extrema, puede crear barreras eléctricas y lanzar descargas que desorientan a sus rivales. Neon representa un estilo dinámico y explosivo, ideal para jugadores que disfrutan de la acción constante y ataques veloces.",
                ImagenUrl = "/images/agents/Neon_icon-2.png",
                ImagenFullUrl = "/images/agents/Neon-full.png",
                StatDano = 7, StatUtilidad = 5, StatMovilidad = 10, StatControl = 6, StatSupervivencia = 5
            },
            new Item
            {
                Id = 4,
                Nombre = "Iso",
                Genero = "Masculino",
                Origen = "China",
                Rol = "Duelista",
                Descripcion = "Iso es un mercenario chino disciplinado y reservado que se especializa en dominar enfrentamientos individuales. Gracias a su concentración y control mental, puede entrar en un estado de flujo que le permite protegerse temporalmente del daño y aumentar sus posibilidades de supervivencia en combate. Su estilo de juego está diseñado para aislar enemigos y ganar duelos directos mediante precisión y sangre fría. Iso transmite una personalidad seria y calculadora, enfocada completamente en la eficiencia y la eliminación estratégica de objetivos.",
                ImagenUrl = "/images/agents/Iso.png",
                ImagenFullUrl = "/images/agents/Iso_Full.png",
                StatDano = 8, StatUtilidad = 4, StatMovilidad = 5, StatControl = 6, StatSupervivencia = 9
            },
            new Item
            {
                Id = 5,
                Nombre = "Clove",
                Genero = "Femenino",
                Origen = "Escocia",
                Rol = "Controlador",
                Descripcion = "Clove es una controladora escocesa con habilidades sobrenaturales que le permiten influir en la batalla incluso después de morir. Utiliza humo y energía mística para bloquear la visión enemiga, apoyar a su equipo y mantener presión constante sobre el mapa. A diferencia de otros agentes de control, Clove combina utilidad táctica con un estilo más agresivo y flexible, permitiendo participar activamente en los enfrentamientos. Su personalidad relajada y desafiante refleja perfectamente su forma impredecible de combatir.",
                ImagenUrl = "/images/agents/Clove_icon-2.png",
                ImagenFullUrl = "/images/agents/Clove_Full.png",
                StatDano = 6, StatUtilidad = 8, StatMovilidad = 5, StatControl = 7, StatSupervivencia = 10
            }
        };

        // VISTA PRINCIPAL: Lista de agentes con filtros
        public IActionResult Index(string? genero)
        {
            var resultado = string.IsNullOrEmpty(genero)
                ? _items
                : _items.Where(i => i.Genero == genero).ToList();

            ViewBag.Generos = _items.Select(i => i.Genero).Distinct().ToList();
            ViewBag.GeneroActual = genero;

            return View(resultado);
        }

        // VISTA DE DETALLE: Expediente del agente
        public IActionResult Detalle(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            return item == null ? NotFound() : View(item);
        }

        // VISTA AGREGAR: Formulario de reclutamiento
        public IActionResult Agregar()
        {
            return View();
        }

        // PROCESO DE AGREGAR: Lógica de guardado y disparo de cinemática
        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            // 1. Generar ID automático
            item.Id = _items.Any() ? _items.Max(i => i.Id) + 1 : 1;

            // 2. Lógica de "Imagen Desconocida" para activar el CSS
            // Al dejarlo como string vacío, el CSS aplicará el efecto de silueta.
            if (string.IsNullOrWhiteSpace(item.ImagenUrl))
            {
                item.ImagenUrl = "";
            }
            if (string.IsNullOrWhiteSpace(item.ImagenFullUrl))
            {
                item.ImagenFullUrl = "";
            }

            // 3. Stats equilibrados por defecto si vienen en 0
            if (item.StatDano == 0) item.StatDano = 5;
            if (item.StatUtilidad == 0) item.StatUtilidad = 5;
            if (item.StatMovilidad == 0) item.StatMovilidad = 5;
            if (item.StatControl == 0) item.StatControl = 5;
            if (item.StatSupervivencia == 0) item.StatSupervivencia = 5;

            // 4. Guardar en la lista
            _items.Add(item);

            // 5. Señal para el motor de cinemáticas en Agregar.cshtml
            TempData["AgenteReclutado"] = true;

            // Retornamos la vista para que el script de cinemática pueda ejecutarse
            return View();
        }
    }
}
namespace Catalogo.Models
{
    public class Item

    {
        public string ImagenUrl { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Origen { get; set; }
        public string Rol { get; set; }
        public string Descripcion { get; set; }

        public string? ImagenFullUrl { get; set; } // Imagen de cuerpo completo

        
        public int StatDano { get; set; }
        public int StatUtilidad { get; set; }
        public int StatMovilidad { get; set; }
        public int StatControl { get; set; }
        public int StatSupervivencia { get; set; }
    }


}

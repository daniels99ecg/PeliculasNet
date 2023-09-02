using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace identity.Models
{
    public class Peliculas
    {
        

        public int id { get; set; }
        public string? url2 { get; set; }

        public string?  nombre { get; set; }

        public string? sinopsis { get; set; }

        public string? director { get; set; }

        public string? genero { get; set; }

        public int calificacion { get; set; }



    }
}

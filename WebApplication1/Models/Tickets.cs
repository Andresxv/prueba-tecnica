using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPruebaBackend.Models
{
    public class Tickets
    {
        [Key]
        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public  DateTime FechaActualizacion { get; set; }
        public bool Estatus { get; set; }
    }
}

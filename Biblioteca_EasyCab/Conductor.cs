using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_EasyCab
{
    public class Conductor:Persona
    {
        public int Id_Conductor { get; set; }
        public float Comision { get; set; }
        public string Categoria { get; set; }
        public DateTime Fecha_Expiracion { get; set; }
    }
}

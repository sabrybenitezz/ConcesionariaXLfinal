using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcesionariaVehiculos.DTOs
{
    public class ServicioPosVenta
    {
        public int ClienteId { get; set; }
        public string TipoServicio { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
    }
}
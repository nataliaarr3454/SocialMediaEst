using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.CustomEntities
{
    public class CrecimientoMensualComentariosResponse
    {
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int TotalComentarios { get; set; }
        public int? MesAnterior { get; set; }
        public double? CrecimientoPorcentual { get; set; }
    }
}

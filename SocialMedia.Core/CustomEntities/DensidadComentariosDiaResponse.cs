using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.CustomEntities
{
    public class DensidadComentariosDiaResponse
    {
        public string DiaSemana { get; set; }
        public int TotalComentarios { get; set; }
        public int UsuariosUnicos { get; set; }
    }
}

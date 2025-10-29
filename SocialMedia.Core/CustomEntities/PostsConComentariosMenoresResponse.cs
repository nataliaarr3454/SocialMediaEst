using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.CustomEntities
{
    public class PostsConComentariosMenoresResponse
    {
        public int IdPost { get; set; }
        public string PostDescription { get; set; }
        public int ComentariosMenores { get; set; }
    }
}

using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPostAsync(PostQueryFilter postQueryFilter);
        Task<IEnumerable<Post>> GetAllPostDapperAsync();
        Task<IEnumerable<PostComentariosUsersResponse>> GetPostCommentUserAsync();
        Task<IEnumerable<UsuariosSinComentariosResponse>> GetUsuariosSinComentariosAsync();//1
       
        Task<IEnumerable<ComentariosTresMesesResponse>> GetComentariosTresMesesUsuariosMayoresAsync();//2
        Task<IEnumerable<PostsSinComentariosResponse>> GetPostsSinComentariosUsuariosActivosAsync();//3
        Task<IEnumerable<UsuariosComentanPostsDiferentesResponse>> GetUsuariosComentanPostsDiferentesAsync();//4
        Task<IEnumerable<PostsConComentariosMenoresResponse>> GetPostsConComentariosMenoresAsync();//5
        Task<IEnumerable<DensidadComentariosDiaResponse>> GetDensidadComentariosPorDiaAsync();//6
        Task<IEnumerable<CrecimientoMensualComentariosResponse>> GetCrecimientoMensualComentariosAsync();//7


        Task<Post> GetPostAsync(int id);
        Task InsertPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id);
    }
}

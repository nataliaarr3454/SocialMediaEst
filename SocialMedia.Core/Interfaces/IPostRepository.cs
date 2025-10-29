using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> GetAllPostByUserAsync(int idUser);
        Task<IEnumerable<Post>> GetAllPostDapperAsync(int limit = 10);
        Task<IEnumerable<PostComentariosUsersResponse>> GetPostCommentUserAsync();
        //Task<Post> GetPostAsync(int id);
        //Task InsertPostAsync(Post post);
        //Task UpdatePostAsync(Post post);
        //Task DeletePostAsync(Post post);
        //1
        Task<IEnumerable<UsuariosSinComentariosResponse>> GetUsuariosSinComentariosAsync();
        //2
        Task<IEnumerable<ComentariosTresMesesResponse>> GetComentariosTresMesesUsuariosMayoresAsync();
       //3
        Task<IEnumerable<PostsSinComentariosResponse>> GetPostsSinComentariosUsuariosActivosAsync();
       //4
      
       Task<IEnumerable<UsuariosComentanPostsDiferentesResponse>> GetUsuariosComentanPostsDiferentesAsync();
        //5
       Task<IEnumerable<PostsConComentariosMenoresResponse>> GetPostsConComentariosMenoresAsync();
       
    }
}
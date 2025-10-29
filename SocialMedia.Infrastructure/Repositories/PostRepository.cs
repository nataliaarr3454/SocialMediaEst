using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enum;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Queries;
using System.Security.Cryptography;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly IDapperContext _dapper;
        //private readonly SocialMediaContext _context;
        public PostRepository(SocialMediaContext context, IDapperContext dapper) : base(context)
        {
            _dapper = dapper;
            //_context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPostByUserAsync(int idUser)
        {
            var posts = await _entities.Where(x => x.UserId == idUser).ToListAsync();
            return posts;
        }


        public async Task<IEnumerable<Post>> GetAllPostDapperAsync(int limit = 10)
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => PostQueries.PostQuerySqlServer,
                    DatabaseProvider.MySql => PostQueries.PostQueryMySQl,
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<Post>(sql, new { Limit = limit });
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<IEnumerable<PostComentariosUsersResponse>> GetPostCommentUserAsync()
        {
            try
            {
                var sql = PostQueries.PostComentadosUsuariosActivos;

                return await _dapper.QueryAsync<PostComentariosUsersResponse>(sql);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        //public async Task<Post> GetPostAsync(int id)
        //{
        //    var post = await _context.Posts.FirstOrDefaultAsync(
        //        x => x.Id == id);
        //    return post;
        //}

        //public async Task InsertPostAsync(Post post)
        //{
        //    _context.Posts.Add(post);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task UpdatePostAsync(Post post)
        //{
        //    _context.Posts.Update(post);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeletePostAsync(Post post)
        //{
        //    _context.Posts.Remove(post);
        //    await _context.SaveChangesAsync();
        //}
        //1
        public async Task<IEnumerable<UsuariosSinComentariosResponse>> GetUsuariosSinComentariosAsync()
        {
            
            try
            {
                var sql = PostQueries.UsuariosSinComentarios;
                return await _dapper.QueryAsync<UsuariosSinComentariosResponse>(sql);
            }
            catch (Exception err)
            {
                throw new Exception($"Error al obtener usuarios sin comentarios: {err.Message}");
            }
        }
        public async Task<IEnumerable<ComentariosTresMesesResponse>> GetComentariosTresMesesUsuariosMayoresAsync()
        {
            try
            {
                var sql = PostQueries.ComentariosTresMesesUsuariosMayores;
                return await _dapper.QueryAsync<ComentariosTresMesesResponse>(sql);
            }
            catch (Exception err)
            {
                throw new Exception($"Error al obtener los comentarios de los últimos 3 meses: {err.Message}");
            }
        }
        //3
        public async Task<IEnumerable<PostsSinComentariosResponse>> GetPostsSinComentariosUsuariosActivosAsync()
        {
            try
            {
                var sql = PostQueries.PostsSinComentariosUsuariosActivos;
                return await _dapper.QueryAsync<PostsSinComentariosResponse>(sql);
            }
            catch (Exception err)
            {
                throw new Exception($"Error al obtener los posts sin comentarios activos: {err.Message}");
            }
        }

        //4
        public async Task<IEnumerable<UsuariosComentanPostsDiferentesResponse>> GetUsuariosComentanPostsDiferentesAsync()
        {
            try
            {
                var sql = PostQueries.UsuariosComentanPostsDiferentes;
                return await _dapper.QueryAsync<UsuariosComentanPostsDiferentesResponse>(sql);
            }
            catch (Exception err)
            {
                throw new Exception($"Error al obtener usuarios que comentaron en posts de diferentes usuarios: {err.Message}");
            }
        }
        //5
        public async Task<IEnumerable<PostsConComentariosMenoresResponse>> GetPostsConComentariosMenoresAsync()
        {
            try
            {
                var sql = PostQueries.PostsConComentariosMenores;
                return await _dapper.QueryAsync<PostsConComentariosMenoresResponse>(sql);
            }
            catch (Exception err)
            {
                throw new Exception($"Error al obtener los posts con comentarios de menores: {err.Message}");
            }
        }
       
    }
}

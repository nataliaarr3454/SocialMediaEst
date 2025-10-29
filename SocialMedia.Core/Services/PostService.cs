﻿using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        //public readonly IPostRepository _postRepository;
        //public readonly IBaseRepository<Post> _postRepository;
        //public readonly IUserRepository _userRepository;
        //public readonly IBaseRepository<User> _userRepository;

        public readonly IUnitOfWork _unitOfWork;

        public PostService(
            //IPostRepository postRepository, 
            //IUserRepository userRepository) 
            //IBaseRepository<Post> postRepository,
            //IBaseRepository<User> userRepository,
            IUnitOfWork unitOfWork)
        {
            //_postRepository = postRepository;
            //_userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Post>> GetAllPostAsync(
            PostQueryFilter postQueryFilter)
        {
            var posts = await _unitOfWork.PostRepository.GetAll();
            if (postQueryFilter.userId != null)
            {
                posts = posts.Where(a => a.UserId == postQueryFilter.userId);
            }
            if (postQueryFilter.Date != null)
            {
                posts = posts.Where(x => x.Date.ToShortDateString() ==
                postQueryFilter.Date?.ToShortDateString());
            }
            if (postQueryFilter.Description != null)
            {
                posts = posts.Where(x =>
                x.Description.ToLower().Contains(postQueryFilter.Description.ToLower()));
            }

            //return await _postRepository.GetAll();
            //return await _unitOfWork.PostRepository.GetAll();
            return posts;
        }

        public async Task<IEnumerable<Post>> GetAllPostDapperAsync()
        {
            var posts = await _unitOfWork.PostRepository.GetAllPostDapperAsync(5);

            return posts;
        }

        public async Task<IEnumerable<PostComentariosUsersResponse>> GetPostCommentUserAsync()
        {
            var posts = await _unitOfWork.PostRepository.GetPostCommentUserAsync();

            return posts;
        }

        public async Task<Post> GetPostAsync(int id)
        {
            //return await _postRepository.GetById(id);
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public async Task InsertPostAsync(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new BussinesException("El usuario no existe");
            }

            if (ContainsForbiddenWord(post.Description))
            {
                throw new BussinesException("El contenido no es permitido");
            }
            //Si el usuario tiene menos de 10 publicaciones,
            //solo puede publicar 1 sola vez por semana
            var userPost = await _unitOfWork.PostRepository
                .GetAllPostByUserAsync(post.UserId);
            if (userPost.Count() < 7)
            {
                var lastPost = userPost.OrderByDescending(x => x.Date).FirstOrDefault();
                if ((DateTime.Now - lastPost.Date).TotalDays < 7)
                {
                    throw new BussinesException("No puedes publicar el post");
                }
            }

            await _unitOfWork.PostRepository.Add(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            await _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeletePostAsync(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        //Lista de palabras no permitidas
        public readonly string[] ForbiddensWords =
        {
            "violencia",
            "odio",
            "groseria",
            "discriminacion"
        };

        public bool ContainsForbiddenWord(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;

            foreach (var word in ForbiddensWords)
            { 
                if (text.Contains(word, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
        //1
        public async Task<IEnumerable<UsuariosSinComentariosResponse>> GetUsuariosSinComentariosAsync()
        {
            var usuarios = await _unitOfWork.PostRepository.GetUsuariosSinComentariosAsync();
            return usuarios;
        }
        //2
        public async Task<IEnumerable<ComentariosTresMesesResponse>> GetComentariosTresMesesUsuariosMayoresAsync()
        {
            var comentarios = await _unitOfWork.PostRepository.GetComentariosTresMesesUsuariosMayoresAsync();
            return comentarios;
        }
        //3
        public async Task<IEnumerable<PostsSinComentariosResponse>> GetPostsSinComentariosUsuariosActivosAsync()
        {
            var posts = await _unitOfWork.PostRepository.GetPostsSinComentariosUsuariosActivosAsync();
            return posts;
        }
        //4
        public async Task<IEnumerable<UsuariosComentanPostsDiferentesResponse>> GetUsuariosComentanPostsDiferentesAsync()
        {
            var usuarios = await _unitOfWork.PostRepository.GetUsuariosComentanPostsDiferentesAsync();
            return usuarios;
        }
        //5
        public async Task<IEnumerable<PostsConComentariosMenoresResponse>> GetPostsConComentariosMenoresAsync()
        {
            var posts = await _unitOfWork.PostRepository.GetPostsConComentariosMenoresAsync();
            return posts;
        }
        //6
        public async Task<IEnumerable<DensidadComentariosDiaResponse>> GetDensidadComentariosPorDiaAsync()
        {
            var densidad = await _unitOfWork.PostRepository.GetDensidadComentariosPorDiaAsync();
            return densidad;
        }
        //7
        public async Task<IEnumerable<CrecimientoMensualComentariosResponse>> GetCrecimientoMensualComentariosAsync()
        {
            var crecimiento = await _unitOfWork.PostRepository.GetCrecimientoMensualComentariosAsync();
            return crecimiento;
        }


    }
}

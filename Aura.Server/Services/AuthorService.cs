﻿namespace Aura.Server.Services
{
    using System.Linq;
    using Data;
    using Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Mapping;

    public class AuthorService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly string _userName;

        public AuthorService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = dbContext;
            _userName = httpContextAccessor?.HttpContext?.User.Identity?.Name;
        }

        public bool IsAuthor()
        {
            return _applicationDbContext.Authors.Any(a => a.UserName == _userName);
        }

        public AuthorProfileModel GetAsAuthor()
        {
            var author = _applicationDbContext.Authors
                .Include(a => a.Tracks)
                .Include(a => a.Albums)
                .SingleOrDefault(a => a.UserName == _userName);

            return author?.MapAuthorProfileModel();
        }

        public void MakeAuthor(string nickname)
        {
            var user = _applicationDbContext.Users.Single(u => u.UserName == _userName);
            var author = new Author(user)
            {
                Nickname = nickname
            };

            _applicationDbContext.Users.Remove(user);
            _applicationDbContext.Authors.Add(author);
            _applicationDbContext.SaveChanges();
        }
    }
}
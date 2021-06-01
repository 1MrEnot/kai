namespace Aura.Server.Services
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class AuthorService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly string _userName;

        public AuthorService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = dbContext;
            _userName = httpContextAccessor?.HttpContext?.User.Identity?.Name;
        }

        public async Task<bool> IsAuthorAsync()
        {
            var fond = await _applicationDbContext.Authors.SingleOrDefaultAsync(a => a.UserName == _userName);
            return fond is not null;
        }

        public async Task<Author> GetAsAuthor()
        {
            return await _applicationDbContext.Authors.SingleOrDefaultAsync(u => u.UserName == _userName);
        }

        public async Task MakeAuthor(string nickname)
        {
            var user = await _applicationDbContext.Users.SingleAsync(u => u.UserName == _userName);
            var author = new Author(user)
            {
                Nickname = nickname
            };

            _applicationDbContext.Users.Remove(user);
            await _applicationDbContext.Authors.AddAsync(author);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
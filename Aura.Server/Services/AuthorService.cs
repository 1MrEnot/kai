namespace Aura.Server.Services
{
    using System.Linq;
    using System.Threading.Tasks;
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

        public async Task<bool> IsAuthorAsync()
        {
            var fond = await _applicationDbContext.Authors.SingleOrDefaultAsync(a => a.UserName == _userName);
            return fond is not null;
        }

        public bool IsAuthor()
        {
            var fond = _applicationDbContext.Authors.SingleOrDefault(a => a.UserName == _userName);
            return fond is not null;
        }

        public AuthorProfileModel GetAsAuthor()
        {
            var tracks = _applicationDbContext.Tracks.ToList();
            var authors = _applicationDbContext.Authors.Include(a => a.Tracks).ToList();

            var author = _applicationDbContext.Authors
                .Include(a => a.Tracks)
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
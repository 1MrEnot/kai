namespace Aura.Server.Services
{
    using System.Linq;
    using Data;
    using Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Mapping;

    public class UserService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly TrackDataDbContext _trackDataDbContext;
        private readonly string _userName;
        private readonly object _lock;

        public UserService(ApplicationDbContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            TrackDataDbContext trackDataDbContext)
        {
            _applicationDbContext = dbContext;
            _trackDataDbContext = trackDataDbContext;
            _userName = httpContextAccessor?.HttpContext?.User.Identity?.Name;
            _lock = new object();
        }

        public bool IsAuthor()
        {
            lock (_lock)
            {
                return _applicationDbContext.Authors.Any(a => a.UserName == _userName);
            }
        }

        public AuthorProfileModel GetAsAuthor()
        {
            lock (_lock)
            {
                var author = _applicationDbContext.Authors
                    .Include(a => a.Tracks)
                    .Include(a => a.Albums)
                    .SingleOrDefault(a => a.UserName == _userName);

                return author?.MapAuthorProfileModel(_trackDataDbContext);
            }
        }

        public UserModel GetUser()
        {
            lock (_lock)
            {
                var user = _applicationDbContext.Users
                    .Include(a => a.SavedAlbums)
                    .Include(a => a.SavedTracks)
                    .SingleOrDefault(a => a.UserName == _userName);

                return user?.MapModel(_trackDataDbContext);
            }
        }

        public void MakeAuthor(string nickname)
        {
            lock (_lock)
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
}
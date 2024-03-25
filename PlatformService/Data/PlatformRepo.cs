using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _appDbContext;

        public PlatformRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));

            }
            else
            {
                _appDbContext.Platforms.Add(platform);
            }
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _appDbContext.Platforms.ToList();
        }

        public Platform getPlatformById(int Id)
        {
            return _appDbContext.Platforms.FirstOrDefault(p => p.Id == Id);
        }

        public bool SaveChanges()
        {
            return _appDbContext.SaveChanges() > 0;
        }
    }
}
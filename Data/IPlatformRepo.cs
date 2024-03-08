using PlatformService.Models;

namespace PlatformService.Data{
    public interface IPlatformRepo{

        bool SaveChanges();
        void CreatePlatform(Platform platform);
        Platform getPlatformById(int Id);
        IEnumerable<Platform> GetAllPlatforms();

    }
}
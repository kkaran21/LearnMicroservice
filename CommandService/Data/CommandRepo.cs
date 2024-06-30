using CommandService.Models;

namespace CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _appDbContext;
        public CommandRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void createCommand(int PlatformId, Command command)
        {
            if (platformExists(PlatformId) && command != null)
            {
                command.PlatformId = PlatformId;
            }

            _appDbContext.Add(command);
        }

        public void createPlatform(Platform platform)
        {
            if(platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
            _appDbContext.Platforms.Add(platform);
        }

        public bool externalPlatformExists(int externalPlatformId)
        {
            return _appDbContext.Platforms.Any(p => p.ExternalId == externalPlatformId);
        }

        public IEnumerable<Platform> getAllPlatforms()
        {
            return _appDbContext.Platforms.ToList();
        }

        public Command getCommand(int PlatformId, int CommandId)
        {
            return _appDbContext.Commands.Where(c => c.PlatformId == PlatformId && c.Id == CommandId).FirstOrDefault();
        }

        public IEnumerable<Command> getCommandsForPlatform(int PlatformId)
        {
            return _appDbContext.Commands.Where(c => c.PlatformId == PlatformId);
        }

        public bool platformExists(int platformId)
        {
            return _appDbContext.Platforms.Any(p => p.Id == platformId);
        }

        public bool SaveChanges()
        {
            return _appDbContext.SaveChanges() >= 0;
        }
    }
}
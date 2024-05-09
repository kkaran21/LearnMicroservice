using CommandService.Models;

namespace CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext appDbContext;
        public CommandRepo(AppDbContext appDbContext)
        {
            appDbContext = this.appDbContext;
        }

        public void createCommand(int PlatformId, Command command)
        {
             command.PlatformId = PlatformId;
             appDbContext.Add(command);
        }

        public void createPlatform(Platform platform)
        {
             appDbContext.Platforms.Add(platform);
        }

        public IEnumerable<Platform> getAllPlatforms()
        {
            return appDbContext.Platforms.ToList();
        }

        public Command getCommand(int PlatformId, int CommandId)
        {
            return appDbContext.Commands.Where(c => c.PlatformId == PlatformId && c.Id == CommandId).FirstOrDefault();
        }

        public IEnumerable<Command> getCommandsForPlatform(int PlatformId)
        {
            return appDbContext.Commands.Where(c => c.PlatformId == PlatformId);
        }

        public bool platformExists(int platformId)
        {
            return appDbContext.Platforms.Any(p => p.Id == platformId);
        }

        public bool SaveChanges()
        {
            return appDbContext.SaveChanges() >= 0;
        }
    }
}
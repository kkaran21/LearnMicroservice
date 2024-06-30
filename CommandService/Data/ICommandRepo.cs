using CommandService.Models;

namespace CommandService.Data
{
    public interface ICommandRepo
    {
        bool SaveChanges();

        //platform
        IEnumerable<Platform> getAllPlatforms();
        void createPlatform(Platform platform);
        bool platformExists(int platformId);
        bool externalPlatformExists(int externalPlatformId);



        //Command
        IEnumerable<Command> getCommandsForPlatform(int PlatformId);
        Command getCommand(int PlatformId, int CommandId);
        void createCommand(int PlatformId, Command command);
        
    }
}
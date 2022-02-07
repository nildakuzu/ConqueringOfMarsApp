using ConqueringOfMars.Model;

namespace ConqueringOfMars.Interface
{
    public interface IRoverSpeaker
    {
        string SayIdentity(RoverModel rover);

        string SayCoordinate(RoverModel roverModel);

        string SayNavigation(string msg);
    }
}

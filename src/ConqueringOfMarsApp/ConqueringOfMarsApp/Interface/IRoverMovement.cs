using ConqueringOfMars.Consant.Enum;
using ConqueringOfMars.Model;

namespace ConqueringOfMars.Interface
{
    public interface IRoverMovement
    {
        void SearchFacingCompassPoint(EnmInstruction currentInstruction, RoverModel rover);

        CoordinateModel GetNextCoordinate(RoverModel rover);

        void Move(RoverModel rover, CoordinateModel nextStep);

        void StartExploring(RoverModel rover);

    }
}

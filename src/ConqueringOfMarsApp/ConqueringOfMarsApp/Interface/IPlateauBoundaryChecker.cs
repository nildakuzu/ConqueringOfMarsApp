using ConqueringOfMars.Model;

namespace ConqueringOfMars.Interface
{
    public interface IPlateauBoundaryChecker
    {
        bool CheckOutOfBoundary(RoverModel roverModel, CoordinateModel nextStep);
    }
}

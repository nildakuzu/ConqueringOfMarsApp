using ConqueringOfMars.Interface;
using ConqueringOfMars.Model;

namespace ConqueringOfMars.Class
{
    public class PlateauBoundaryChecker : IPlateauBoundaryChecker
    {
        public bool CheckOutOfBoundary(RoverModel roverModel, CoordinateModel nextStep)
        {
            return (nextStep.Coordinate_X < 0 || nextStep.Coordinate_X > roverModel.MaxExploringCoordinate.Coordinate_X)
                || (nextStep.Coordinate_Y < 0 || nextStep.Coordinate_Y > roverModel.MaxExploringCoordinate.Coordinate_Y);
        }
    }
}

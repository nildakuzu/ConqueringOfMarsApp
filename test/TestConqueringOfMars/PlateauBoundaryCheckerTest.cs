using ConqueringOfMars.Class;
using ConqueringOfMars.Model;
using Xunit;

namespace TestConqueringOfMars
{
    public class PlateauBoundaryCheckerTest
    {
        [Fact]
        public void IfItIsOutOfBoundary_ReturnTrue()
        {
            var roverModel = new RoverModel()
            {
                MaxExploringCoordinate = new CoordinateModel(2, 2),
            };

            var nextCoordinate = new CoordinateModel(3, 3);

            var outOfBoundary = new PlateauBoundaryChecker().CheckOutOfBoundary(roverModel, nextCoordinate);

            Assert.True(outOfBoundary);
        }

        [Fact]
        public void IfItIsNotOutOfBoundary_ReturnFalse()
        {
            var roverModel = new RoverModel()
            {
                MaxExploringCoordinate = new CoordinateModel(3, 3),
            };

            var nextCoordinate = new CoordinateModel(3, 3);

            var outOfBoundary = new PlateauBoundaryChecker().CheckOutOfBoundary(roverModel, nextCoordinate);

            Assert.False(outOfBoundary);
        }
    }
}

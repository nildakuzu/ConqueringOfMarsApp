using ConqueringOfMars.Consant;
using ConqueringOfMars.Consant.Enum;
using ConqueringOfMars.Model;
using System;
using System.Text;
using Xunit;

namespace TestConqueringOfMars
{
    public class RoverSpeakerTest
    {
        [Fact]
        public void SayCoordinate_For_Actual_Coordinate()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.North,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            var coordinateMsg = $"I am on ({roverModel.CurrentCoordinate.Coordinate_X},{roverModel.CurrentCoordinate.Coordinate_Y})";

            Assert.Equal("I am on (1,2)", coordinateMsg);
        }

        [Fact]
        public void SayNavigation_For_Actual_Navigation()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.North,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            var navigationMsg = RoverNavigationMessage.Moving;

            Assert.Equal("I am moving", navigationMsg);
        }

        [Fact]
        public void SayIdentity_For_Rover()
        {
            var rover = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                Mission = "Mission",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.North,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
            };

            var sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine($" I am {rover.Identity} {rover.Name}");
            sb.AppendLine($" My Mission is {rover.Mission}");

            Assert.Equal("\r\n I am MyIdentity MyName\r\n My Mission is Mission\r\n", sb.ToString());
        }
    }
}

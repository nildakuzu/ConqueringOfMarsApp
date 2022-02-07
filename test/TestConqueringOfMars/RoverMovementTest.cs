using ConqueringOfMars.Class;
using ConqueringOfMars.Consant;
using ConqueringOfMars.Consant.Enum;
using ConqueringOfMars.Interface;
using ConqueringOfMars.Model;
using Moq;
using System;
using Xunit;

namespace TestConqueringOfMars
{
    public class RoverMovementTest
    {
        private readonly Mock<IRoverSpeaker> roverSpeakerMock;
        private readonly Mock<IConverter> converterMock;
        private readonly Mock<IPlateauBoundaryChecker> plateauBoundaryCheckerMock;
        private readonly Mock<IRoverMovement> roverMovementMock;

        public RoverMovementTest()
        {
            roverSpeakerMock = new Mock<IRoverSpeaker>(MockBehavior.Loose);
            converterMock = new Mock<IConverter>(MockBehavior.Loose);
            plateauBoundaryCheckerMock = new Mock<IPlateauBoundaryChecker>(MockBehavior.Loose);
            roverMovementMock = new Mock<IRoverMovement>(MockBehavior.Loose);
        }


        [Fact]
        public void StartExploring_SayIdentity_Should_Be_Triggered_Once()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>(),
            };

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).StartExploring(roverModel);

            roverSpeakerMock.Verify(e => e.SayIdentity(It.IsAny<RoverModel>()), Times.Once());
        }

        [Fact]
        public void StartExploring_If_There_Is_A_Move_Command_Say_Coordinate()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "M" },
                FacingCompassPoint = EnmCompassPoint.North,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            plateauBoundaryCheckerMock.Setup(s => s.CheckOutOfBoundary(It.IsAny<RoverModel>(), It.IsAny<CoordinateModel>())).Returns(false);
            converterMock.Setup(s => s.ConvertInstructionStrToEnum(It.IsAny<string>())).Returns(EnmInstruction.Move);

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).StartExploring(roverModel);

            roverSpeakerMock.Verify(e => e.SayCoordinate(It.IsAny<RoverModel>()), Times.Once());
        }

        [Fact]
        public void StartExploring_If_It_Is_OutOfBoundary_Say_Navigation_For_Boundary()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "M" },
                FacingCompassPoint = EnmCompassPoint.North,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            plateauBoundaryCheckerMock.Setup(s => s.CheckOutOfBoundary(It.IsAny<RoverModel>(), It.IsAny<CoordinateModel>())).Returns(true);
            converterMock.Setup(s => s.ConvertInstructionStrToEnum(It.IsAny<string>())).Returns(EnmInstruction.Move);

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).StartExploring(roverModel);

            roverSpeakerMock.Verify(e => e.SayNavigation(RoverNavigationMessage.ReachBoundary), Times.Once());
        }

        [Fact]
        public void StartExploring_If_Instruction_List_Has_Value_ConvertInstructionStrToEnum_Should_Be_Triggered()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "L", "R", "R" },
                FacingCompassPoint = EnmCompassPoint.North,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            plateauBoundaryCheckerMock.Setup(s => s.CheckOutOfBoundary(It.IsAny<RoverModel>(), It.IsAny<CoordinateModel>())).Returns(true);
            converterMock.Setup(s => s.ConvertInstructionStrToEnum(It.IsAny<string>())).Returns(EnmInstruction.Move);

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).StartExploring(roverModel);

            converterMock.Verify(e => e.ConvertInstructionStrToEnum(It.IsAny<string>()), Times.Exactly(roverModel.InstructionList.Count));
        }

        [Fact]
        public void SearchFacingCompassPoint_If_CurrentacingCompassPoint_Is_North_And_Instruction_Is_Left_FacingCOmpassPoint_Should_Be_West()
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

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).SearchFacingCompassPoint(EnmInstruction.Left, roverModel);

            Assert.Equal(EnmCompassPoint.West, roverModel.FacingCompassPoint);
        }

        [Fact]
        public void SearchFacingCompassPoint_If_CurrentacingCompassPoint_Is_North_And_Instruction_Is_Rigth_FacingCOmpassPoint_Should_Be_East()
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

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).SearchFacingCompassPoint(EnmInstruction.Right, roverModel);

            Assert.Equal(EnmCompassPoint.East, roverModel.FacingCompassPoint);
        }

        [Fact]
        public void SearchFacingCompassPoint_If_CurretnFacingCompassPoint_Is_South_And_Instruction_Is_Left_FacingCOmpassPoint_Should_Be_East()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.South,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).SearchFacingCompassPoint(EnmInstruction.Left, roverModel);

            Assert.Equal(EnmCompassPoint.East, roverModel.FacingCompassPoint);
        }

        [Fact]
        public void SearchFacingCompassPoint_If_CurretnFacingCompassPoint_Is_South_And_Instruction_Is_Rightt_FacingCOmpassPoint_Should_Be_West()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.South,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).SearchFacingCompassPoint(EnmInstruction.Right, roverModel);

            Assert.Equal(EnmCompassPoint.West, roverModel.FacingCompassPoint);
        }

        [Fact]
        public void SearchFacingCompassPoint_If_CurretnFacingCompassPoint_Is_East_And_Instruction_Is_Rigth_FacingCOmpassPoint_Should_Be_South()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.East,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).SearchFacingCompassPoint(EnmInstruction.Right, roverModel);

            Assert.Equal(EnmCompassPoint.South, roverModel.FacingCompassPoint);
        }

        [Fact]
        public void SearchFacingCompassPoint_If_CurretnFacingCompassPoint_Is_East_And_Instruction_Is_Left_FacingCOmpassPoint_Should_Be_North()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.East,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).SearchFacingCompassPoint(EnmInstruction.Left, roverModel);

            Assert.Equal(EnmCompassPoint.North, roverModel.FacingCompassPoint);
        }

        [Fact]
        public void SearchFacingCompassPoint_If_CurretnFacingCompassPoint_Is_West_And_Instruction_Is_Left_FacingCOmpassPoint_Should_Be_South()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.West,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).SearchFacingCompassPoint(EnmInstruction.Left, roverModel);

            Assert.Equal(EnmCompassPoint.South, roverModel.FacingCompassPoint);
        }

        [Fact]
        public void SearchFacingCompassPoint_If_CurretnFacingCompassPoint_Is_East_And_Instruction_Is_Right_FacingCOmpassPoint_Should_Be_North()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.West,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).SearchFacingCompassPoint(EnmInstruction.Right, roverModel);

            Assert.Equal(EnmCompassPoint.North, roverModel.FacingCompassPoint);
        }

        [Fact]
        public void SearchFacingCompassPoint_If_Instruction_Is_Right_RoverSpeaker_Should_Say_Turning_Right()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.West,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).SearchFacingCompassPoint(EnmInstruction.Right, roverModel);

            roverSpeakerMock.Verify(e => e.SayNavigation(RoverNavigationMessage.TurnRight), Times.Once());
        }

        [Fact]
        public void SearchFacingCompassPoint_If_Instruction_Is_Left_RoverSpeaker_Should_Say_Turning_Left()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.West,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).SearchFacingCompassPoint(EnmInstruction.Left, roverModel);

            roverSpeakerMock.Verify(e => e.SayNavigation(RoverNavigationMessage.TurnLeft), Times.Once());
        }

        [Fact]
        public void SearchFacingCompassPoint_If_Instruction_Is_Unknown_Throw_IndexOutOfRangeException()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.West,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            Assert.Throws<IndexOutOfRangeException>(() => new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).SearchFacingCompassPoint((EnmInstruction)10, roverModel));
        }


        [Theory]
        [InlineData(EnmCompassPoint.North, 1, 2, 1, 3)]
        [InlineData(EnmCompassPoint.South, 4, 3, 4, 2)]
        [InlineData(EnmCompassPoint.South, 2, 5, 2, 4)]
        [InlineData(EnmCompassPoint.North, 3, 3, 3, 4)]
        public void GetNextCoordinate_If_Current_Facing_Compass_Is_North_Or_South_Return_Y_Is_Affected(EnmCompassPoint currentFacingCompassPoint, int currentCoordinate_X, int currentCoordinate_Y, int nexCoordinate_X, int nexCoordinate_Y)
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = currentFacingCompassPoint,
                CurrentCoordinate = new CoordinateModel(currentCoordinate_X, currentCoordinate_Y),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            var _nextCoordinate = new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).GetNextCoordinate(roverModel);

            Assert.Equal(_nextCoordinate.Coordinate_X, nexCoordinate_X);
            Assert.Equal(_nextCoordinate.Coordinate_Y, nexCoordinate_Y);
        }

        [Theory]
        [InlineData(EnmCompassPoint.West, 1, 2, 0, 2)]
        [InlineData(EnmCompassPoint.West, 4, 3, 3, 3)]
        [InlineData(EnmCompassPoint.East, 2, 5, 3, 5)]
        [InlineData(EnmCompassPoint.East, 3, 3, 4, 3)]
        public void GetNextCoordinate_If_Current_Facing_Compass_Is_West_Or_East_Return_X_Is_Affected(EnmCompassPoint currentFacingCompassPoint, int currentCoordinate_X, int currentCoordinate_Y, int nexCoordinate_X, int nexCoordinate_Y)
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = currentFacingCompassPoint,
                CurrentCoordinate = new CoordinateModel(currentCoordinate_X, currentCoordinate_Y),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            var _nextCoordinate = new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).GetNextCoordinate(roverModel);

            Assert.Equal(_nextCoordinate.Coordinate_X, nexCoordinate_X);
            Assert.Equal(_nextCoordinate.Coordinate_Y, nexCoordinate_Y);
        }

        [Fact]
        public void GetNextCoordinate_If_Current_Facing_Compass_Is_North_Y_Coordinate_Should_Increase()
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

            var roverCoordinate_Y = roverModel.CurrentCoordinate.Coordinate_Y;

            var nextCoordinate = new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).GetNextCoordinate(roverModel);

            Assert.Equal(roverCoordinate_Y + 1, nextCoordinate.Coordinate_Y);
        }

        [Fact]
        public void GetNextCoordinate_If_Current_Facing_Compass_Is_South_Y_Coordinate_Should_Decrease()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.South,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            var roverCoordinate_Y = roverModel.CurrentCoordinate.Coordinate_Y;

            var nextCoordinate = new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).GetNextCoordinate(roverModel);

            Assert.Equal(roverCoordinate_Y - 1, nextCoordinate.Coordinate_Y);
        }

        [Fact]
        public void GetNextCoordinate_If_Current_Facing_Compass_Is_West_X_Coordinate_Should_Decrease()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.West,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            var roverCoordinate_X = roverModel.CurrentCoordinate.Coordinate_X;

            var nextCoordinate = new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).GetNextCoordinate(roverModel);

            Assert.Equal(roverCoordinate_X - 1, nextCoordinate.Coordinate_X);
        }

        [Fact]
        public void GetNextCoordinate_If_Current_Facing_Compass_Is_East_X_Coordinate_Should_Increase()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = EnmCompassPoint.East,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            var roverCoordinate_X = roverModel.CurrentCoordinate.Coordinate_X;

            var nextCoordinate = new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).GetNextCoordinate(roverModel);

            Assert.Equal(roverCoordinate_X + 1, nextCoordinate.Coordinate_X);
        }

        [Fact]
        public void GetNextCoordinate_If_Current_Facing_Compass_Unknown_Throw_IndexOutOfRangeException()
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = (EnmCompassPoint)10,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            Assert.Throws<IndexOutOfRangeException>(() => new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).GetNextCoordinate(roverModel));
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(4, 3)]
        [InlineData(2, 5)]
        [InlineData(3, 3)]
        public void Move_If_Next_Step_Coordinates_Should_Be_Set_Rovers_Current_Coordinates(int nextCoordinate_X, int nextCoordinate_Y)
        {
            var roverModel = new RoverModel()
            {
                Name = "MyName",
                Identity = "MyIdentity",
                InstructionList = new System.Collections.Generic.List<string>() { "L", "R", "L" },
                FacingCompassPoint = (EnmCompassPoint)10,
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(1, 2),
                Mission = "Mission",
            };

            var nextStepCoordinate = new CoordinateModel(nextCoordinate_X, nextCoordinate_Y);

            new RoverMovement(converterMock.Object, plateauBoundaryCheckerMock.Object, roverSpeakerMock.Object).Move(roverModel, nextStepCoordinate);

            Assert.Equal(roverModel.CurrentCoordinate.Coordinate_X, nextStepCoordinate.Coordinate_X);
            Assert.Equal(roverModel.CurrentCoordinate.Coordinate_Y, nextStepCoordinate.Coordinate_Y);
        }
    }
}

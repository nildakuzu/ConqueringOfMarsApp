using ConqueringOfMars.Consant;
using ConqueringOfMars.Consant.Enum;
using ConqueringOfMars.Interface;
using ConqueringOfMars.Model;
using System;

namespace ConqueringOfMars.Class
{
    public class RoverMovement : IRoverMovement
    {

        private readonly IConverter _converter;
        private readonly IPlateauBoundaryChecker _plateauBoundaryChecker;
        private readonly IRoverSpeaker _roverSpeaker;

        public RoverMovement(IConverter converter,
            IPlateauBoundaryChecker plateauBoundaryChecker,
            IRoverSpeaker roverSpeaker)
        {
            _converter = converter;
            _plateauBoundaryChecker = plateauBoundaryChecker;
            _roverSpeaker = roverSpeaker;
        }

        public void SearchFacingCompassPoint(EnmInstruction currentInstruction, RoverModel rover)
        {
            var currentFacingCompassPoint = (int)rover.FacingCompassPoint;

            switch (currentInstruction)
            {
                case EnmInstruction.Right:
                    currentFacingCompassPoint -= 1;
                    _roverSpeaker.SayNavigation(RoverNavigationMessage.TurnRight);
                    break;
                case EnmInstruction.Left:
                    currentFacingCompassPoint += 1;
                    _roverSpeaker.SayNavigation(RoverNavigationMessage.TurnLeft);
                    break;
                case EnmInstruction.Move:
                    break;
                default:
                    throw new IndexOutOfRangeException($"Unknown instruction : {currentInstruction}");
            }

            currentFacingCompassPoint = currentFacingCompassPoint % 4;

            if (currentFacingCompassPoint == 0)
            {
                currentFacingCompassPoint = 4;
            }

            rover.FacingCompassPoint = (EnmCompassPoint)currentFacingCompassPoint;
        }

        public CoordinateModel GetNextCoordinate(RoverModel rover)
        {
            var nextCoordinate_X = rover.CurrentCoordinate.Coordinate_X;
            var nextCoordinate_Y = rover.CurrentCoordinate.Coordinate_Y;

            string facingMsg;

            switch (rover.FacingCompassPoint)
            {
                case EnmCompassPoint.North:
                    nextCoordinate_Y += 1;
                    facingMsg = RoverNavigationMessage.FacingNorth;
                    break;
                case EnmCompassPoint.West:
                    nextCoordinate_X -= 1;
                    facingMsg = RoverNavigationMessage.FacingWest;
                    break;
                case EnmCompassPoint.South:
                    facingMsg = RoverNavigationMessage.FacingSouth;
                    nextCoordinate_Y -= 1;
                    break;
                case EnmCompassPoint.East:
                    facingMsg = RoverNavigationMessage.FacingEast;
                    nextCoordinate_X += 1;
                    break;
                default:
                    throw new IndexOutOfRangeException($"Unknown FacingCompassPoint : {rover.FacingCompassPoint}");
            }

            _roverSpeaker.SayNavigation(facingMsg);

            return new CoordinateModel(nextCoordinate_X, nextCoordinate_Y);
        }

        public void Move(RoverModel rover, CoordinateModel nextStep)
        {
            rover.CurrentCoordinate.Coordinate_X = nextStep.Coordinate_X;
            rover.CurrentCoordinate.Coordinate_Y = nextStep.Coordinate_Y;

            _roverSpeaker.SayNavigation(RoverNavigationMessage.Moving);
        }

        public void StartExploring(RoverModel rover)
        {
            _roverSpeaker.SayIdentity(rover);

            foreach (var instruction in rover.InstructionList)
            {
                var currentInstruction = _converter.ConvertInstructionStrToEnum(instruction);

                SearchFacingCompassPoint(currentInstruction, rover);

                if (currentInstruction == EnmInstruction.Move)
                {
                    var nextCoordinate = GetNextCoordinate(rover);

                    var outOfBoundary = _plateauBoundaryChecker.CheckOutOfBoundary(rover, nextCoordinate);

                    if (outOfBoundary)
                    {
                        _roverSpeaker.SayNavigation(RoverNavigationMessage.ReachBoundary);
                    }
                    else
                    {
                        Move(rover, nextCoordinate);
                    }

                    _roverSpeaker.SayCoordinate(rover);
                }
            }
        }
    }
}

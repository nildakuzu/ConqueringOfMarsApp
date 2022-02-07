using ConqueringOfMars.Consant;
using ConqueringOfMars.Consant.Enum;
using ConqueringOfMars.Interface;
using System;

namespace ConqueringOfMars.Class
{
    public class Converter : IConverter
    {
        public EnmCompassPoint ConvertCompassPointStrToEnum(string strCompassPoint)
        {
            switch (strCompassPoint)
            {
                case CompassPoint.North:
                    return EnmCompassPoint.North;
                case CompassPoint.South:
                    return EnmCompassPoint.South;
                case CompassPoint.West:
                    return EnmCompassPoint.West;
                case CompassPoint.East:
                    return EnmCompassPoint.East;
                default:
                    throw new IndexOutOfRangeException($"Unknown compassPointStr : {strCompassPoint}");
            }
        }

        public string ConvertCompassPointEnumToStr(EnmCompassPoint enmCompassPoint)
        {
            switch (enmCompassPoint)
            {
                case EnmCompassPoint.North:
                    return CompassPoint.North;
                case EnmCompassPoint.South:
                    return CompassPoint.South;
                case EnmCompassPoint.West:
                    return CompassPoint.West;
                case EnmCompassPoint.East:
                    return CompassPoint.East;
                default:
                    throw new IndexOutOfRangeException($"Unknown enmCompassPoint : {enmCompassPoint}");
            }
        }

        public EnmInstruction ConvertInstructionStrToEnum(string strInstruction)
        {
            switch (strInstruction)
            {
                case Instruction.Left:
                    return EnmInstruction.Left;
                case Instruction.Right:
                    return EnmInstruction.Right;
                case Instruction.Move:
                    return EnmInstruction.Move;
                default:
                    throw new IndexOutOfRangeException($"Unknown strInstruction : {strInstruction}");
            }
        }

        public string ConvertInstructionEnumToStr(EnmInstruction enmInstruction)
        {
            switch (enmInstruction)
            {
                case EnmInstruction.Left:
                    return Instruction.Left;
                case EnmInstruction.Right:
                    return Instruction.Right;
                case EnmInstruction.Move:
                    return Instruction.Move;
                default:
                    throw new IndexOutOfRangeException($"Unknown enmInstruction : {enmInstruction}");
            }
        }
    }
}

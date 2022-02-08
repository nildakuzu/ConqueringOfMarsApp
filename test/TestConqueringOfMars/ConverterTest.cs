using ConqueringOfMars.Class;
using ConqueringOfMars.Consant.Enum;
using System;
using Xunit;

namespace TestConqueringOfMars
{
    public class ConverterTest
    {
        [Theory]
        [InlineData("A")]
        [InlineData("B")]
        [InlineData("C")]
        [InlineData("Z")]
        public void ConvertCompassPointStrToEnum_If_CompassPointStr_Is_Unknown_Throw_IndexOutOfRangeException(string compassPointStr)
        {
            Assert.Throws<IndexOutOfRangeException>(() => new Converter().ConvertCompassPointStrToEnum(compassPointStr));
        }

        [Theory]
        [InlineData("N", EnmCompassPoint.North)]
        [InlineData("E", EnmCompassPoint.East)]
        [InlineData("S", EnmCompassPoint.South)]
        [InlineData("W", EnmCompassPoint.West)]
        public void ConvertCompassPointStrToEnum_If_CompassPointStr_Valid_Return_Correct_Enum(string compassPointStr, EnmCompassPoint enmCompassPoint)
        {
            var _compassPointEnm = new Converter().ConvertCompassPointStrToEnum(compassPointStr);

            Assert.Equal(enmCompassPoint, _compassPointEnm);
        }

        [Theory]
        [InlineData("N", EnmCompassPoint.North)]
        [InlineData("E", EnmCompassPoint.East)]
        [InlineData("S", EnmCompassPoint.South)]
        [InlineData("W", EnmCompassPoint.West)]
        public void ConvertCompassPointEnumToStr_If_CompassPointEnum_Valid_Return_Correct_Str(string compassPointStr, EnmCompassPoint enmCompassPoint)
        {
            var _compassPointStr = new Converter().ConvertCompassPointEnumToStr(enmCompassPoint);

            Assert.Equal(compassPointStr, _compassPointStr);
        }

        [Fact]
        public void ConvertCompassPointEnumToStr_If_CompassPointEnm_Is_Unknown_Throw_IndexOutOfRangeException()
        {

            var uknownCompassPointEnm = (EnmCompassPoint)10;

            Assert.Throws<IndexOutOfRangeException>(() => new Converter().ConvertCompassPointEnumToStr(uknownCompassPointEnm));
        }

        [Theory]
        [InlineData((EnmInstruction)22)]
        [InlineData((EnmInstruction)25)]
        [InlineData((EnmInstruction)56)]
        [InlineData((EnmInstruction)999)]
        public void ConvertInstructionEnumToStr_If_Instruction_Is_Unknown_Throw_IndexOutOfRangeException(EnmInstruction instruction)
        {

            Assert.Throws<IndexOutOfRangeException>(() => new Converter().ConvertInstructionEnumToStr(instruction));
        }

        [Theory]
        [InlineData(EnmInstruction.Left, "L")]
        [InlineData(EnmInstruction.Right, "R")]
        [InlineData(EnmInstruction.Move, "M")]
        public void ConvertInstructionEnumToStr_If_Instruction_Is_Valid_Return_Correct_Str(EnmInstruction instruction, string validInstructionStr)
        {
            var _validInstructionStr = new Converter().ConvertInstructionEnumToStr(instruction);

            Assert.Equal(_validInstructionStr, validInstructionStr);
        }

        [Theory]
        [InlineData(EnmInstruction.Left, "L")]
        [InlineData(EnmInstruction.Right, "R")]
        [InlineData(EnmInstruction.Move, "M")]
        public void ConvertInstructionStrToEnum_If_Instruction_Is_Valid_Return_Correct_Enm(EnmInstruction validInstructionEnm, string instructionStr)
        {
            var _validInstructionEnm = new Converter().ConvertInstructionStrToEnum(instructionStr);

            Assert.Equal(_validInstructionEnm, validInstructionEnm);
        }
    }
}

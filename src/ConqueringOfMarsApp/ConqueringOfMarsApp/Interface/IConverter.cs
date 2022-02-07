using ConqueringOfMars.Consant.Enum;

namespace ConqueringOfMars.Interface
{
    public interface IConverter
    {
        string ConvertCompassPointEnumToStr(EnmCompassPoint enmCompassPoint);

        string ConvertInstructionEnumToStr(EnmInstruction enmInstruction);

        EnmCompassPoint ConvertCompassPointStrToEnum(string strCompassPoint);

        EnmInstruction ConvertInstructionStrToEnum(string strInstruction);
    }
}

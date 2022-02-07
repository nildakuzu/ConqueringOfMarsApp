using ConqueringOfMars.Consant.Enum;
using System.Collections.Generic;

namespace ConqueringOfMars.Model
{
    public class RoverModel 
    {
        public string Identity { get; set; }

        public string Name { get; set; }

        public string Mission { get; set; }

        public CoordinateModel CurrentCoordinate { get; set; }

        public CoordinateModel MaxExploringCoordinate { get; set; }

        public EnmCompassPoint FacingCompassPoint { get; set; }

        public List<string> InstructionList { get; set; }
    }
}

using ConqueringOfMars.Interface;
using ConqueringOfMars.Model;
using System;
using System.Text;

namespace ConqueringOfMars.Class
{
    public class RoverSpeaker : IRoverSpeaker
    {
        public string SayCoordinate(RoverModel roverModel)
        {
            var coordinateMsg = $"I am on ({roverModel.CurrentCoordinate.Coordinate_X},{roverModel.CurrentCoordinate.Coordinate_Y})";

            Console.WriteLine(coordinateMsg);

            return coordinateMsg;
        }

        public string SayNavigation(string msg)
        {
            Console.WriteLine(msg);

            return msg;
        }

        public string SayIdentity(RoverModel rover)
        {
            var sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine($" I am {rover.Identity} {rover.Name}");
            sb.AppendLine($" My Mission is {rover.Mission}");

            Console.WriteLine(sb.ToString());

            return sb.ToString();
        }
    }
}

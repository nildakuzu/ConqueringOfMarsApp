using ConqueringOfMars.Class;
using ConqueringOfMars.Consant.Enum;
using ConqueringOfMars.Interface;
using ConqueringOfMars.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ConqueringOfMars
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IPlateauBoundaryChecker, PlateauBoundaryChecker>()
                .AddTransient<IConverter, Converter>()
                .AddTransient<IRoverMovement, RoverMovement>()
                .AddTransient<IRoverSpeaker, RoverSpeaker>()
                .AddTransient<IRoverSpeaker, RoverSpeaker>()
                .BuildServiceProvider();

            IConfiguration Config = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                .AddJsonFile("appSettings.json")
                .Build();

            PlateauModel plataeuModel = InitiliazePlateauBoundary(Config);

            var rover1 = InitiliazeRover_1(plataeuModel);
            var rover2 = InitiliazeRover_2(plataeuModel);

            IRoverMovement roverMovement = serviceProvider.GetService<IRoverMovement>();

            roverMovement.StartExploring(rover1);
            roverMovement.StartExploring(rover2);

        }

        private static PlateauModel InitiliazePlateauBoundary(IConfiguration Config)
        {
            var plataeuBoundary_X = int.Parse(Config.GetSection("Boundary:Coordinate_Y").Value);
            var plataeuBoundary_Y = int.Parse(Config.GetSection("Boundary:Coordinate_Y").Value);

            return new PlateauModel(plataeuBoundary_X, plataeuBoundary_Y);
        }

        static RoverModel InitiliazeRover_1(PlateauModel plateauModel)
        {
            return new RoverModel()
            {
                Identity = "RVR906_W",
                Name = "Red Kit",
                Mission = "Exploration Of The Mars's West Side",
                CurrentCoordinate = new CoordinateModel(1, 2),
                MaxExploringCoordinate = new CoordinateModel(plateauModel.BoundaryCoordinate_X, plateauModel.BoundaryCoordinate_Y),
                FacingCompassPoint = EnmCompassPoint.North,
                InstructionList = new List<string> { "L", "M", "L", "M", "L", "M", "L", "M", "M" },
            };

        }

        static RoverModel InitiliazeRover_2(PlateauModel plateauModel)
        {
            return new RoverModel()
            {
                Identity = "RVR777_E",
                Name = "Sunrise",
                Mission = "Exploration Of The Mars's East Side",
                CurrentCoordinate = new CoordinateModel(3, 3),
                MaxExploringCoordinate = new CoordinateModel(plateauModel.BoundaryCoordinate_X, plateauModel.BoundaryCoordinate_Y),
                FacingCompassPoint = EnmCompassPoint.East,
                InstructionList = new List<string> { "M", "M", "R", "M", "M", "R", "M", "R", "R", "M" },
            };

        }
    }
}

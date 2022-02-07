namespace ConqueringOfMars.Model
{
    public class PlateauModel
    {
        public PlateauModel(int boundaryCoordinate_X, int boundaryCoordinate_Y)
        {
            BoundaryCoordinate_X = boundaryCoordinate_X;
            BoundaryCoordinate_Y = boundaryCoordinate_Y;
        }
        public int BoundaryCoordinate_X { get; set; }

        public int BoundaryCoordinate_Y { get; set; }
    }
}

namespace ConqueringOfMars.Model
{
    public class CoordinateModel 
    {
        public CoordinateModel(int coordinate_X, int coordinate_Y)
        {
            Coordinate_X = coordinate_X;
            Coordinate_Y = coordinate_Y;

        }
        public int Coordinate_X { get; set; }

        public int Coordinate_Y { get; set; }
    }
}

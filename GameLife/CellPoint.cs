namespace GameLife
{
    public class CellPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int CountNeighbors { get; set; }
        public CellPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;
            CellPoint p = (CellPoint)obj;
            return (X == p.X) && (Y == p.Y);
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}

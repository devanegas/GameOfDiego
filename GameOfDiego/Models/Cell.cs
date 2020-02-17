namespace GameOfDiego
{
    public class Cell 
    {
        public int x { get; set; }
        public int y { get; set; }
        public bool IsAlive { get; set; }

        public override string ToString()
        {
            return $"({x},{y}, {(IsAlive ? "Alive" : "Dead")})";
        }

        public Cell BecomeAlive()
        {
            return new Cell { x = this.x, y = this.y, IsAlive = true };
        }
        public Cell Die()
        {
            return new Cell { x = this.x, y = this.y, IsAlive = false };
        }

    }

}

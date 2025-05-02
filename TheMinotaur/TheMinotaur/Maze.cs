namespace TheMinotaur
{
    internal class Maze
    {
        int rows;
        int cols;

        List<List<Tile>> tiles;

        public Maze()
        {
            Random rand = new Random();
            rows = rand.Next(Options.mapMin, Options.mapMax);
            cols = rand.Next(Options.mapMin, Options.mapMax);
            tiles = new List<List<Tile>>();
        }

        public void GenerateMaze()
        {
            foreach (List<Tile> row in tiles)
            {
                foreach(Tile tile in row)
                {
                    
                }
            }
        }

        public void PrintMap()
        {

        }
    }
}

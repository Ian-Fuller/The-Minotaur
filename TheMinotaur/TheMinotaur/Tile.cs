namespace TheMinotaur
{
    internal class Tile
    {
        // Position of the tile in the grid it's a part of
        public int row;
        public int col;

        // Keep track of neighbors
        public Tile north { get; set; }
        public Tile south { get; set; }
        public Tile east { get; set; }
        public Tile west { get; set; }
        public List<Tile> nbrs
        {
            get
            {
                return new[]
                {
                    north, south, east, west
                }
                .Where(c => c != null)
                .ToList();
            }
        }

        // Tiles this tile is linked to
        private Dictionary<Tile, bool> links;

        // Used by the DFS algorithm to ignore previously visited tiles
        public bool visited { get; set; }

        // Constructor
        public Tile(int row, int col)
        {
            this.row = row;
            this.col = col;
            links = new Dictionary<Tile, bool>();
            visited = false;
        }

        // Linking functions
        public void Link(Tile tile, bool bidirectional = true)
        {
            links[tile] = true;
            if (bidirectional)
            {
                tile.Link(this, false);
            }
        }
        public void Unlink(Tile tile, bool bidirectional = true)
        {
            links.Remove(tile);
            if (bidirectional)
            {
                tile.Unlink(this, false);
            }
        }
        public bool IsLinked(Tile tile)
        {
            if (tile == null)
            {
                return false;
            }
            return links.ContainsKey(tile);
        }
    }
}

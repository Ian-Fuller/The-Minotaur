namespace TheMinotaur
{
    internal class Cell
    {
        // Position of the cell in the grid it's a part of
        public int row;
        public int col;

        // Keep track of neighbors
        public Cell north { get; set; }
        public Cell south { get; set; }
        public Cell east { get; set; }
        public Cell west { get; set; }
        public List<Cell> nbrs
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

        // Cells this cell is linked to
        private Dictionary<Cell, bool> links;

        // Used by the DFS algorithm to ignore previously visited cells
        public bool visited { get; set; }

        // Constructor
        public Cell(int row, int col)
        {
            this.row = row;
            this.col = col;
            links = new Dictionary<Cell, bool>();
            visited = false;
        }

        // Linking functions
        public void Link(Cell cell, bool bidirectional = true)
        {
            links[cell] = true;
            if (bidirectional)
            {
                cell.Link(this, false);
            }
        }
        public void Unlink(Cell cell, bool bidirectional = true)
        {
            links.Remove(cell);
            if (bidirectional)
            {
                cell.Unlink(this, false);
            }
        }
        public bool IsLinked(Cell cell)
        {
            if (cell == null)
            {
                return false;
            }
            return links.ContainsKey(cell);
        }
    }
}

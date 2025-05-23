﻿namespace MazesInCS
{
    public class Cell
    {
        // Position in the maze
        public int Row { get; }
        public int Column { get; }

        // Neighboring cells
        public Cell North { get; set; }
        public Cell South { get; set; }
        public Cell East { get; set; }
        public Cell West { get; set; }
        public List<Cell> Neighbors
        {
            get { return new[] { North, South, East, West }.Where(c => c != null).ToList(); }
        }

        // Cells that are linked to this cell
        private readonly Dictionary<Cell, bool> _links;
        public List<Cell> Links => _links.Keys.ToList();

        // For DFS algorithm
        public bool Visited { get; set; }

        public Cell(int row, int col)
        {
            Row = row;
            Column = col;
            _links = new Dictionary<Cell, bool>();
            Visited = false;
        }

        // Linking functions
        public void Link(Cell cell, bool bidirectional = true)
        {
            _links[cell] = true;
            if (bidirectional)
            {
                cell.Link(this, false);
            }
        }
        public void Unlink(Cell cell, bool bidirectional = true)
        {
            _links.Remove(cell);
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
            return _links.ContainsKey(cell);
        }
    }
}

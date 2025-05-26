using System.Numerics;
using System.Text;

namespace TheMinotaur
{
    internal class Maze
    {
        private int rows;
        private int cols;
        private int top;
        private int left;

        private List<List<Cell>> grid; // Grid system that is used to generate the maze. This isn't representative of all the tiles the player will be able to traverse across
        private List<List<Entity>> entities; // Player, Monsters, Obstacles, and Items
        private char[,] tiles; // Text to be printed to the user that is generated using the above two variables

        public Maze()
        {
            Random rand = new Random();
            rows = rand.Next(Options.mapMin, Options.mapMax);
            cols = rand.Next(Options.mapMin, Options.mapMax) * 2;
            grid = new List<List<Cell>>();
            top = rows * 2 + 1; // 2 chars for each row + the top border
            left = cols * 2 + 2; // 2 chars for each column + the left border + the newline character
            tiles = new char[top, left];
            entities = new List<List<Entity>>();
            GenerateMaze();
            GenerateMazeTiles();
            CleanMaze();
        }

        // Access single cell by index
        public virtual Cell this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= rows || col < 0 || col >= cols)
                {
                    return null;
                }
                return grid[row][col];
            }
        }
        // Iterate over all cells
        private IEnumerable<Cell> Cells
        {
            get
            {
                foreach (List<Cell> row in grid)
                {
                    foreach (Cell cell in row)
                    {
                        yield return cell;
                    }
                }
            }
        }

        // Generates a grid of cells, and uses that grid to generate a maze using a Depth-First Search algorithm
        private void GenerateMaze()
        {
            // Populate the grid with cells
            for (int r = 0; r < rows; r++)
            {
                List<Cell> row = new List<Cell>();
                for (int c = 0; c < cols; c++)
                {
                    row.Add(new Cell(r, c));
                }
                grid.Add(row);
            }

            // Assign neighbors to cells
            foreach (Cell cell in Cells)
            {
                int row = cell.row;
                int col = cell.col;

                cell.north = this[row - 1, col];
                cell.south = this[row + 1, col];
                cell.east = this[row, col + 1];
                cell.west = this[row, col - 1];
            }

            // Generate maze using DFS algorithm
            Random rand = new Random();

            // Queue that the DFS search will use to navigate
            Stack<Cell> search = new Stack<Cell>();

            // Initialize variables for the do while loop
            Cell start = this[rand.Next(0, rows), rand.Next(0, cols)];
            Cell currentCell = start;
            currentCell.visited = true;
            search.Push(currentCell);

            do
            {
                // Put all of the unvisited neighbors of the current cell into a list
                List<Cell> unvNbrs = new List<Cell>();
                foreach (Cell nbr in currentCell.nbrs)
                {
                    if (!nbr.visited)
                    {
                        unvNbrs.Add(nbr);
                    }
                }

                // If there are no neighbors, go back up the search, otherwise visit the next one at random and link it to the previous cell
                if (unvNbrs.Count == 0)
                {
                    currentCell = search.Pop();
                }
                else
                {
                    Cell prevCell = currentCell;
                    currentCell = unvNbrs[rand.Next(0, unvNbrs.Count)];
                    currentCell.Link(prevCell);
                    currentCell.visited = true;
                    search.Push(currentCell);
                }
            }
            while (currentCell != start); // Loop ends once the DFS returns to the start
        }

        // Uses the data created by GenrateMaze() to create a map of tiles that can be displayed to the player so they can move around in it
        private void GenerateMazeTiles()
        {
            StringBuilder output = new StringBuilder();

            // Upper border
            output.Append("╬");
            for (int col = 0; col < cols; col++)
            {
                output.Append("╬╬");
            }
            output.Append("\n");

            // Main body
            foreach (List<Cell> row in grid) // For each row
            {
                output.Append("╬"); // Left border
                foreach (Cell cell in row)
                {
                    output.Append(cell.IsLinked(cell.east) ? "  " : " ╬");
                }
                output.Append("\n");

                output.Append("╬"); // Left border
                foreach (Cell cell in row)
                {
                    output.Append(cell.IsLinked(cell.south) ? " ╬" : "╬╬");
                }
                output.Append("\n");
            }

            int i = 0;
            for (int t = 0; t < top; t++)
            {
                for (int l = 0; l < left; l++)
                {
                    tiles[t, l] = output[i++]; 
                }
            }
        }

        // Fills tiles[,] with structures and entities
        private void PopulateMaze()
        {

        }

        // Changes the wall characters in tiles[,] so they flow with one another, instead of them all being ╬
        private void CleanMaze()
        {
            char[] boxDrawing = 
            {
                ' ', //  0 - none (shouldn't be possible)
                '║', //  1 - north
                '║', //  2 - south
                '║', //  3 - north & south
                '═', //  4 - east
                '╚', //  5 - north & east
                '╔', //  6 - east & south
                '╠', //  7 - north, east, & south
                '═', //  8 - west
                '╝', //  9 - north & west
                '╗', // 10 - south & west
                '╣', // 11 - north, south, & west
                '═', // 12 - east & west
                '╩', // 13 - north, east, & west
                '╦', // 14 - east, south, & west
                '╬'  // 15 - all
            };

            for (int t = 0; t < top; t++)
            {
                for (int l = 0; l < left; l++)
                {
                    if (tiles[t, l] == '╬') {
                        int total = 0;

                        try
                        {
                            if (tiles[t - 1, l] != ' ' && tiles[t - 1, l] != '\n')
                            {
                                total += 1;
                            }
                        }
                        catch { }

                        try
                        {
                            if (tiles[t + 1, l] != ' ' && tiles[t + 1, l] != '\n')
                            {
                                total += 2;
                            }
                        }
                        catch { }

                        try
                        {
                            if (tiles[t, l + 1] != ' ' && tiles[t, l + 1] != '\n')
                            {
                                total += 4;
                            }
                        }
                        catch { }

                        try
                        {
                            if (tiles[t, l - 1] != ' ' && tiles[t, l - 1] != '\n')
                            {
                                total += 8;
                            }
                        }
                        catch { }

                        tiles[t, l] = boxDrawing[total];
                    }
                }
            }
        }

        // Prints tiles[,] to the console
        public void PrintMaze()
        {
            StringBuilder output = new StringBuilder();

            for (int t = 0; t < top; t++)
            {
                for (int l = 0; l < left; l++)
                {
                    output.Append(tiles[t, l]);
                }
            }

            Console.Write(output.ToString());
        }
    }
}

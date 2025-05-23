﻿using System.Text;

namespace TheMinotaur
{
    internal class Maze
    {
        int rows;
        int cols;
        int top;
        int left;

        List<List<Cell>> grid; // Grid system that is used to generate the maze. This isn't representative of all the tiles the player will be able to traverse across
        List<List<Entity>> entities; // Player, Monsters, Obstacles, and Items
        //List<List<char>> tiles; // Text to be printed to the user that is generated using the above two variables
        char[,] tiles; // Text to be printed to the user that is generated using the above two variables

        public Maze()
        {
            Random rand = new Random();
            rows = rand.Next(Options.mapMin, Options.mapMax);
            cols = rand.Next(Options.mapMin, Options.mapMax) * 2;
            grid = new List<List<Cell>>();
            //tiles = new List<List<char>>();
            top = rows * 2 + 1;
            left = cols * 2 + 2;
            tiles = new char[top, left];
            entities = new List<List<Entity>>();
            GenerateMaze();
            GenerateTiles();
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
        public IEnumerable<Cell> Cells
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

        public void GenerateMaze()
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

        public void GenerateTiles()
        {
            // Top border
            for (int l = 0; l < rows; l++)
            {
                tiles[0, l] = '╬';
            }

            // Main body
            for (int t = 1; t < top; t+=2) // top, as in from the top // starts at 1 because of the top border
            {
                tiles[t, 0] = '╬';

                for (int l = 1; l < left; l += 2) // left, as in from the left // starts at 1 because of the left border
                {
                    tiles[t - 1, l - 1] = ' ';
                    tiles[t - 1, l] = '╬';
                }

                for (int l = 1; l < left; l += 2) // left, as in from the left
                {
                    tiles[t, l - 1] = '╬';
                    tiles[t, l] = '╬';
                }

                tiles[t, left] = '\n';
            }
        }

        public void PrintMap()
        {
            //StringBuilder output = new StringBuilder();

            //// Upper border
            //output.Append("╔");
            //for (int col = 0; col < cols; col++)
            //{
            //    output.Append("═╦");
            //}
            //output.Append("\n");

            //// Main body
            //foreach (List<Cell> row in grid) // For each row
            //{
            //    output.Append("║"); // Left border
            //    foreach (Cell cell in row)
            //    {
            //        output.Append(cell.IsLinked(cell.east) ? "  " : " ║");
            //    }
            //    output.Append("\n");

            //    output.Append("╠"); // Left border
            //    foreach (Cell cell in row)
            //    {
            //        output.Append(cell.IsLinked(cell.south) ? " ╬" : "═╬");
            //    }
            //    output.Append("\n");
            //}

            //Console.WriteLine(output.ToString());

            StringBuilder output = new StringBuilder();

            for (int t = 0; t < rows; t++)
            {
                for (int l = 0; l < rows; l++)
                {
                    output.Append(tiles[t, l]);
                }
            }

            Console.Write(output.ToString());
        }
    }
}

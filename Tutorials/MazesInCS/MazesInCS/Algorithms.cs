using System.Diagnostics;

namespace MazesInCS
{
    public class Algorithms
    {
        public static Grid BinaryTree(Grid grid, int seed = -1)
        {
            var rand = seed >= 0 ? new Random(seed) : new Random();

            foreach (var cell in grid.Cells)
            {
                var neighbors = new[] { cell.North, cell.East }.Where(c => c != null).ToList();
                if (!neighbors.Any())
                {
                    continue;
                }

                var neighbor = ListExtensions.Sample(neighbors, rand);
                if (neighbor != null)
                {
                    cell.Link(neighbor);
                }
            }

            return grid;
        }

        public static Grid Sidewinder(Grid grid, int seed = -1)
        {
            var rand = seed >= 0 ? new Random(seed) : new Random();

            foreach (var row in grid.Row)
            {
                var run = new List<Cell>();

                foreach (var cell in row)
                {
                    run.Add(cell);

                    var atEasternBoundary = cell.East == null;
                    var atNorthernBoundary = cell.North == null;

                    var shouldCloseOut = atEasternBoundary || (!atNorthernBoundary && rand.Next(2) == 0);

                    if (shouldCloseOut)
                    {
                        var member = ListExtensions.Sample(run, rand);
                        if (member.North != null)
                        {
                            member.Link(member.North);
                        }
                        run.Clear();
                    }
                    else
                    {
                        cell.Link(cell.East);
                    }
                }
            }

            return grid;
        }

        // function still doesn't link any cells together
        // RandomCell() still doesn't accept a seed
        public static Grid DepthFirst(Grid grid, int seed = -1)
        {
            var rand = seed >= 0 ? new Random(seed) : new Random();

            Stack<Cell> search = new Stack<Cell>();

            Cell start = grid.RandomCell(seed);
            Cell currentCell = start;
            currentCell.Visited = true;
            search.Push(currentCell);

            do
            {
                List<Cell> unvNbrs = new List<Cell>();
                foreach (Cell neighbor in currentCell.Neighbors)
                {
                    if (neighbor.Visited == false)
                    {
                        unvNbrs.Add(neighbor);
                    }
                }

                if (unvNbrs.Count == 0)
                {
                    currentCell = search.Pop();
                }
                else
                {
                    Cell prevCell = currentCell;
                    currentCell = unvNbrs[rand.Next(0, unvNbrs.Count)];
                    currentCell.Link(prevCell);
                    currentCell.Visited = true;
                    search.Push(currentCell);
                }
            }
            while (currentCell != start);

            return grid;
        }
    }
}
